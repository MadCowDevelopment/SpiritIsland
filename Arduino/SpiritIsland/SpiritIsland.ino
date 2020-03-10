const int SER = 8;
const int LATCH = 9;
const int CLK = 10;

int sensor1Pin = A0; 
int sensor2Pin = A1;  

int seq[8] = { 1,2,4,8,16,32,64,128 };

int R1 = 1;
int G1 = 2;
int Blue1 = 4;
int R2 = 8;
int G2 = 16;
int B2 = 32;
int R3 = 64;
int G3 = 128;

int B3 = 1;
int R4 = 2;
int B4 = 4;
int G4 = 8;
int R5 = 16;
int G5 = 32;
int B5 = 64;
int R6 = 128;

int B6 = 1;
int G6 = 2;
int R7 = 4;
int G7 = 8;
int B7 = 16;
int R8 = 32;
int G8 = 64;
int B8 = 128;

int value = 0;
int sensor1Value = 0;
int sensor2Value = 0; 

bool connected;

void loop() {
  sensor1Value = analogRead(sensor1Pin);
  sensor2Value = analogRead(sensor2Pin);

  if(sensor1Value < 180) {
    topButtonPressed();
  }
  else if(sensor1Value > 900) {
    bottomButtonPressed();
  }
  else middleButtonPressed();

  readSerialPort();
  
  delay(50);
}
