void readSerialPort() { 
  if (Serial.available() > 0) {
    lastInputTime = millis();
    String str = Serial.readStringUntil('\n');

    if(str.startsWith("CONNECT")){
      connected = true;
    } 
    else if (str.startsWith("LED:")) {
      lastLedState = str;    
    }
  }
}

int charToDigit(char character){
  return int(character) - 48;
}
