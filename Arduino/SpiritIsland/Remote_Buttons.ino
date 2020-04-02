int TOP = 1;
int MIDDLE = 2;
int BOTTOM = 3;
int lastButton = 0;

int sensor1Value = 0;
int sensor2Value = 0; 

void handleRemoteInput() {
  sensor1Value = analogRead(sensor1Pin);
  sensor2Value = analogRead(sensor2Pin);

  if(sensor2Value > 900) topButtonPressed();
  else if(sensor1Value > 900) bottomButtonPressed();
  else middleButtonPressed();
}

void topButtonPressed() {  
  if(!started) return;
  if(lastButton == TOP) return;
  lastButton = TOP;  
  Serial.println("CMD:EXPLORE");
}

void middleButtonPressed() {
  if(!connected) return;
  if(started) return;
  if(lastButton == MIDDLE) return;
  lastButton = MIDDLE;
  Serial.println("CMD:START");
  started = true;  
}

void bottomButtonPressed() {
  if(!started) return;
  if(lastButton == BOTTOM) return;  
  lastButton = BOTTOM;  
  Serial.println("CMD:ADVANCE");
}
