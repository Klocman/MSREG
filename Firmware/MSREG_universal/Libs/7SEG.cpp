#include "7SEG.h"
#include "../PinConfiguration.h"
#include "../KlocTools.h"
//#ifdef SEVSEG_INVERTED
//#define SEVSEG_INV_MODIFIER ~
//#else
//#define SEVSEG_INV_MODIFIER
//#endif

#define SEVSEGA 0
#define SEVSEGB 1
#define SEVSEGC 2
#define SEVSEGD 3
#define SEVSEGE 4
#define SEVSEGF 5
#define SEVSEGG 6
#define SEVSEGDP 7

#define sevSegDigits_COUNT 14

static const uint8_t sevSegDigits[sevSegDigits_COUNT] = {
	(0 | (1 << SEVSEGA) | (1 << SEVSEGB) | (1 << SEVSEGC) | (1 << SEVSEGD) | (1 << SEVSEGE) | (1 << SEVSEGF)), //0
	(0 | (1 << SEVSEGB) | (1 << SEVSEGC)), //1
	(0 | (1 << SEVSEGA) | (1 << SEVSEGB) | (1 << SEVSEGD) | (1 << SEVSEGE) | (1 << SEVSEGG)), //2
	(0 | (1 << SEVSEGA) | (1 << SEVSEGB) | (1 << SEVSEGC) | (1 << SEVSEGD) | (1 << SEVSEGG)), //3
	(0 | (1 << SEVSEGB) | (1 << SEVSEGC) | (1 << SEVSEGF) | (1 << SEVSEGG)), //4
	(0 | (1 << SEVSEGA) | (1 << SEVSEGC) | (1 << SEVSEGD) | (1 << SEVSEGF) | (1 << SEVSEGG)), //5
	(0 | (1 << SEVSEGA) | (1 << SEVSEGC) | (1 << SEVSEGD) | (1 << SEVSEGE) | (1 << SEVSEGF) | (1 << SEVSEGG)), //6
	(0 | (1 << SEVSEGA) | (1 << SEVSEGB) | (1 << SEVSEGC)), //7
	(0 | (1 << SEVSEGA) | (1 << SEVSEGB) | (1 << SEVSEGC) | (1 << SEVSEGD) | (1 << SEVSEGE) | (1 << SEVSEGF) | (1 << SEVSEGG)), //8
	(0 | (1 << SEVSEGA) | (1 << SEVSEGB) | (1 << SEVSEGC) | (1 << SEVSEGD) | (1 << SEVSEGF) | (1 << SEVSEGG)), //9
	(0), //Blank, 10
	(0 | (1 << SEVSEGG)), //- (bar), 11
	(0 | (1 << SEVSEGA) | (1 << SEVSEGE) | (1 << SEVSEGF) | (1 << SEVSEGG)), //F, 12
	(0 | (1 << SEVSEGA) | (1 << SEVSEGD) | (1 << SEVSEGE) | (1 << SEVSEGF) | (1 << SEVSEGG)), //E, 13
	
	//SEVSEG_INV_MODIFIER(0 | (1 << SEVSEGA) | (1 << SEVSEGB) | (1 << SEVSEGC) | (1 << SEVSEGD) | (1 << SEVSEGE) | (1 << SEVSEGF) | (1 << SEVSEGG)) //Placeholder
};

void SevenSegmentDisplay::WriteDigit(volatile uint8_t* output, uint8_t digit, const uint8_t addDp)
{
	if (digit >= sevSegDigits_COUNT)
	digit = sevSegDigits_COUNT - 1;
	
	*output = sevSegDigits[digit];
	
	if (addDp)
	{
		*output |= (1 << SEVSEGDP);
	}
}

void SevenSegmentDisplay::SetupPins()
{
	SET_PIN_OUTPUT(SEVSEG_A_DDR, SEVSEG_A_PIN);
	SET_PIN_OUTPUT(SEVSEG_B_DDR, SEVSEG_B_PIN);
	SET_PIN_OUTPUT(SEVSEG_C_DDR, SEVSEG_C_PIN);
	SET_PIN_OUTPUT(SEVSEG_D_DDR, SEVSEG_D_PIN);
	SET_PIN_OUTPUT(SEVSEG_E_DDR, SEVSEG_E_PIN);
	SET_PIN_OUTPUT(SEVSEG_F_DDR, SEVSEG_F_PIN);
	SET_PIN_OUTPUT(SEVSEG_G_DDR, SEVSEG_G_PIN);
	SET_PIN_OUTPUT(SEVSEG_DP_DDR, SEVSEG_DP_PIN);
	
	//currentOutput = {0,0,0}; // blank screen
	currentOutput.digit1 = sevSegDigits[11]; // "-" sign
	currentOutput.digit2 = sevSegDigits[11];
	currentOutput.digit3 = sevSegDigits[11];
}

