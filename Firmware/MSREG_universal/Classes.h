/*
 * Classes.h
 *
 * Created: 30-09-14 9:27:50 PM
 *  Author: Klocman
 */ 


#ifndef CLASSES_H_
#define CLASSES_H_

#include <util/delay.h>
#include "KlocTools.h"
#include <util/atomic.h>

//////////////////////////////////////////////////////////////////////////
// Delay Timer
class Timer
{
	uint16_t counterTop;
	volatile uint16_t currentCounter;
	public:
	
	volatile Action OnTimerElapsed;
	
	inline void Init()
	{
		OnTimerElapsed = 0;
		currentCounter = 0;
		counterTop = 0;
	}
	
	// Target is in 12.3s format
	inline void SetCounterTop (int16_t target)
	{
		if (target <= 0)
		{
			counterTop = 0;
		}
		else
		{
			counterTop = ((uint16_t)target * 13) / 4;
		}
	}
	// Reset counter. When counter finishes, currentCounter equals 0.
	inline void ResetCounter ()
	{
		ATOMIC_BLOCK(ATOMIC_FORCEON)
		{
			//cli();
			currentCounter = counterTop;
			//sei();
		}
	}
	inline void Tick ()
	{
		if (currentCounter > 0)
		{
			currentCounter--;
			if (!currentCounter && OnTimerElapsed)
			{
				OnTimerElapsed();
			}
		}
	}
	inline uint8_t IsRunning () const
	{
		uint8_t result;
		ATOMIC_BLOCK(ATOMIC_FORCEON)
		{
			result = counterTop == 0 ? 0 : currentCounter;
		}
		return result;
	}
	inline void AddTicks (uint8_t ticks)
	{
		//ATOMIC_BLOCK(ATOMIC_FORCEON)
		{
			currentCounter += ticks;
		}
	}
	inline void Stop ()
	{
		ATOMIC_BLOCK(ATOMIC_FORCEON)
		{
			currentCounter = 0;
		}
	}
};

inline void Timer_Setup ()
{
	// Timer2 setup for 0.032512ms
	OCR2 = 0xfe;
	TCCR2 |= (1<<CS22) | (1<<CS21) | (1<<CS20); //prescaler to 1024
	TIMSK |= (1<<OCIE2);
}

//////////////////////////////////////////////////////////////////////////
// Output triacs

class OutputTriac
{
	private:
	volatile uint8_t *port;
	uint8_t pin;
	int16_t *normallyClosed;
	//uint8_t state;
	
	public:
	inline void InitPorts(volatile uint8_t *triacPort, volatile uint8_t *triacDdr, const uint8_t triacPin, int16_t *ncSetting)
	{
		//state = 0;
		port = triacPort;
		pin = triacPin;
		normallyClosed = ncSetting;
		
		SET_PIN_OUTPUT(*triacDdr, pin);
		Disable();
	}
	
	inline void Disable ()
	{
		BIT_LOW(*port, pin);
	}
	
	void SetOutput (uint8_t e)
	{
		ATOMIC_BLOCK(ATOMIC_FORCEON)
		{
			if (*normallyClosed)
			{
				e = !e;
			}
			
			if (e)
			{
				BIT_HIGH(*port, pin);
				//state = 1;
			}
			else
			{
				BIT_LOW(*port, pin);
				//state = 0;
			}
		}
	}
	
	inline uint8_t GetState () const
	{
		return BIT_READ(*port, pin) ? 1 : 0;
	}
};

//////////////////////////////////////////////////////////////////////////
// LED control

enum LedIDs
{
	LED_SetHist = 0,
	LED_SetTime = 1,
	LED_SetAlarm = 2,
	
	LED_SetValue = 3,
	LED_Praca = 4,
	LED_Alarm = 5,
	
	LED_ShowTemp = 6,
	LED_ShowHum = 7
};

