void readSerialPort() {
 
 if (Serial.available() > 0) {
   String str = Serial.readString();

   if(str.startsWith("CONNECT")){
    connected = true;
   } else if (str.startsWith("LED:")) {
    int register1 = 0;
    int register2 = 0;
    int register3 = 0;

    register1 += R1 * charToDigit(str[4]);
    register1 += R2 * charToDigit(str[5]);
    register1 += R3 * charToDigit(str[6]);
    register2 += R4 * charToDigit(str[7]);
    register2 += R5 * charToDigit(str[8]);
    register2 += R6 * charToDigit(str[9]);
    register3 += R7 * charToDigit(str[10]);
    register3 += R8 * charToDigit(str[11]);
    register1 += G1 * charToDigit(str[12]);
    register1 += G2 * charToDigit(str[13]);
    register1 += G3 * charToDigit(str[14]);
    register2 += G4 * charToDigit(str[15]);
    register2 += G5 * charToDigit(str[16]);
    register3 += G6 * charToDigit(str[17]);
    register3 += G7 * charToDigit(str[18]);
    register3 += G8 * charToDigit(str[19]);
    register1 += Blue1 * charToDigit(str[20]);
    register1 += B2 * charToDigit(str[21]);
    register2 += B3 * charToDigit(str[22]);
    register2 += B4 * charToDigit(str[23]);
    register2 += B5 * charToDigit(str[24]);
    register3 += B6 * charToDigit(str[25]);
    register3 += B7 * charToDigit(str[26]);
    register3 += B8 * charToDigit(str[27]);

    int escalation = 255 * charToDigit(str[28]);
    
    turnOnLeds(register1, register2, register3);
   }
 }
}

int charToDigit(char character){
  return int(character) - 48;
}
