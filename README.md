![Finished regulator, and a partially assembled board](/Documentation/Photos/banner.jpg?raw=true "Finished regulator, and a partially assembled board")
## MSREG (Most recently MSREG33E)
Temperature and (optionally) humidity regulator based on DS18B20 or DHT22 sensors. This project contains the board layout, firmware, bootloader, and Windows tools that can be used with the regulator to monitor, configure, and update it. 

This regulator can be used for terrariums, heaters, egg incubators, refrigerators - anything that depends on stable temperature and/or humidity, and can be controlled by turning something On or Off.

## Features and technical specifications
[Read the datasheet (only in Polish at the moment)](https://github.com/Klocman/MSREG/blob/master/Documentation/datasheet_pl_msreg33E_r7.pdf)

## Compiling / Software required
Boards and schematics were created with Eagle PCB v6.6.0. 

Firmare and Bootloader can be opened and compiled with Atmel Studio 6 or newer. The bootloader .hex has to be manually merged with the main firmware .hex.

The Windows applications can be compiled in any modern version of Visual Studio with C# support.

## Photos and videos of the regulator
[Youtube video of MSREG33E working with the Windows application](https://youtu.be/d277WWdbOqA?t=52s)

[You can view the full gallery here](https://github.com/Klocman/MSREG/tree/master/Documentation/Photos)

![Fully assembled and turned on](/Documentation/Photos/working2.jpg?raw=true "Fully assembled and turned on")
![Fully assembled with the back removed](/Documentation/Photos/board_assembled_cased.jpg?raw=true "Fully assembled with the back removed")
![Front of a partially assembled board](/Documentation/Photos/board_new3.jpg?raw=true "Front of a partially assembled board")
![Back of a partially assembled board](/Documentation/Photos/board_new4.jpg?raw=true "Back of a partially assembled board")