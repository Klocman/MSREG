/*
* MSREG_universal.cpp
*
* Created: 20-09-14 12:55:07 PM
*  Author: Klocman
*/

#define F_CPU 8000000UL

#include <avr/io.h>
#include <avr/interrupt.h>
#include <avr/eeprom.h>
#include <avr/wdt.h>
#include <avr/pgmspace.h>
#include <stdio.h>

#include "Version/VersionInfo.h"
#include "PinConfiguration.h"

#include "Libs/7SEG.h"
#include "KlocTools.h"
#include "Classes.h"
#include "Libs/DHT22.h"
#include "Tools/RollingAverage.h"

#include "Libs/ExternalDeviceConnector.h"

#define REGULATION_TIMER_MAX 300
#define REGULATION_TIMER_MIN 0

#define UART_BUFFER_SIZE 34

const char newLine_P[] PROGMEM = "\r\n";
const char okResponse_P[] PROGMEM = "OK\r\n";

ExternalDeviceConnector extConnector;
LedIndicators ledIndicators;
Speaker speaker;
OutputTriac triacTemp;
OutputTriac triacHum;
Timer timerTemp;
Timer timerHum;
Timer sensorRefreshTimer;

RegulationSettings regSettingsTemp;
RegulationSettings regSettingsHum;

uint8_t saveSettingsQueued = 0;
uint8_t bootCompleted = 0;

//RollingAverage sensorResultAverageTemperature;
//RollingAverage sensorResultAverageHumidity;

//RollingAverage sensorAverage;

char buffor[UART_BUFFER_SIZE+1];

void OnWaitTimeChanged();
void OnReturnedToMainMenu();
void OnAlarmStarted ()
{
	speaker.PlayContinous();
	extConnector.SetDumbAlarm(1);
}
void OnAlarmEnded ()
{
	speaker.StopPlaying();
	extConnector.SetDumbAlarm(0);
}

#pragma region Alarm

enum AlarmType
{
	AlarmType_None = 0,
	AlarmType_BOOT_FAILED = 1,
	AlarmType_SENSOR_FAILURE = 2,
	AlarmType_TEMPERATURE_REGULATION = 3,
	AlarmType_HUMIDITY_REGULATION = 4,
	AlarmType_SOFTWARE_FAIL = 6,
	AlarmType_OTHER = 7
};

class AlarmSystem
{
	uint8_t activeFlags;
	//Action OnAlarmStarted;
	//Action OnAlarmEnded;
	
	public:
	/*inline void Init (Action onAlarmStartedCallback, Action onAlarmEndedCallback)
	{
		OnAlarmStarted = onAlarmStartedCallback;
		OnAlarmEnded = onAlarmEndedCallback;
		activeFlags = 0;
	}*/
	// Set AlarmType_None to clear all of the flags
	inline void SetAlarmFlag (AlarmType flag)
	{
		if (flag != AlarmType_None)
		{
			if (!activeFlags)
			{
				OnAlarmStarted();
			}
			BIT_HIGH(activeFlags, flag);
		}
	}
	inline void ClearAlarmFlag (AlarmType flag)
	{
		if (flag != AlarmType_None)
		{
			if (activeFlags)
			{
				BIT_LOW(activeFlags, flag);
				if (!activeFlags)
				{
					OnAlarmEnded();
				}
			}
		}
	}
	inline void ClearAllFlags ()
	{
		activeFlags = 0;
	}
	inline uint8_t GetActiveFlags () const
	{
		return activeFlags;
	}
	inline uint8_t GetActiveCriticalFlags () const
	{
		return activeFlags & ~((1<<AlarmType_HUMIDITY_REGULATION) | (1<<AlarmType_TEMPERATURE_REGULATION));
	}
	// Get number of the most significant set alarm flag. If no flags are set, 0 is returned.
	uint8_t GetAlarmDigit () const
	{
		if (activeFlags)
		{
			for(uint8_t i = 0; i < 8; i++)
			{
				if (BIT_READ(activeFlags, i))
				{
					return i;
				}
			}
		}
		return 0;
	}
};

AlarmSystem alarm;

#pragma endregion Alarm

#pragma region EEPROM

#define EEvalidValue 0x12
uint8_t EEMEM EEvalid;
RegulationSettings EEMEM EeTempConfig;
RegulationSettings EEMEM EeHumConfig;

