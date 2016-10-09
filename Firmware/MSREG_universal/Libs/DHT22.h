/*
 * DHT22.h
 *
 * Created: 22-09-14 1:25:48 PM
 *  Author: Klocman
 */ 


#ifndef DHT22_H_
#define DHT22_H_

#include "../KlocTools.h"
#include "../PinConfiguration.h"

#define DHT22_RESULT_DECIMALPOINT_PLACE 1
#define DHT22_TEMP_MAX 1200
#define DHT22_TEMP_MIN -350
#define DHT22_TEMP_HIST_MAX 500
#define DHT22_HUM_MAX 1000
#define DHT22_HUM_MIN 0
#define DHT22_HUM_HIST_MAX 500

struct DHT22result
{
	int16_t temperature;
	// Humidity should always be >= 0 and <= 100
	int16_t humidity;
};

// After a successful read wait this much time before attempting to read again (12.3 format, uint8)
#define DHT22_REFRESHTIME 6
#define DHT22_MAXERRORS 20

class DHT22sensor
{
	private:
	uint8_t errorCount;
	
	//uint8_t jumpCount;
	
	inline SensorResult ProcessResult (SensorResult input)
	{
		switch(input)
		{
			case SensorOK:
			errorCount = 0;
			return SensorOK;
			
			default:
			if(errorCount >= DHT22_MAXERRORS)
			{
				return input;
			}
			else
			{
				errorCount++;
				return SensorBusy;
			}
		}
	}
	public:
	SensorResult ReadSensor (DHT22result* result);
	inline void Init ()
	{
		errorCount = 0;
	}
};

#endif /* DHT22_H_ */