class LedIndicators
{
	private:
	uint8_t ledRegisterBuffer;
	volatile uint8_t ledRegister;
	public:
	inline void TurnOn (LedIDs id)
	{
		BIT_HIGH(ledRegisterBuffer, id);
	}/*
	inline void TurnOff (LedIDs id)
	{
		BIT_LOW(ledRegisterBuffer, id);
	}
	inline void TurnOffAll ()
	{
		ledRegisterBuffer = 0;
	}*/
	inline void PushChanges ()
	{
		ledRegister = ledRegisterBuffer;
		ledRegisterBuffer = 0;
	}
	inline void Init ()
	{
		ledRegister = 0;
		ledRegisterBuffer = 0;
		SET_PIN_OUTPUT(LEDS_1_DDR, LEDS_1_PIN);
		SET_PIN_OUTPUT(LEDS_2_DDR, LEDS_2_PIN);
		SET_PIN_OUTPUT(LEDS_3_DDR, LEDS_3_PIN);
		BIT_HIGH(LEDS_1_PORT, LEDS_1_PIN);
		BIT_HIGH(LEDS_2_PORT, LEDS_2_PIN);
		BIT_HIGH(LEDS_3_PORT, LEDS_3_PIN);
	}
	inline void UpdateLeds (const uint8_t currentMultiplex) const
	{
		if (BIT_READ(ledRegister, currentMultiplex))
		{
			BIT_LOW(LEDS_1_PORT, LEDS_1_PIN);
		}
		else
		{
			BIT_HIGH(LEDS_1_PORT, LEDS_1_PIN);
		}
		
		if (BIT_READ(ledRegister, (currentMultiplex + 3)))
		{
			BIT_LOW(LEDS_2_PORT, LEDS_2_PIN);
		}
		else
		{
			BIT_HIGH(LEDS_2_PORT, LEDS_2_PIN);
		}
		
		if ((currentMultiplex < 2) && (BIT_READ(ledRegister, (currentMultiplex + 6))))
		{
			BIT_LOW(LEDS_3_PORT, LEDS_3_PIN);
		}
		else
		{
			BIT_HIGH(LEDS_3_PORT, LEDS_3_PIN);
		}
	}
};


//////////////////////////////////////////////////////////////////////////
// Buttons

enum ButtonEvent
{
	BUTTON_NONE = 0,
	BUTTON_MULTIPLE,
	BUTTON_LEFT_SINGLE = 5,
	BUTTON_RIGHT_SINGLE,
	BUTTON_HOME_SINGLE,
	BUTTON_ONHOLD_MODIFIER = 5,
	BUTTON_LEFT_ONHOLD = 10,
	BUTTON_RIGHT_ONHOLD,
	BUTTON_HOME_ONHOLD,
	// Rapid is mostly the same as single, except it doesn't make beeps
	BUTTON_RAPID_MODIFIER = 10,
	BUTTON_LEFT_RAPID = 15,
	BUTTON_RIGHT_RAPID,
	BUTTON_HOME_RAPID
};

#define BUTTONS_DEBOUNCE_CYCLES 3
#define BUTTONS_HOLD_CYCLES 27
class Buttons
{
	private:
	uint8_t holdTimer;
	ButtonEvent lastPressed;
	
	static inline ButtonEvent GrabInputs ()
	{
		ButtonEvent tempEvent = BUTTON_NONE;
		
		#ifdef BUTTON_ADC
		{
			// Start adc conversion and wait for the result
			BIT_HIGH(ADCSRA, ADSC);
			while(BIT_READ(ADCSRA, ADSC));
			
			/*
			* Read the result and figure out whats pressed.
			* result	S1	S2	S3
			* VCC		0	0	0
			* VCC 2/3	0	0	1
			* VCC 2/3	0	1	0
			* GND		1	0	0
			* 
			* Compare to values in between expected results.
			* 5/6
			* 7/12
			* 1/6
			*/
			
			uint8_t result = ADCH;
			if (result < 0xff/6) // GND, S1
			{
				tempEvent = BUTTON_LEFT_SINGLE;
			} 
			else if (result < (0xff*7)/12) // 1/2 VCC, S3
			{
				tempEvent = BUTTON_RIGHT_SINGLE;
			}
			else if (result < (0xff*5)/6) // 2/3 VCC, S2
			{
				tempEvent = BUTTON_HOME_SINGLE;
			}
		}
		#else
		{
			if (!BIT_READ(BUTTON_1_PININ, BUTTON_1_PIN)) // Low if S2 is pressed
			{
				tempEvent = BUTTON_LEFT_SINGLE;
			}
			
			if (!BIT_READ(BUTTON_2_PININ, BUTTON_2_PIN)) // Low if S3 is pressed
			{
				if (tempEvent != BUTTON_NONE)
				{
					return BUTTON_MULTIPLE;
				}
				tempEvent = BUTTON_RIGHT_SINGLE;
			}
			
			BIT_LOW(BUTTON_1_PORT, BUTTON_1_PIN);
			SET_PIN_OUTPUT(BUTTON_1_DDR, BUTTON_1_PIN);
			
			_delay_us(10);
			
			if (tempEvent == BUTTON_NONE)
			{
				if (!BIT_READ(BUTTON_2_PININ, BUTTON_2_PIN)) // Low if S1 is pressed
				{
					tempEvent = BUTTON_HOME_SINGLE;
				}
			}
			
			SET_PIN_INPUT(BUTTON_1_DDR, BUTTON_1_PIN);
			BIT_HIGH(BUTTON_1_PORT, BUTTON_1_PIN);
			//_delay_us(10);
		}
		#endif // BUTTONS_ADC
		
		return tempEvent;
	}
	
