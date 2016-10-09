/*
 * AutoBaud.h
 *
 * Created: 2014-11-23 15:41:31
 *  Author: Klocman
 */ 


#ifndef AUTOBAUD_H_
#define AUTOBAUD_H_

#include <avr/io.h>
#include <util/delay_basic.h>

// Calibrate baud rate if host is sending space (' ', 0b00100000) char
static inline uint8_t CalibrateUartBaud()
{
	uint8_t readByte;
	for (uint8_t tries = 0; tries < 10; tries++)
	{
		_delay_loop_2(0); // Max delay - 32,77ms
		
		if (UCSRA & _BV(RXC))
		{
			readByte = UDR;
			
			if ((readByte & 0b00111111) == 0b00100000 || (readByte & 0b00111111) == 0)
			{
				//tries = 0;
				switch(readByte)
				{
					case 0b00100000:
					// The space char is clear
					return 1;
					
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
			}
			
			readByte = UDR; // Clear the data register after changing the baudrate
			_delay_loop_1(200);
			readByte = UDR; // Make sure there was no ongoing transfer while clearing the register
		}
	}
	return 0;
}

#endif /* AUTOBAUD_H_ */