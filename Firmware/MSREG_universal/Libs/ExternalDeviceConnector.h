/*
 * ExternalDeviceConnector.h
 *
 * Created: 28-10-14 6:12:02 PM
 *  Author: Klocman
 */ 


#ifndef EXTERNALDEVICECONNECTOR_H_
#define EXTERNALDEVICECONNECTOR_H_

#include <avr/io.h>
#include <avr/pgmspace.h>
#include "../BitTools.h"

extern "C" {
	#include "uart.h"
}

#define USART_BAUDRATE 4800
//#define UART_NO_DATA          0x0100              /* no receive data available   */

enum ExternalDeviceConnectorMode
{
	ExternalDevice_Dumb = 0,
	ExternalDevice_RS232
};

class ExternalDeviceConnector
{
	public:
	static inline void SetMode (const ExternalDeviceConnectorMode mode)
	{
		switch(mode)
		{
			default:
			case ExternalDevice_Dumb:
			BIT_LOW(UCSRB, TXEN);
			BIT_LOW(PORTD, PIND1);
			SET_PIN_OUTPUT(DDRD, PIND1);
			break;
			
			case ExternalDevice_RS232:
			BIT_LOW(PORTD, PIND1);
			BIT_HIGH(UCSRB, TXEN);
			break;
		}
	}
	
	static inline void SetDumbAlarm (const uint8_t e)
	{
		if (!BIT_READ(UCSRB, TXEN))
		{
			if (e)
			{
				BIT_HIGH(PORTD, PIND1);
			}
			else
			{
				BIT_LOW(PORTD, PIND1);
			}
		}
	}
	
	static inline void Init ()
	{
		uart_init(UART_BAUD_SELECT(USART_BAUDRATE, F_CPU));
		SetMode(ExternalDevice_Dumb);
	}
	
	static inline unsigned int uGetc(void)
	{
		return uart_getc();
	}
	
	static inline void uPutc(unsigned char data)
	{
		if (BIT_READ(UCSRB, TXEN))
		{
			uart_putc(data);
		}
	}
	
	static inline void uPuts(const char *s )
	{
		if (BIT_READ(UCSRB, TXEN))
		{
			uart_puts(s);
		}
	}
	
	static inline void uPuts_p(const char *s )
	{
		if (BIT_READ(UCSRB, TXEN))
		{
			uart_puts_p(s);
		}
	}
	
	static inline void uFlush(void)
	{
		uart_flush();
	}
};

#endif /* EXTERNALDEVICECONNECTOR_H_ */