inline void SaveSettings ()
{
	/*
	RegulationSettingsExtract te,he;
	te = regSettingsTemp.GetExtract();
	he = regSettingsHum.GetExtract();
	
	// EEPROM is not valid, make it valid
	uint8_t isInvalid = (eeprom_read_byte(&EEvalid) != EEvalidValue);
	
	RegulationSettingsExtract t,h;
	eeprom_read_block(&t, &EeTempConfig, sizeof(RegulationSettingsExtract));
	eeprom_read_block(&h, &EeHumConfig, sizeof(RegulationSettingsExtract));
	
	if(isInvalid || !te.Equals(&t))
	{
	eeprom_write_block(&te, &EeTempConfig, sizeof(RegulationSettingsExtract));
	}
	
	if (isInvalid || !he.Equals(&h))
	{
	eeprom_write_block(&he, &EeHumConfig, sizeof(RegulationSettingsExtract));
	}
	
	if (isInvalid)
	{
	eeprom_write_byte(&EEvalid, EEvalidValue);
	}*/
	
	eeprom_write_block(&regSettingsTemp, &EeTempConfig, sizeof(RegulationSettings));
	eeprom_write_block(&regSettingsHum, &EeHumConfig, sizeof(RegulationSettings));
	eeprom_write_byte(&EEvalid, EEvalidValue);
}

void ReadDefaultSettings ()
{
	regSettingsTemp.Init(DHT22_TEMP_MIN, DHT22_TEMP_MAX, DHT22_TEMP_HIST_MAX, REGULATION_TIMER_MAX);
	regSettingsHum.Init(DHT22_HUM_MIN, DHT22_HUM_MAX, DHT22_HUM_HIST_MAX, REGULATION_TIMER_MAX);
	
	regSettingsTemp.alarmOffset.variable = 0;
	regSettingsTemp.histeresis.variable = 1;
	regSettingsTemp.targetValue.variable = 366;
	regSettingsTemp.waitTime.variable = 0;
	regSettingsTemp.normallyClosed.variable = 0;
	
	regSettingsHum.alarmOffset.variable = 0;
	regSettingsHum.histeresis.variable = 5;
	regSettingsHum.targetValue.variable = 500;
	regSettingsHum.waitTime.variable = 0;
	regSettingsHum.normallyClosed.variable = 0;
	
	OnWaitTimeChanged();
}

void ReadSettings ()
{
	if(eeprom_read_byte(&EEvalid) != EEvalidValue)
	{
		// EEPROM is not valid, read fail safe settings
		ReadDefaultSettings();
	}
	else
	{
		eeprom_read_block(&regSettingsTemp, &EeTempConfig, sizeof(RegulationSettings));
		eeprom_read_block(&regSettingsHum, &EeHumConfig, sizeof(RegulationSettings));
		
		OnWaitTimeChanged();
	}
}

#pragma endregion EEPROM

#pragma region Seven segment menu

enum DisplayedVariable
{
	Display_Temperature = 0,
	Display_TemperatureValue = 1,
	Display_TemperatureHist = 2,
	Display_TemperatureTime = 3,
	Display_TemperatureAlarm = 4,
	
	Display_Humidity = 10,
	Display_HumidityValue = 11,
	Display_HumidityHist = 12,
	Display_HumidityTime = 13,
	Display_HumidityAlarm = 14,
	
	Display_OtherAlarm = 20
};

class SevSegMenu
{
	private:
	uint8_t currentlyDisplayed;
	uint8_t forcedAlarm;
	SevenSegmentDisplay sevSegDisplay;
	
	Timer menuTimeout;
	
	inline void TimeoutTimerElapsed ()
	{
		if (currentlyDisplayed > Display_Temperature && currentlyDisplayed < Display_Humidity)
		{
			currentlyDisplayed = Display_Temperature;
			OnReturnedToMainMenu();
		}
		else if (currentlyDisplayed > Display_Humidity && currentlyDisplayed < Display_OtherAlarm)
		{
			currentlyDisplayed = Display_Humidity;
			OnReturnedToMainMenu();
		}
		//else // We are already in the main menu!
	}
	
