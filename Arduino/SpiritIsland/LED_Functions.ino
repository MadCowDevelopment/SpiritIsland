void turnAllOff() {
  digitalWrite(LATCH, LOW);
  shiftOut(SER, CLK, MSBFIRST, 0);
  shiftOut(SER, CLK, MSBFIRST, 0);
  shiftOut(SER, CLK, MSBFIRST, 0);
  digitalWrite(LATCH, HIGH);  
}

void turnAllRed() {
  digitalWrite(LATCH, LOW);
  shiftOut(SER, CLK, MSBFIRST, R7 + R8);
  shiftOut(SER, CLK, MSBFIRST, R4 + R5 + R6);
  shiftOut(SER, CLK, MSBFIRST, R1 + R2 + R3);
  digitalWrite(LATCH, HIGH);
}

void turnAllBlue() {
  digitalWrite(LATCH, LOW);
  shiftOut(SER, CLK, MSBFIRST, B6 + B7 + B8);
  shiftOut(SER, CLK, MSBFIRST, B3 + B4 + B5);
  shiftOut(SER, CLK, MSBFIRST, Blue1 + B2);
  digitalWrite(LATCH, HIGH);
}

void turnAllGreen() {
  digitalWrite(LATCH, LOW);
  shiftOut(SER, CLK, MSBFIRST, G6 + G7 + G8);
  shiftOut(SER, CLK, MSBFIRST, G4 + G5);
  shiftOut(SER, CLK, MSBFIRST, G1 + G2 + G3);
  digitalWrite(LATCH, HIGH);
}

void turnOnLeds(int register1, int register2, int register3) {
  digitalWrite(LATCH, LOW);
  shiftOut(SER, CLK, MSBFIRST, register3);
  shiftOut(SER, CLK, MSBFIRST, register2);
  shiftOut(SER, CLK, MSBFIRST, register1);
  digitalWrite(LATCH, HIGH);
}
void writeEscalate() {
  if(escalate) {
    analogWrite(ESCALATE, 255);  
  }
  else {
    analogWrite(ESCALATE, 0);  
  }
}

void cycleColors(int delayMs) {
  turnAllRed();
  analogWrite(ESCALATE, 255);  
  delay(delayMs);

  turnAllGreen();
  analogWrite(ESCALATE, 0);  
  delay(delayMs);

  turnAllBlue();
  analogWrite(ESCALATE, 255);  
  delay(delayMs);   
}

void cycleLands() {
  int seq[8] = { 1,2,4,8,16,32,64,128 };
  int delayMs = 200;
  for(int i=0; i<8; i++) {
    digitalWrite(LATCH, LOW);
    shiftOut(SER, CLK, MSBFIRST, 0);
    shiftOut(SER, CLK, MSBFIRST, 0);
    shiftOut(SER, CLK, MSBFIRST, seq[i]);
    digitalWrite(LATCH, HIGH);
    delay(delayMs);
  }
      
  for(int i=0; i<8; i++) {
    digitalWrite(LATCH, LOW);
    shiftOut(SER, CLK, MSBFIRST, 0);
    shiftOut(SER, CLK, MSBFIRST, seq[i]);
    shiftOut(SER, CLK, MSBFIRST, 0);
    digitalWrite(LATCH, HIGH);
    delay(delayMs);
  }
      
  for(int i=0; i<8; i++) {
    digitalWrite(LATCH, LOW);
    shiftOut(SER, CLK, MSBFIRST, seq[i]);
    shiftOut(SER, CLK, MSBFIRST, 0);
    shiftOut(SER, CLK, MSBFIRST, 0);
    digitalWrite(LATCH, HIGH);
    delay(delayMs);
  }  
}
