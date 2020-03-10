
void readSerialPort() {
 int register1 = 0;
 int register2 = 0;
 int register3 = 0;

 if (Serial.available() < 10) {
   connected = true;
 }
 else if (Serial.available() == 25) {   
    register1 += R1 * (int(Serial.read()) - 48);
    register1 += R2 * (int(Serial.read()) - 48);
    register1 += R3 * (int(Serial.read()) - 48);
    register2 += R4 * (int(Serial.read()) - 48);
    register2 += R5 * (int(Serial.read()) - 48);
    register2 += R6 * (int(Serial.read()) - 48);
    register3 += R7 * (int(Serial.read()) - 48);
    register3 += R8 * (int(Serial.read()) - 48);
    register1 += G1 * (int(Serial.read()) - 48);
    register1 += G2 * (int(Serial.read()) - 48);
    register1 += G3 * (int(Serial.read()) - 48);
    register2 += G4 * (int(Serial.read()) - 48);
    register2 += G5 * (int(Serial.read()) - 48);
    register3 += G6 * (int(Serial.read()) - 48);
    register3 += G7 * (int(Serial.read()) - 48);
    register3 += G8 * (int(Serial.read()) - 48);
    register1 += Blue1 * (int(Serial.read()) - 48);
    register1 += B2 * (int(Serial.read()) - 48);
    register2 += B3 * (int(Serial.read()) - 48);
    register2 += B4 * (int(Serial.read()) - 48);
    register2 += B5 * (int(Serial.read()) - 48);
    register3 += B6 * (int(Serial.read()) - 48);
    register3 += B7 * (int(Serial.read()) - 48);
    register3 += B8 * (int(Serial.read()) - 48);

    int escalation = 255 * (int(Serial.read()) - 48);
    
    turnOnLeds(register1, register2, register3);
  }
}