	public:
	inline void Init ()
	{
		currentlyDisplayed = Display_Temperature;
		sevSegDisplay.SetupPins();
		
		menuTimeout.Init();
		menuTimeout.SetCounterTop(50);
	}
	inline void DrawOutput (const uint8_t currentMultiplex) const
	{
		sevSegDisplay.DrawOutput(currentMultiplex);
	}
	inline void SetAlarm (uint8_t alarmNumber)
	{
		forcedAlarm = alarmNumber;
	}
	inline void Tick ()
	{
		menuTimeout.Tick();
	}
	inline void MainMenuUpdate (ButtonEvent input, const DHT22result *sensorResult)
	{
		//sevSegDisplay.WriteVariable(123, 1, 0);
		
		if (forcedAlarm)
		{
			sevSegDisplay.WriteError(forcedAlarm);
			return;
		}
		
		if (!bootCompleted)
		{
			return;
		}
		
		// Menu timeout (auto return to main menu after inactivity)
		if (input != BUTTON_NONE)
		{
			menuTimeout.ResetCounter();
		}
		else
		{
			if (!menuTimeout.IsRunning())
			{
				TimeoutTimerElapsed(); // Does nothing unless not in main menu, so it can be ran frequently
			}
		}
		
		SingleSetting *targetSetting = 0;
		{
			RegulationSettings *targetSetings;
			if (currentlyDisplayed < Display_Humidity)
			{
				targetSetings = &regSettingsTemp;
			}
			else
			{
				targetSetings = &regSettingsHum;
			}
			
			switch(currentlyDisplayed)
			{
				case Display_TemperatureAlarm:
				case Display_HumidityAlarm:
				targetSetting = &targetSetings->alarmOffset;
				break;
				case Display_TemperatureHist:
				case Display_HumidityHist:
				targetSetting = &targetSetings->histeresis;
				break;
				case Display_TemperatureTime:
				case Display_HumidityTime:
				targetSetting = &targetSetings->waitTime;
				break;
				
				case Display_TemperatureValue:
				case Display_HumidityValue:
				targetSetting = &targetSetings->targetValue;
				break;
				
				default:
				break;
			}
		}
		
		switch(input)
		{
			case BUTTON_HOME_SINGLE:
			if (currentlyDisplayed >= Display_OtherAlarm)
			{
				currentlyDisplayed = Display_Temperature;
				OnReturnedToMainMenu();
			}
			else
			{
				currentlyDisplayed += 1;
				if (currentlyDisplayed == 5 || currentlyDisplayed == 15)
				{
					// Return to the main menu
					currentlyDisplayed -=5;
					OnReturnedToMainMenu();
				}
			}
			break;
			
			case BUTTON_HOME_ONHOLD:
			case BUTTON_HOME_RAPID:
			break;
			
			// Remove 1
			case BUTTON_LEFT_RAPID:
			case BUTTON_LEFT_SINGLE:
			if (targetSetting)
			{
				targetSetting->Sub1();
				OnWaitTimeChanged();
			}
			break;
			
			// Add 1
			case BUTTON_RIGHT_RAPID:
			case BUTTON_RIGHT_SINGLE:
			if (targetSetting)
			{
				targetSetting->Add1();
				OnWaitTimeChanged();
			}
			break;
			
			case BUTTON_LEFT_ONHOLD:
			if (currentlyDisplayed == Display_Humidity)
			currentlyDisplayed = Display_Temperature;
			break;
			
			case BUTTON_RIGHT_ONHOLD:
			if (currentlyDisplayed == Display_Temperature)
			currentlyDisplayed = Display_Humidity;
			break;
			
			default:
			break;
		}
		
		// Repaint display output
		switch(currentlyDisplayed)
		{
			case Display_Temperature:
			sevSegDisplay.WriteVariable(sensorResult->temperature, DHT22_RESULT_DECIMALPOINT_PLACE, 0);
			break;
			case Display_Humidity:
			sevSegDisplay.WriteVariable(sensorResult->humidity, DHT22_RESULT_DECIMALPOINT_PLACE, 0);
			break;
			
			// Settings: Time, hist, value and alarm
			default:
			if (targetSetting)
			{
				// If setting is a value, pass 0 to writeVariable, else pass 1 to replace 0 with "OFF"
				sevSegDisplay.WriteVariable(targetSetting->variable, DHT22_RESULT_DECIMALPOINT_PLACE,
				!(currentlyDisplayed == Display_HumidityValue || currentlyDisplayed == Display_TemperatureValue));
			}
			else
			{
				//sevSegDisplay.WriteError(9); if set, flashes briefly when entering settings
			}
			break;
		}
	}
	
	inline void TurnOnMenuLeds () const
	{
		if (currentlyDisplayed < Display_Humidity) // Temperature related
		{
			ledIndicators.TurnOn(LED_ShowTemp);
		}
		else if (currentlyDisplayed < Display_OtherAlarm) // Humidity related
		{
			ledIndicators.TurnOn(LED_ShowHum);
		}
		
		switch(currentlyDisplayed)
		{
			case Display_TemperatureAlarm:
			case Display_HumidityAlarm:
			ledIndicators.TurnOn(LED_SetAlarm);
			break;
			
			case Display_TemperatureHist:
			case Display_HumidityHist:
			ledIndicators.TurnOn(LED_SetHist);
			break;
			
			case Display_TemperatureTime:
			case Display_HumidityTime:
			ledIndicators.TurnOn(LED_SetTime);
			break;
			
			case Display_TemperatureValue:
			case Display_HumidityValue:
			ledIndicators.TurnOn(LED_SetValue);
			break;
			
			default:
			break;
		}
	}
};

SevSegMenu sevSegMenu;

#pragma endregion Seven segment menu

#pragma region Regulation logic

uint8_t regulationOverride = 0;

inline void DisableRegulationOverride()
{
	regulationOverride = 0;
	TCCR1B = 0;
}
inline void EnableRegulationOverride()
{
	regulationOverride = 1;
	TCNT1 = 0;
	// 5 seconds with 1024 prescaler
	OCR1A = 39063;
	// T1 CTC
	BIT_HIGH(TCCR1A, WGM12);
	// T1 OCR1 interrupt
	BIT_HIGH(TIMSK, OCIE1A);
	// T1 1024 prescaler, start timer
	BIT_HIGH(TCCR1B, CS10);
	BIT_HIGH(TCCR1B, CS12);
}

