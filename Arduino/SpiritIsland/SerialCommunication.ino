void readSerialPort() {
 
 if (Serial.available() > 0) {
   String str = Serial.readStringUntil('\n');

   if(str.startsWith("CONNECT")){
    connected = true;
   } 
   else if (str.startsWith("LED:")) {
    for(int i=4; i<28; i++) {
      sr.set(LEDs[i-4], intensity * charToDigit(str[i]));
    }

    escalate = charToDigit(str[28]);
   }
 }
}

int charToDigit(char character){
  return int(character) - 48;
}