	public:
	inline void Init()
	{
		holdTimer = 0;
		lastPressed = BUTTON_NONE;
		
		#ifdef BUTTON_ADC
		{
			ADMUX = BUTTON_ADC | (1 << REFS0) | (1 << ADLAR);
			
			ADCSRA = (1 << ADEN) | (1 << ADPS2) | (1 << ADPS1);
		}
		#else
		{
			SET_PIN_INPUT(BUTTON_1_DDR, BUTTON_1_PIN);
			SET_PIN_INPUT(BUTTON_2_DDR, BUTTON_2_PIN);
			BIT_HIGH(BUTTON_1_PORT, BUTTON_1_PIN);
			BIT_HIGH(BUTTON_2_PORT, BUTTON_2_PIN);
		}
		#endif
	}
	inline ButtonEvent ProcessInputs()
	{
		ButtonEvent currentlyPressed = GrabInputs();
		
		if (holdTimer <= BUTTONS_HOLD_CYCLES)
		{
			holdTimer++;
		}
		
		switch(currentlyPressed)
		{
			case BUTTON_NONE:
			if (lastPressed != BUTTON_NONE && holdTimer <= BUTTONS_DEBOUNCE_CYCLES)
			{
				return BUTTON_NONE;
			}
			else
			{
				ButtonEvent temp = lastPressed;
				lastPressed = BUTTON_NONE;
				return temp;
			}
			break;
			
			case BUTTON_MULTIPLE:
			lastPressed = BUTTON_MULTIPLE;
			break;
			
			// Home, left and right buttons
			default:
			if (lastPressed == BUTTON_NONE)
			{
				lastPressed = currentlyPressed;
				holdTimer = 0;
				return BUTTON_NONE;
			}
			else if (lastPressed == currentlyPressed)
			{
				if (holdTimer == BUTTONS_HOLD_CYCLES)
				{
					return (ButtonEvent)(currentlyPressed + BUTTON_ONHOLD_MODIFIER);
				}
				else if (holdTimer > BUTTONS_HOLD_CYCLES)
				{
					return (ButtonEvent)(currentlyPressed + BUTTON_RAPID_MODIFIER);
				}
				else
				{
					return BUTTON_NONE;
				}
			}
			else
			{
				lastPressed = BUTTON_MULTIPLE;
			}
			break;
		}
		
		return BUTTON_MULTIPLE;
	}
};

//////////////////////////////////////////////////////////////////////////
// Speaker
#define SPEAKER_TONELENGTH 1
class Speaker// : private Timer
{
	private:
	Timer speakerTimer;
	
	public:
	inline void InitSpeaker()
	{
		SET_PIN_OUTPUT(SPEAKER_DDR, SPEAKER_PIN);
		
		// Timer setup
		speakerTimer.Init();
		speakerTimer.SetCounterTop(SPEAKER_TONELENGTH);
		speakerTimer.OnTimerElapsed = [](){BIT_LOW(SPEAKER_PORT, SPEAKER_PIN);};
	}
	void PlayContinous()
	{
		speakerTimer.Stop();
		BIT_HIGH(SPEAKER_PORT, SPEAKER_PIN);
	}
	void PlayTone()
	{
		// Enable speaker and start timer that will disable it after some time
		PlayContinous();
		speakerTimer.ResetCounter();
	}
	inline void StopPlaying()
	{
		speakerTimer.Stop();
		BIT_LOW(SPEAKER_PORT, SPEAKER_PIN);
	}
	inline void TimerTick()
	{
		speakerTimer.Tick();
	}
	inline uint8_t IsPlayingTone () const
	{
		return speakerTimer.IsRunning();
	}
	static inline uint8_t IsEnabled ()
	{
		return BIT_READ(SPEAKER_PORT, SPEAKER_PIN);
	}
};

#endif /* CLASSES_H_ */