//////////////////////////////////////////////////////////////////////////
// Regulation logic
// Hysteresis and alarm should always be >= 0 (0 for disabled)
RegulationResult ProcessRegulationData ( const int16_t measuredValue, const RegulationSettings *settings)
{
	if (!regulationOverride)
	{
		if (settings->histeresis.variable <= 0 || !bootCompleted)
		{
			return Result_Disable;
		}
		
		if (measuredValue <= settings->targetValue.variable)
		{
			if ((settings->alarmOffset.variable > 0) && (measuredValue <= settings->targetValue.variable - settings->alarmOffset.variable))
			{
				return Result_BelowSettingAlarm;
			}
			return Result_BelowSetting;
		}
		else if (measuredValue >= (settings->targetValue.variable + settings->histeresis.variable))
		{
			if ((settings->alarmOffset.variable > 0) && (measuredValue >= settings->targetValue.variable + settings->histeresis.variable + settings->alarmOffset.variable))
			{
				return Result_AboveSettingAlarm;
			}
			return Result_AboveSetting;
		}
	}
	
	return Result_DoNothing;
	
	/* // Old regulation logic
	if (!histeresis)
	{
	return Result_Disable;
	}
	
	uint8_t tempHist = dht22_setting_temperature_histeresis/2;
	
	// If above set temperature + half histeresis (round up)
	if ((sensor_values.temperature - (tempHist + (dht22_setting_temperature_histeresis % 2) ? 1 : 0)) > dht22_setting_temperature_value)
	{
	// Set alarm flag if temperature goes too far out
	if (dht22_setting_temperature_alarm && (sensor_values.temperature - dht22_setting_temperature_alarm) > dht22_setting_temperature_value)
	return Result_AboveSettingAlarm;
	else
	return Result_AboveSetting;
	}
	// If below set temperature + half histeresis (round down)
	else if ((sensor_values.temperature + tempHist) <= dht22_setting_temperature_value)
	{
	if (dht22_setting_temperature_alarm && (sensor_values.temperature + dht22_setting_temperature_alarm) <= dht22_setting_temperature_value)
	return Result_BelowSettingAlarm;
	else
	return Result_BelowSetting;
	}
	
	return Result_DoNothing;
	*/
}

RegulationResult RegulationUpdate(const AlarmType regulationType, const DHT22result *sensorResult)
{
	RegulationResult result = Result_AboveSettingAlarm;//Result_WaitForTimer;
	
	OutputTriac *outTriac;
	Timer *outTimer;
	
	if (regulationType == AlarmType_TEMPERATURE_REGULATION)
	{
		result = ProcessRegulationData(sensorResult->temperature, &regSettingsTemp);
		outTimer = &timerTemp;
		outTriac = &triacTemp;
	}
	else //if (regulationType == AlarmType_HUMIDITY_REGULATION)
	{
		result = ProcessRegulationData(sensorResult->humidity, &regSettingsHum);
		outTimer = &timerHum;
		outTriac = &triacHum;
	}
	/*else
	{
		alarm.SetAlarmFlag(AlarmType_SOFTWARE_FAIL);
		return;
	}*/

	uint8_t triacPreviousState = outTriac->GetState();
	
	if (!outTimer->IsRunning())
	{
		switch(result)
		{
			case Result_AboveSettingAlarm:
			outTriac->SetOutput(0);
			alarm.SetAlarmFlag(regulationType);
			break;
			
			case Result_AboveSetting:
			outTriac->SetOutput(0);
			alarm.ClearAlarmFlag(regulationType);
			break;
			
			case Result_BelowSettingAlarm:
			outTriac->SetOutput(1);
			alarm.SetAlarmFlag(regulationType);
			break;
			
			case Result_BelowSetting:
			outTriac->SetOutput(1);
			alarm.ClearAlarmFlag(regulationType);
			break;
			
			case Result_DoNothing:
			// Clear alarm since when doing nothing we have to be in the hysteresis range
			alarm.ClearAlarmFlag(regulationType);
			break;
			
			default:
			case Result_WaitForTimer:
			break;
			
			case Result_Disable:
			outTriac->Disable();
			alarm.ClearAlarmFlag(regulationType);
			break;
		}
		
		if (triacPreviousState != outTriac->GetState())
		{
			outTimer->ResetCounter();
		}
	}
	else
	{
		result = Result_WaitForTimer;
	}
	
	return result;
}

#pragma endregion Regulation logic

#pragma region Uart control

