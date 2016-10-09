/*
 * _7SEG.h
 *
 * Created: 20-09-14 1:56:52 PM
 *  Author: Klocman
 */ 


#ifndef SEVSEG_H_
#define SEVSEG_H_

#include <avr/io.h>

struct SevenSegmentOutput
{
	uint8_t digit1,digit2,digit3;
};

class SevenSegmentDisplay
{
	private:
	//void GetDigit (uint8_t* output, uint8_t digit, uint8_t addDp);
	static void WriteDigit(volatile uint8_t* output, uint8_t digit, const uint8_t addDp);
	volatile SevenSegmentOutput currentOutput;
	
	public:
	void SetupPins ();
	void WriteVariable (int16_t data, const uint8_t dpPos, const uint8_t replace0WithOff);
	void WriteError (uint8_t errorCode);
	void DrawOutput (const uint8_t digitNumber) const;
};


#endif /* SEVSEG_H_ */