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
    sr.set(LEDs[i], 128);  
    delay(delayMs);  
  }
  
  sr.set(LEDs[23], 0);
}


int charToDigit(char character){
  return int(character) - 48;
}

void rainbow() {
  analogWrite(PIN_ESCALATE, 255);  
  unsigned int rgbColour[3];

  // Start off with red.
  rgbColour[0] = 96;
  rgbColour[1] = 0;
  rgbColour[2] = 0;  

  // Choose the colours to increment and decrement.
  for (int decColour = 0; decColour < 3; decColour += 1) {
    int incColour = decColour == 2 ? 0 : decColour + 1;

    // cross-fade the two colours.
    for(int i = 0; i < 96; i += 1) {
      rgbColour[decColour] -= 1;
      rgbColour[incColour] += 1;

      for(int k = 0; k<8; k++) setColourRgb(k, rgbColour[0], rgbColour[1], rgbColour[2]);
      delay(1);
    }
  }
}

void setColourRgb(unsigned int i, unsigned int red, unsigned int green, unsigned int blue) {
  sr.set(LEDs[i], red);
  sr.set(LEDs[i+8], green);
  sr.set(LEDs[i+16], blue);
 }