uint8_t ProcessUartSettingEditHelper (SingleSetting* target)
{
	// Wait for transfer to finish
	_delay_ms(20);
	
	uint8_t inputs[7];
	inputs[6] = 0;
	int8_t i = 0;
	for (; i<7;i++)
	{
		//_delay_ms(10);
		uint16_t uartInput = extConnector.uGetc();
		if (uartInput == UART_NO_DATA)
		{
			// Should not happen, expecting 'f' as the last char
			return 1;
		}
		else
		{
			uint8_t smallUartInput = uartInput;
			//uart_putc(smallUartInput);
			if (smallUartInput >= '0' && smallUartInput <= '9')
			{
				inputs[i] = smallUartInput - '0';
			}
			else if (smallUartInput == '.' || smallUartInput == '+' || smallUartInput == '-')
			{
				inputs[i] = smallUartInput;
			}
			else if (smallUartInput == 'f')
			{
				if (i < 3)
				{
					// There is not enough data to make a valid input (0.0 minimum)
					return 2;
				}
				else
				{
					i--;
					break;
				}
			}
			else
			{
				// Unexpected character
				return 3;
			}
		}
	}
	
	if (i >= 7)
	{
		return 1;
	}
	
	int16_t result = 0;
	
	// Read inputs backwards
	for (int8_t count = 0;i>=0;i--)
	{
		if (count == 0)
		{
			if (i < 2)
			{
				// There is not enough data to make a valid input
				return 2;
			}
			
			if (inputs[i] == '.')
			{
				count++;
			}
			else if (inputs[i] < 10 && inputs[i-1] == '.')
			{
				result = inputs[i];
				count++;
				i--; // Skip the dot too
			}
			else
			{
				// Unexpected character
				return 3;
			}
		}
		else
		{
			if (inputs[i] < 10)
			{
				result += inputs[i] * PowerOf10((uint8_t)count);
				count++;
			}
			else if (inputs[i] == '-')
			{
				result *= -1;
				break;
			}
			else if (inputs[i] == '+')
			{
				break;
			}
			else
			{
				// Unexpected character
				return 3;
			}
			
			if (count > 4)
			{
				// Input is too long
				return 4;
			}
		}
	}
	
	// Check if value is valid
	if (target->SetValue(result) == SettingChange_OK)
	{
		return 0;
	} 
	else
	{
		// Value is out of acceptable range
		return 5;
	}
}

inline uint8_t ProcessUartSettingEdit ()
{
	uint16_t uartInput = extConnector.uGetc();
	if (uartInput == UART_NO_DATA)
	{
		// Missing setting set
		return 8;
	}
	
	RegulationSettings* selectedSettingSet;
	switch((uint8_t)uartInput)
	{
		case 't':
		selectedSettingSet = &regSettingsTemp;
		break;
		case 'h':
		selectedSettingSet = &regSettingsHum;
		break;
		
		default:
		// Unknown set sign
		return 9;
	}
	
	uartInput = extConnector.uGetc();
	if (uartInput == UART_NO_DATA)
	{
		// Missing setting number
		return 8;
	}
	
	switch((uint8_t)uartInput)
	{
		case '1':
		return ProcessUartSettingEditHelper(&selectedSettingSet->targetValue);
		
		case '2':
		return ProcessUartSettingEditHelper(&selectedSettingSet->histeresis);
		
		case '3':
		{
			uint8_t tempResult = ProcessUartSettingEditHelper(&selectedSettingSet->waitTime);
			if(tempResult == 0)
			{
				OnWaitTimeChanged();
			}
			return tempResult;
		}
		
		case '4':
		return ProcessUartSettingEditHelper(&selectedSettingSet->alarmOffset);
		
		case '5':
		return ProcessUartSettingEditHelper(&selectedSettingSet->normallyClosed);
		
		default:
		// Unknown setting number
		return 9;
	}
}

// Calibrate baud rate if host is sending space (' ', 0b00100000) char
inline void CalibrateUartBaud(const uint8_t readByte)
{
	if ((readByte & 0b00111111) == 0b00100000 || (readByte & 0b00111111) == 0)
	{
		switch(readByte)
		{
			case 0b00100000:
			// The space char is clear
			extConnector.SetMode(ExternalDevice_RS232);
			//extConnector.uPuts_p(PSTR("bOK\r\n"));
			extConnector.uPutc('b');
			extConnector.uPuts_p(okResponse_P);
			// Skip uart flush
			return;
			
			case 0b01100000:
			case 0b01000000:
			case 0b11000000:
			case 0b10000000:
			// Clock is running too fast
			{
				uint16_t tempRate = (uint16_t)(UBRRL | (UBRRH << 8));
				tempRate += 7;
				UBRRL = tempRate;
				UBRRH = tempRate >> 8;
			}
			break;
			
			case 0:
			// Serial port can spit out nulls for no reason when the USB converter gets plugged in
			break;
			
			default:
			// Clock is running too slow
			{
				uint16_t tempRate = (uint16_t)(UBRRL | (UBRRH << 8));
				tempRate -= 7;
				UBRRL = tempRate;
				UBRRH = tempRate >> 8;
			}
			break;
		}
		
		extConnector.uFlush();
	}
}

