/*
 * RollingAverage.h
 *
 * Created: 3/26/2014 9:47:11 AM
 *  Author: Administrator
 */ 


#ifndef ROLLINGAVERAGE_H_
#define ROLLINGAVERAGE_H_

#include <avr/io.h>
#include "../Libs/DHT22.h"
#include "../KlocTools.h"

#define ROLLINGAVERAGE_COUNT 3

class RollingAverage 
{
	private:
	DHT22result store[ROLLINGAVERAGE_COUNT];
	DHT22result sum;
	uint8_t index;//, jumpCount;
	
	public:
	inline RollingAverage() : sum({0,0}), index(0)
	{
		//index = 0;
		//sum = {0,0};
		for(uint8_t i=0;i<ROLLINGAVERAGE_COUNT;i++)
		{
			store[index] = {0,0};
		}
	}
	inline void ProcessValue (DHT22result *input)
	{
		index++;
		if (index >= ROLLINGAVERAGE_COUNT) index = 0;
		
		sum.temperature = (sum.temperature - store[index].temperature) + input->temperature;
		sum.humidity = (sum.humidity - store[index].humidity) + input->humidity;
		
		store[index].temperature = input->temperature;
		store[index].humidity = input->humidity;
		
		input->temperature = sum.temperature / ROLLINGAVERAGE_COUNT;
		input->humidity = sum.humidity / ROLLINGAVERAGE_COUNT;
	}
	
	//void FillValues (int16_t target);
	//inline void Init (){index = ROLLINGAVERAGE_COUNT;}
	//void AddValue (DHT22result input);
	//DHT22result GetValue () const;
};

#endif /* ROLLINGAVERAGE_H_ */