/*
 * BitTools.h
 *
 * Created: 2014-11-11 14:26:46
 *  Author: Klocman
 */ 


#ifndef BITTOOLS_H_
#define BITTOOLS_H_


#define BIT_LOW(port,pin) (port) &= ~(1<<pin)
#define BIT_HIGH(port,pin) (port) |= (1<<pin)
#define BIT_TOGGLE(port,pin) (port) ^= (1<<pin)
#define BIT_READ(portpin,pin) ((portpin) & (1<<pin))
#define SET_PIN_INPUT(ddr,pin) (ddr) &= ~(1<<pin)
#define SET_PIN_OUTPUT(ddr,pin) (ddr) |= (1<<pin)


#endif /* BITTOOLS_H_ */