#define EXT_REPLY_TEMPSETTINGS 't'
#define EXT_REPLY_HUMSETTINGS 'h'

void UartSendSettings(const char type)
{
	extConnector.uPutc(type);
	RegulationSettings *rs = (type == EXT_REPLY_HUMSETTINGS) ? &regSettingsHum : &regSettingsTemp;
	rs->ToString(buffor);
	extConnector.uPuts(buffor);
}
/*
void UartSendHumSettings()
{
	extConnector.uPutc('h');
	regSettingsHum.ToString(buffor)//, UART_BUFFER_SIZE);
	extConnector.uPuts(buffor);
	//extConnector.uPuts_p(newLine_P);
}*/

uint8_t UartProcessControlCommand ()
{
	// Syntax: THf - T and H can be either 0 or 1, f is necessary at the end
	
	//uint16_t uartInput = extConnector.uGetc();
	//uint8_t tempRegOverride, humRegOverride;
	
	uint8_t uartResult[3];
	
	// This loop will flip the syntax to fHT for 012 pos.
	for (int8_t i = 2; i >= 0; i--)
	{
		_delay_ms(5);
		uint16_t uartInput = extConnector.uGetc();
		if (uartInput == UART_NO_DATA)
		{
			return 1;
		}
		
		uint8_t tempInput = (uint8_t)uartInput;
		uartResult[i] = tempInput - '0';
	}
	
	if (uartResult[2] > 1 || uartResult[1] > 1 || uartResult[0] != ('f' - '0'))
	{
		return 1;
	}
	
	/*
	tempRegOverride = ((uint8_t)uartInput) - '0';
	if (uartInput == UART_NO_DATA || tempRegOverride > 1)
	{
		return 1;
	}
	
	uartInput = extConnector.uGetc();
	humRegOverride = ((uint8_t)uartInput) - '0';
	if (uartInput == UART_NO_DATA || humRegOverride > 1)
	{
		return 1;
	}
	
	uartInput = extConnector.uGetc();
	if (uartInput == UART_NO_DATA || ((uint8_t)uartInput) != 'f')
	{
		return 1;
	}
	
	regulationOverride = 1;
	
	triacTemp.SetOutput(tempRegOverride);
	triacHum.SetOutput(humRegOverride);
	*/
	
	triacTemp.SetOutput(uartResult[2]);
	triacHum.SetOutput(uartResult[1]);
	
	EnableRegulationOverride();
	return 0;
}

inline void ProcessUartInput()
{
	for(uint8_t i = 10; i > 0; i--) // Max amount of commands to process in one loop of main
	{
		_delay_ms(17); // At least one delay
		
		uint16_t uartInput = extConnector.uGetc();
		if (uartInput == UART_NO_DATA)
		{
			return;
		}
		
		switch((uint8_t)uartInput)
		{
			case 't':
			//UartSendTempSettings();
			UartSendSettings(EXT_REPLY_TEMPSETTINGS);
			break;
			
			case 'h':
			//UartSendHumSettings();
			UartSendSettings(EXT_REPLY_HUMSETTINGS);
			break;
			
			case 'e':
			{
				uint8_t tempResult = ProcessUartSettingEdit();
				if (tempResult == 0)
				{
					extConnector.uPuts_p(PSTR("eOK"));
				}
				else
				{
					char resultText[] = {'e','E', (char)(tempResult + '0'), 0};
					extConnector.uPuts(resultText);
					
					// Ignore rest of the data packet in case it's corrupted
					//extConnector.uFlush();
				}
				extConnector.uPuts_p(newLine_P);
			}
			break;
			
			case 's':
			saveSettingsQueued = 1;
			//extConnector.uPuts_p(PSTR("sOK\r\n"));
			extConnector.uPutc('s');
			extConnector.uPuts_p(okResponse_P);
			break;
			
			case 'd':
			ReadDefaultSettings();
			//extConnector.uPuts_p(PSTR("dOK\r\n"));
			extConnector.uPutc('d');
			extConnector.uPuts_p(okResponse_P);
			break;
			
			case 'r':
			ReadSettings();
			//extConnector.uPuts_p(PSTR("rOK\r\n"));
			extConnector.uPutc('r');
			extConnector.uPuts_p(okResponse_P);
			break;
			
			case 'v':
			extConnector.uPutc('v');
			extConnector.uPuts_p(versionInfo_P);
			extConnector.uPuts_p(versionInfoCopyright_P);
			break;
			
			case 'c':
			if(UartProcessControlCommand())
			{
				extConnector.uPuts_p(PSTR("cE1"));
				extConnector.uPuts_p(newLine_P);
			}
			else
			{
				//extConnector.uPuts_p(PSTR("cOK\r\n"));
				extConnector.uPutc('c');
				extConnector.uPuts_p(okResponse_P);
			}
			break;
			
			case '!':
			extConnector.uPutc('!');
			extConnector.uPuts_p(okResponse_P);
			while(1){} //Wait for watchdog to kick in.
			break;
			
			default:
			CalibrateUartBaud((uint8_t)uartInput);
			break;
		}
	}
}

