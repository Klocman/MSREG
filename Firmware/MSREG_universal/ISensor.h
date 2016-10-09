/* 
* ISensor.h
*
* Created: 20-09-14 6:26:52 PM
* Author: Klocman
*/


#ifndef __ISENSOR_H__
#define __ISENSOR_H__

struct SensorSettings
{
	int16_t TargetValue,
	int16_t Histeresis,
	int16_t AlarmOffset,
	
	int16_t TargetValue,
	int16_t Histeresis,
	int16_t AlarmOffset,
};

class ISensor
{
//functions
public:
	virtual ~ISensor(){}
	virtual void ReadSensor() = 0;
	virtual void DisplayValue() = 0;
	virtual void UpdateSensorEEPROM() = 0;
	virtual void ReadSensorEEPROM() = 0;
	virtual void SensorEEPROM_Setup() = 0;
	virtual void ActionSubtract() = 0;
	virtual void ActionAdd() = 0;
	virtual void TemperatureRegulation() = 0;
	virtual void HumidityRegulation() = 0;

	uint8_t settings[6];
}; //ISensor

#endif //__ISENSOR_H__
