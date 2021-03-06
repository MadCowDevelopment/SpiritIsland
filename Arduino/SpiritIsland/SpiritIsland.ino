const int PIN_SER = 2;
const int PIN_CLK = 3;
const int PIN_LATCH = 4;

const int PIN_ESCALATE = 5;

#include "ShiftRegisterPWM.h"
ShiftRegisterPWM sr(3, 128);

const int PIN_PHOTO = A0;
const int sensor1Pin = A1; 
const int sensor2Pin = A2;  


const int R1 = 7;
const int G1 = 6;
const int Blue1 = 5;
const int R2 = 4;
const int G2 = 3;
const int B2 = 2;
const int R3 = 1;
const int G3 = 0;

const int B3 = 15;
const int R4 = 14;
const int B4 = 13;
const int G4 = 12;
const int R5 = 11;
const int G5 = 10;
const int B5 = 9;
const int R6 = 8;

const int B6 = 23;
const int G6 = 22;
const int R7 = 21;
const int G7 = 20;
const int B7 = 19;
const int R8 = 18;
const int G8 = 17;
const int B8 = 16;

//int LEDs[24] = { R1, R2, R3, R4, R5, R6, R7, R8, G1, G2, G3, G4, G5, G6, G7, G8, Blue1, B2, B3, B4, B5, B6, B7, B8 };
int LEDs[24] = { 7,6,5,4,3,2,1,0, 15,14,13,12,11,10,9,8, 23,22,21,20,19,18,17,16 };
String lastLedState = "LED:0000000000000000000000000";

const int MAX_INTENSITY = 255 ;
const int MIN_INTENSITY = 12;
const int MS_UNTIL_DIM = 12000;
int intensity = MAX_INTENSITY;

bool connected = false;
bool started = false;

unsigned long lastInputTime;

void updateIntensity() {
  int poti = 1023;
  int intensityPercent = 100;
  
  if(millis() - lastInputTime > MS_UNTIL_DIM) {
    if(intensity > MIN_INTENSITY) intensity = MIN_INTENSITY * intensityPercent / 100;    
  } else intensity = MAX_INTENSITY * intensityPercent / 100;

  intensity = constrain(intensity, MIN_INTENSITY, MAX_INTENSITY);
}

void loop() {  
  handleRemoteInput();
  readSerialPort();
  readLightSensor();
  updateIntensity();
  if(started) setLEDsFromState(lastLedState);
  else cycleColors(500);

  delay(100);
}