void SendAlarmInfo()
{
	sprintf_P(buffor, PSTR("\r\nA%u\r\n"), alarm.GetActiveFlags());
	extConnector.uPuts(buffor);
}

#pragma endregion Uart control

#pragma region Misc

//#define STACK_GUARD_VALUE 0b01010101
//volatile uint8_t stackGuard = STACK_GUARD_VALUE;

void OnWaitTimeChanged()
{
	// Fired every time any setting is changed by sevseg menu
	timerTemp.SetCounterTop(regSettingsTemp.waitTime.variable);
	timerHum.SetCounterTop(regSettingsHum.waitTime.variable);
}
void OnReturnedToMainMenu()
{
	speaker.PlayTone();
	saveSettingsQueued = 1;
	UartSendSettings(EXT_REPLY_TEMPSETTINGS);
	UartSendSettings(EXT_REPLY_HUMSETTINGS);
}

inline void MULTIPLEX_Setup()
{
	//BIT_HIGH(MULTIPLEX_1_PORT, MULTIPLEX_1_PIN);
	//BIT_HIGH(MULTIPLEX_2_PORT, MULTIPLEX_2_PIN);
	//BIT_HIGH(MULTIPLEX_3_PORT, MULTIPLEX_3_PIN);
	SET_PIN_OUTPUT(MULTIPLEX_1_DDR, MULTIPLEX_1_PIN);
	SET_PIN_OUTPUT(MULTIPLEX_2_DDR, MULTIPLEX_2_PIN);
	SET_PIN_OUTPUT(MULTIPLEX_3_DDR, MULTIPLEX_3_PIN);
	
	// Timer 0 running multiplexing
	TCCR0 |= (1 << CS01) | (1 << CS00); // Set prescaler and start the timer
	TIMSK |= (1 << TOIE0); // Set interrupt on overflow
}

#pragma endregion Misc

int main(void)
{
	// Enable watchdog (restart avr if program hangs for 2 seconds)
	wdt_enable(WDTO_2S);
	
	Buttons buttons;
	DHT22sensor sensor;
	
	//uart_init(UART_BAUD_SELECT(USART_BAUDRATE, F_CPU));
	extConnector.Init();
	
	// Initialize all connected devices
	//alarm.Init(OnAlarmStarted, OnAlarmEnded);
	alarm.ClearAllFlags();
	sevSegMenu.Init();
	ledIndicators.Init();
	buttons.Init();
	
	sensor.Init();
	sensorRefreshTimer.Init();
	sensorRefreshTimer.SetCounterTop(DHT22_REFRESHTIME);
	
	timerHum.Init();
	timerTemp.Init();
	
	speaker.InitSpeaker();
	speaker.PlayTone();
	
	//sensorResultAverageTemperature.Init();
	//sensorResultAverageHumidity.Init();
	
	MULTIPLEX_Setup();
	Timer_Setup();
	
	//ReadDefaultSettings(); done in readsettings if needed
	ReadSettings();
	
	triacTemp.InitPorts(&TRIAC_TEMP_PORT, &TRIAC_TEMP_DDR, TRIAC_TEMP_PIN, &regSettingsTemp.normallyClosed.variable);
	triacHum.InitPorts(&TRIAC_HUM_PORT, &TRIAC_HUM_DDR, TRIAC_HUM_PIN, &regSettingsHum.normallyClosed.variable);
	
	DHT22result sensorResult = {0,0};
	
	sei();
	//alarm.SetAlarmFlag(AlarmType_SOFTWARE_FAIL);
	//DHT22result tempResult = {0,0};
	while (1)
	{
		wdt_reset();
		uint8_t sendSensorDataOverUart = 0;
		
		if (!sensorRefreshTimer.IsRunning())
		{
			SensorResult sr = sensor.ReadSensor(&sensorResult);
			switch(sr)
			{
				case SensorOK:
				alarm.ClearAlarmFlag(AlarmType_SENSOR_FAILURE);
				bootCompleted = 1; // Enable regulation updates
				sendSensorDataOverUart = 1;
				sensorRefreshTimer.ResetCounter();
				//sensorAverage.ProcessValue(&sensorResult);
				break;
				
				default:
				alarm.SetAlarmFlag(AlarmType_SENSOR_FAILURE);
				
				sprintf_P(buffor, PSTR("E%u"), sr); // Newline gets added in SendAlarmInfo
				extConnector.uPuts(buffor); //uart_puts
				SendAlarmInfo();
				
				case SensorBusy:
				sensorRefreshTimer.AddTicks(20); // Reduce the error flood
				break;
			}
		}
		
		// If alarm is because of bad regulation, show normal menu instead of error code
		sevSegMenu.SetAlarm(alarm.GetActiveCriticalFlags());
		
		// Beep the speaker if buttons are pressed
		ButtonEvent currentButtonEvent = buttons.ProcessInputs();
		
		if (currentButtonEvent > BUTTON_MULTIPLE && currentButtonEvent < BUTTON_LEFT_RAPID )
		{
			speaker.PlayTone();
		}
		
		sevSegMenu.MainMenuUpdate(currentButtonEvent, &sensorResult);
		
		//////////////////////////////////////////////////////////////////////////
		// Regulation
		//_delay_ms(1);
		RegulationResult resTemp, resHum;
		
		resTemp = RegulationUpdate(AlarmType_TEMPERATURE_REGULATION, &sensorResult);
		resHum = RegulationUpdate(AlarmType_HUMIDITY_REGULATION, &sensorResult);
		
		//////////////////////////////////////////////////////////////////////////
		// Repaint LEDs
		sevSegMenu.TurnOnMenuLeds();
		
		if (alarm.GetActiveFlags())
		{
			ledIndicators.TurnOn(LED_Alarm);
		}
		
		if (triacTemp.GetState() || triacHum.GetState())
		{
			ledIndicators.TurnOn(LED_Praca);
		}
		ledIndicators.PushChanges();
		
		//////////////////////////////////////////////////////////////////////////
		// Update UART
		if (sendSensorDataOverUart)
		{
			//snprintf_P(buffor, UART_BUFFER_SIZE, PSTR("T%.3u.%u H%.3u.%u "),
			//extConnector.uPuts(buffor);
			
			//snprintf_P(buffor, UART_BUFFER_SIZE, PSTR("T%.3u.%u H%.3u.%u G%u N%u\r\n"),
			
			int8_t tempRemainder = sensorResult.temperature % 10;
			if (tempRemainder < 0)
			{
				tempRemainder *= -1;
			}
			
			sprintf_P(buffor, PSTR("T%i.%i H%u.%u G%u N%u"), // Newline at the end is added in SendAlarmInfo
				sensorResult.temperature / 10, tempRemainder,
				sensorResult.humidity / 10, sensorResult.humidity % 10,
				resTemp, resHum);
			extConnector.uPuts(buffor);
			SendAlarmInfo();
		}
		
		ProcessUartInput();
		
		if (saveSettingsQueued)
		{
			saveSettingsQueued = 0;
			SaveSettings();
		}
	}
}

