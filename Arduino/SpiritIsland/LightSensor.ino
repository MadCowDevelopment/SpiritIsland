int currentIndex = 0;

int lastLightValues[10] = { 0,0,0,0,0,0,0,0,0,0 };

void initLightSensor() {  
  for(int i=0; i<10; i++) {
    lastLightValues[i] = analogRead(PIN_PHOTO);
  }
}
void readLightSensor() {
  int brightness = analogRead(PIN_PHOTO);
  
  int average = 0;
  for(int i=0; i<10; i++) {
    average += lastLightValues[i];
  }
  average /= 10;

  if(average - brightness > abs(20)) {
    lastInputTime = millis();
  }

  lastLightValues[currentIndex++] = brightness;  
  currentIndex = currentIndex % 10;
}
