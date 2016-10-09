#include "DHT22.h"
#include <util/delay.h>
#include <util/atomic.h>

SensorResult DHT22sensor::ReadSensor(DHT22result* result)
{
	SensorResult resultState = SensorOK;
	
	uint8_t inbyte;
	uint8_t buf[5];
	uint8_t to_cnt;
	
	// Pull the line low to start a transfer
	BIT_LOW(SENSOR_PORT, SENSOR_PIN); // set low before going output (open collector line safety)
	SET_PIN_OUTPUT(SENSOR_DDR, SENSOR_PIN);
	_delay_us(700);     // data sheet: at least 500us
	
	// After this point the transfer is very time-sensitive for the most part.
	ATOMIC_BLOCK(ATOMIC_FORCEON)
	{
		// Pull the line high and wait for sensor to reply
		SET_PIN_INPUT(SENSOR_DDR, SENSOR_PIN);
		BIT_HIGH(SENSOR_PORT, SENSOR_PIN);
		
		// go to the middle of the expected low (data sheet: 20-40 high, 80 low, read on 30+80/2, we are checking 40us before sensor pulls up)
		_delay_us(70);
		if(BIT_READ(SENSOR_PORT_IN, SENSOR_PIN))
		{
			// no sensor
			resultState = SensorFailedStep1;
			break;
		}
		
		// Turn off the pull-up and let sensor control the bus
		BIT_LOW(SENSOR_PORT, SENSOR_PIN);
		
		// Go to the middle of the expected high and check (40us later sensor pulls up for 80us, so 40 + 80/2 = 80)
		// Subtract a few cycles because of the code in between. 8 instructions = 1us.
		_delay_us(78); //70
		if(!BIT_READ(SENSOR_PORT_IN, SENSOR_PIN))
		{
			// bad sensor or noise?
			resultState = SensorFailedStep2;
			break;
		}
	
		for (uint8_t b = 0; b < 5; b++)      // 5 bytes
		{
			inbyte = 0;

			for (uint8_t i = 0; i < 8; i++)   // 8 bits
			{
				// Wait for data line to go low to begin transmission of next byte. Non time-critical, so enable interrupts to refresh the display.
				NONATOMIC_BLOCK(NONATOMIC_FORCEOFF)
				{
					to_cnt = 0;
					while(BIT_READ(SENSOR_PORT_IN, SENSOR_PIN))
					{
						_delay_us(2);
						if (to_cnt++ > 25)
						{
							resultState = SensorFailedStep3;
							break;
						}
					}
				}
				// Additional delay since falling edge can be slow on long cables
				_delay_us(4);

				// Wait for bus to go high, could enable interrupts here as well, but interrupts firing right at the end could add significant errors.
				to_cnt = 0;
				while(!BIT_READ(SENSOR_PORT_IN, SENSOR_PIN))//!is_high(pin))
				{
					_delay_us(2);
					if (to_cnt++ > 32)
					{
						resultState = SensorFailedStep4;
						break;
					}
				}
			
				// this is the problematic rising edge
				// data sheet: duration defines bit: 26-28us for 0, 70 for 1, so read at 27 + (70-27)/2 = 48.5
				// Delay from previous code and possibly _delay_us is around 3us, so probe at around 45.5
				// Worst case at 10% clock drift and longest/shortest code path, delay drifts between around -4.5 and +5;
				_delay_us(38); // Slower value is needed (than 45), because it takes too long to get to the next byte.
				
				inbyte <<= 1;
				if(BIT_READ(SENSOR_PORT_IN, SENSOR_PIN))
					inbyte |= 1;
			}
			buf[b] = inbyte;
		}
	
	}
	
	if (resultState == SensorOK)
	{
		// Check data with the checksum, check resulting data for sanity as an extra step
		uint8_t sum = buf[0] + buf[1] + buf[2] + buf[3];
		int16_t tempHum = (buf[0] << 8) | buf[1];   // Combine high and low bytes
	
		if((buf[4] != sum) || (tempHum > 1000 || tempHum < 0))
		{
			resultState = SensorFailedStep5;
		}
		else
		{
			int16_t tempTemp = ((buf[2] & 0b01111111) << 8) | buf[3];   // remove the sign bit and combine
			// Change sign of the result if necessary
			if(buf[2] & 0b10000000)
			{
				tempTemp *= -1;
			}
			/*
			// Simple filtering - filter out single spikes.
			int16_t difference = tempHum - result->humidity;
			if (difference > 10 || difference < 10)
			{
				if (jumpCount < 3)
				{
					jumpCount += 1;
					return SensorBusy;
				}
			}
			else
			{
				jumpCount = 0;
			}*/

			result->temperature = tempTemp;
			result->humidity = tempHum;
		}
	}

	return ProcessResult(resultState);
}