#pragma region Interrupts

uint8_t currentMultiplex = 0;

ISR (TIMER0_OVF_vect)
{
	currentMultiplex++;
	if (currentMultiplex > 2)
	currentMultiplex = 0;
	
	BIT_LOW(MULTIPLEX_1_PORT, MULTIPLEX_1_PIN);
	BIT_LOW(MULTIPLEX_2_PORT, MULTIPLEX_2_PIN);
	BIT_LOW(MULTIPLEX_3_PORT, MULTIPLEX_3_PIN);
	
	//Update LEDs and 7seg display
	sevSegMenu.DrawOutput(currentMultiplex);
	ledIndicators.UpdateLeds(currentMultiplex);
	
	switch(currentMultiplex)
	{
		case 0:
		BIT_HIGH(MULTIPLEX_1_PORT, MULTIPLEX_1_PIN);
		break;
		case 1:
		BIT_HIGH(MULTIPLEX_2_PORT, MULTIPLEX_2_PIN);
		break;
		case 2:
		BIT_HIGH(MULTIPLEX_3_PORT, MULTIPLEX_3_PIN);
		break;
		
		default:
		break;
	}
}

ISR(TIMER2_COMP_vect)
{
	timerHum.Tick();
	timerTemp.Tick();
	sensorRefreshTimer.Tick();
	speaker.TimerTick();
	sevSegMenu.Tick();
}

// Regulation override timer
ISR (TIMER1_COMPA_vect)
{
	DisableRegulationOverride();
}

/*// Menu timeout
ISR (TIMER1_COMPB_vect)
{
	// Return menu to the main screen with a beep, if it's not already there.
	
	if (currentlyDisplayed > Display_Temperature && currentlyDisplayed < Display_Humidity)
	{
		currentlyDisplayed = Display_Temperature;
		SPEAK_PLAY_TONE();
		
		// Update EEPROM with new settings if necessary
		settingsDirty = 1;
	}
	else if (currentlyDisplayed > Display_Humidity && currentlyDisplayed < Display_OtherAlarm)
	{
		currentlyDisplayed = Display_Humidity;
		SPEAK_PLAY_TONE();
		
		// Update EEPROM with new settings if necessary
		settingsDirty = 1;
	}
	else // We are already in the main menu!
	{
		// Disable timer now, since we are playing return tones with speaker in other cases
		TIMER1_STOP();
	}
	}*/
/*
ISR(BADISR_vect)
{
	alarm.SetAlarmFlag(AlarmType_SOFTWARE_FAIL);
}*/

#pragma endregion Interrupts