/*
void SevenSegmentDisplay::WriteVariable(int16_t data, uint8_t dpPos)
Max 3 digits on left of dpPos (123.45)
Highest dpPos is 2 (1.23), lowest is 0 (123.)
*/
void SevenSegmentDisplay::WriteVariable(int16_t data, uint8_t dpPos, const uint8_t replace0WithOff)
{
	if (data < 0)
	{
		// Handle negative value
		data = -data;
		while(1)
		{
			if (dpPos == 0 && data >= 100)
			{
				// Overload
				WriteDigit(&currentOutput.digit1, 11, 0);
				WriteDigit(&currentOutput.digit2, 9, 0);
				WriteDigit(&currentOutput.digit3, 9, 0);
				return;
			}
			else if (data < 100)
			{
				WriteDigit(&currentOutput.digit3, data%10, dpPos == 0);
				data /= 10;
				WriteDigit(&currentOutput.digit2, data%10, dpPos == 1);
				WriteDigit(&currentOutput.digit1, 11, 0); // "-" sign
				return;
			}
			else
			{
				data /= 10;
				dpPos--;
			}
		}
	}
	else if (!replace0WithOff || data > 0)
	{
		while(1)
		{
			if (dpPos == 0 && data >= 1000)
			{
				// Overload
				WriteDigit(&currentOutput.digit1, 9, 0);
				WriteDigit(&currentOutput.digit2, 9, 0);
				WriteDigit(&currentOutput.digit3, 9, 0);
				return;
			}
			else if (data < 1000)
			{
				WriteDigit(&currentOutput.digit3, data%10, dpPos == 0);
				data /= 10;
				WriteDigit(&currentOutput.digit2, data%10, dpPos == 1);
				data /= 10;
				WriteDigit(&currentOutput.digit1, data%10, dpPos == 2);
				return;
			}
			else
			{
				data /= 10;
				dpPos--;
			}
		}
	}
	else
	{
		// Data equals 0 and we need to replace it with "OFF"
		WriteDigit(&currentOutput.digit1, 0, 0);
		WriteDigit(&currentOutput.digit2, 12, 0);
		WriteDigit(&currentOutput.digit3, 12, 0);
	}
}

// Error code should be below 10
void SevenSegmentDisplay::WriteError(uint8_t errorCode)
{
	WriteDigit(&currentOutput.digit1, 13, 0);
	WriteDigit(&currentOutput.digit2, 10, 0);
	WriteDigit(&currentOutput.digit3, errorCode, 0);
}

//#define SEVSEG_DrawOutput_SETBIT(a,b,c,d) if (BIT_READ(a, b)) BIT_HIGH(c, d); else BIT_LOW(c, d)
#define SEVSEG_DrawOutput_SETBIT(a,b,c,d) if (BIT_READ(a, b)) BIT_LOW(c, d); else BIT_HIGH(c, d)
// digitNumber maps 0 to leftmost digit and >= 2 to the rightmost one
void SevenSegmentDisplay::DrawOutput(const uint8_t digitNumber) const
{
	const volatile uint8_t *data;
	switch(digitNumber)
	{
		case 0:
		data = &currentOutput.digit1;
		break;
		
		case 1:
		data = &currentOutput.digit2;
		break;
		
		default:
		case 2:
		data = &currentOutput.digit3;
		break;
	}
	//uint8_t* a1[];
	//uint8_t a2[];
	/*
	SEVSEG_DrawOutput_SETBIT(*data, SEVSEGA, SEVSEG_A_DDR, SEVSEG_A_PIN);
	SEVSEG_DrawOutput_SETBIT(*data, SEVSEGB, SEVSEG_B_DDR, SEVSEG_B_PIN);
	SEVSEG_DrawOutput_SETBIT(*data, SEVSEGC, SEVSEG_C_DDR, SEVSEG_C_PIN);
	SEVSEG_DrawOutput_SETBIT(*data, SEVSEGD, SEVSEG_D_DDR, SEVSEG_D_PIN);
	SEVSEG_DrawOutput_SETBIT(*data, SEVSEGE, SEVSEG_E_DDR, SEVSEG_E_PIN);
	SEVSEG_DrawOutput_SETBIT(*data, SEVSEGF, SEVSEG_F_DDR, SEVSEG_F_PIN);
	SEVSEG_DrawOutput_SETBIT(*data, SEVSEGG, SEVSEG_G_DDR, SEVSEG_G_PIN);
	SEVSEG_DrawOutput_SETBIT(*data, SEVSEGDP, SEVSEG_DP_DDR, SEVSEG_DP_PIN);
	*/
	SEVSEG_DrawOutput_SETBIT(*data, SEVSEGA, SEVSEG_A_PORT, SEVSEG_A_PIN);
	SEVSEG_DrawOutput_SETBIT(*data, SEVSEGB, SEVSEG_B_PORT, SEVSEG_B_PIN);
	SEVSEG_DrawOutput_SETBIT(*data, SEVSEGC, SEVSEG_C_PORT, SEVSEG_C_PIN);
	SEVSEG_DrawOutput_SETBIT(*data, SEVSEGD, SEVSEG_D_PORT, SEVSEG_D_PIN);
	SEVSEG_DrawOutput_SETBIT(*data, SEVSEGE, SEVSEG_E_PORT, SEVSEG_E_PIN);
	SEVSEG_DrawOutput_SETBIT(*data, SEVSEGF, SEVSEG_F_PORT, SEVSEG_F_PIN);
	SEVSEG_DrawOutput_SETBIT(*data, SEVSEGG, SEVSEG_G_PORT, SEVSEG_G_PIN);
	SEVSEG_DrawOutput_SETBIT(*data, SEVSEGDP, SEVSEG_DP_PORT, SEVSEG_DP_PIN);
}
