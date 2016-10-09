#pragma once

#include <avr/io.h>
#include <avr/pgmspace.h>
#include <stdio.h>
#include "BitTools.h"
#include "./Libs/ExternalDeviceConnector.h"

typedef void (*Action) (void);

/*
struct SensorValue
{
	int16_t val;
	uint8_t decPointLocation;
};
*/
enum SensorResult
{
    SensorOK = 0,
    SensorBusy = 1,
    SensorCriticalFailed = 2,
    SensorFailedStep1 = 5,
    SensorFailedStep2,
    SensorFailedStep3,
    SensorFailedStep4,
    SensorFailedStep5
};

typedef enum
{
    Result_DoNothing = 0,
    Result_Disable = 1,
    Result_AboveSetting = 2,
    Result_AboveSettingAlarm = 3,
    Result_BelowSetting = 4,
    Result_BelowSettingAlarm = 5,
    Result_WaitForTimer = 9
} RegulationResult;

enum SettingChangeResult
{
    SettingChange_OK = 0,
    SettingChange_TooLow,
    SettingChange_TooHigh
};

class SingleSetting
{
private:
    int16_t min, max;

public:
    int16_t variable;

    void Init (const int16_t minVal, const int16_t maxVal)
    {
        min = minVal;
        max = maxVal;
        //SetValueSafe(0);
    }

    inline SettingChangeResult SetValue (const int16_t newVariable)
    {
        if (newVariable > max)
        {
            return SettingChange_TooHigh;
        }

        if (newVariable < min)
        {
            return SettingChange_TooLow;
        }

        variable = newVariable;
        return SettingChange_OK;
    }
    /*
    void SetValueSafe (int16_t newVariable)
    {
    	if (newVariable > max)
    	{
    		variable = max;
    	}
    	else
    	{
    		if (newVariable < min)
    		{
    			variable = min;
    		}
    		else
    		{
    			variable = newVariable;
    		}
    	}
    }*/

    inline void Add1 ()
    {
        if (variable < max)
        {
            variable += 1;
        }
        else
        {
            variable = max;
        }
    }

    inline void Sub1 ()
    {
        if (variable > min)
        {
            variable -= 1;
        }
        else
        {
            variable = min;
        }
    }
};
/*
struct RegulationSettingsExtract
{
	int16_t targetValue;
	int16_t histeresis;
	int16_t alarmOffset;
	int16_t waitTime;
	int16_t normallyClosed;

	RegulationSettingsExtract ()
	{
	}

	RegulationSettingsExtract (int16_t target,int16_t hist,int16_t alarm,int16_t wait,int16_t nc)
	{
		targetValue = target;
		histeresis = hist;
		alarmOffset = alarm;
		waitTime = wait;
		normallyClosed = nc;
	}

	// Returns 0 if not equal, 1 if equal
	uint8_t Equals (RegulationSettingsExtract *other )
	{
		if (other->alarmOffset != alarmOffset
		|| other->histeresis != histeresis
		|| other->targetValue != targetValue
		|| other->waitTime != waitTime
		|| other->normallyClosed != normallyClosed)
		{
			return 0;
		}
		return 1;
	}
};*/

class RegulationSettings
{
public:
    SingleSetting targetValue;
    // Must be a positive value, 0 for disabled regulation
    SingleSetting histeresis;
    // Must be a positive value, 0 for disabled alarm
    SingleSetting alarmOffset;
    // Max 30 seconds, 0 to disable wait time. Remember to call OnWaitTimeChanged when done editing to update timers.
    SingleSetting waitTime;
    // 0 when in NO mode, 1 when NC
    SingleSetting normallyClosed;

    void Init (const int16_t targetMin, const int16_t targetMax, const int16_t histMax, const int16_t waitMax)
    {
        targetValue.Init(targetMin, targetMax);
        histeresis.Init(0, histMax);
        alarmOffset.Init(0,histMax);
        waitTime.Init(0, waitMax);
        normallyClosed.Init(0,1);
    }
    /*
    RegulationSettingsExtract GetExtract ()
    {
    	return RegulationSettingsExtract(targetValue.variable,histeresis.variable,
    	alarmOffset.variable,waitTime.variable,normallyClosed.variable);
    }

    void ApplyExtract (RegulationSettingsExtract *target)
    {
    	targetValue.SetValueSafe(target->targetValue);
    	histeresis.SetValueSafe(target->histeresis);
    	alarmOffset.SetValueSafe(target->alarmOffset);
    	waitTime.SetValueSafe(target->waitTime);
    	normallyClosed.SetValueSafe(target->normallyClosed);
    }
    */
    void ToString (char *buffer)//, const size_t count)
    {
		int8_t targetRemainder = targetValue.variable % 10;
		if (targetRemainder < 0)
		{
			targetRemainder *= -1;
		}
		
        sprintf_P(buffer, PSTR("1%i.%i 2%u.%u 3%u.%u 4%u.%u 5%u\r\n"),
                  targetValue.variable / 10, targetRemainder,
                  histeresis.variable / 10, histeresis.variable % 10,
                  waitTime.variable / 10, waitTime.variable % 10,
                  alarmOffset.variable / 10, alarmOffset.variable % 10,
                  normallyClosed.variable);

        //target->uPuts(outBuffer);
        //uart_puts(outBuffer);
    }
};

void Clamp( uint8_t* input, const uint8_t min, const uint8_t max );

void Wrap( int8_t* input, const int8_t min, const int8_t max );

void WrapSub1( uint8_t *input, const uint8_t min, const uint8_t max );

void WrapAdd1( uint8_t *input, const uint8_t min, const uint8_t max );

void ClampSub1( uint8_t *input, const uint8_t min );

void ClampAdd1( uint8_t *input, const uint8_t max );

uint16_t ClampSubtract( const uint16_t a, const uint16_t b );

//uint8_t ClampDownTo8( const float in );

uint8_t Clamp10To8( const uint16_t in );
/*
inline uint8_t Difference8 ( const uint8_t *a, const uint8_t *b )
{
    if (*a > *b)
    {
        return *a - *b;
    }
    else
    {
        return *b - *a;
    }
}

inline uint16_t Difference16u ( const uint16_t *a, const uint16_t *b )
{
    if (*a > *b)
    {
        return *a - *b;
    }
    else
    {
        return *b - *a;
    }
}
inline uint16_t Difference16 ( const int16_t a, const int16_t b )
{
    if (a > b)
    {
        return (uint16_t)(a - b);
    }
    else
    {
        return (uint16_t)(b - a);
    }
}
inline uint8_t ConvertToPercentage (const uint8_t *val, const uint8_t *max)
{
    return ((uint16_t)(*val) * (uint16_t)100) / *max;
}

inline void Rescale8 (uint8_t *input, const uint8_t max)
{
    *input = ((uint16_t)(*input) * (uint16_t)max) >> 8;
}*/

// Max pow is 3, everything above defaults to pow 3 (1000)
inline int16_t PowerOf10 ( const uint8_t pow)
{
    switch(pow)
    {
    case 0:
        return 1;
    case 1:
        return 10;
    case 2:
        return 100;
    default:
        return 1000; // No need for more
        //default:
        //return 10000;
    }
}