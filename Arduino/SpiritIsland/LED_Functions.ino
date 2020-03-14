void turnAllOff() {
  for(int i=0; i<24; i++) sr.set(LEDs[i], 0);    
}

void turnAllRed() {
  for(int i=0; i<8; i++) sr.set(LEDs[i], 255);     
  for(int i=8; i<24; i++) sr.set(LEDs[i], 0);
}

void turnAllGreen() {
  for(int i=0; i<8; i++) sr.set(LEDs[i], 0);    
  for(int i=8; i<16; i++) sr.set(LEDs[i], 255);    
  for(int i=16; i<24; i++) sr.set(LEDs[i], 0);
}

void turnAllBlue() {
  for(int i=0; i<16; i++) sr.set(LEDs[i], 0);    
  for(int i=16; i<24; i++) sr.set(LEDs[i], 255);
}

void cycleColors(int delayMs) {
  turnAllRed();
  analogWrite(PIN_ESCALATE, 255);  
  delay(delayMs);

  turnAllGreen();
  analogWrite(PIN_ESCALATE, 0);  
  delay(delayMs);

  turnAllBlue();
  analogWrite(PIN_ESCALATE, 255);  
  delay(delayMs); 
}

void setLEDsFromState(String state) {  
  for(int i=4; i<28; i++) {
    sr.set(LEDs[i-4], intensity * charToDigit(state[i]));
  }

  if(charToDigit(state[28])) analogWrite(PIN_ESCALATE, intensity);  
  else analogWrite(PIN_ESCALATE, 0);
}

void cycleLands(int delayMs) {
  for(int i=0; i<24; i++) {
    if(i>0) sr.set(LEDs[i-1], 0);
    sr.set(LEDs[i], 255);  
    delay(delayMs);  
  }
  
  sr.set(LEDs[23], 0);
}
