const int SER = 8;
const int LATCH = 9;
const int CLK = 10;

const int sensor1Pin = A0; 
const int sensor2Pin = A1;  

const int R1 = 1;
const int G1 = 2;
const int Blue1 = 4;
const int R2 = 8;
const int G2 = 16;
const int B2 = 32;
const int R3 = 64;
const int G3 = 128;

const int B3 = 1;
const int R4 = 2;
const int B4 = 4;
const int G4 = 8;
const int R5 = 16;
const int G5 = 32;
const int B5 = 64;
const int R6 = 128;

const int B6 = 1;
const int G6 = 2;
const int R7 = 4;
const int G7 = 8;
const int B7 = 16;
const int R8 = 32;
const int G8 = 64;
const int B8 = 128;

int value = 0;

bool connected;

void loop() {
  handleRemoteInput();
  readSerialPort();  
  delay(50);
}
