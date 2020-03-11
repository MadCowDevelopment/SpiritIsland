int TOP = 1;
int MIDDLE = 2;
int BOTTOM = 3;
int lastButton = 0;

int sensor1Value = 0;
int sensor2Value = 0; 

bool started = false;

void handleRemoteInput() {
  sensor1Value = analogRead(sensor1Pin);
  sensor2Value = analogRead(sensor2Pin);

  if(sensor1Value < 180) {
    topButtonPressed();
  }
  else if(sensor1Value > 900) {
    bottomButtonPressed();
  }
  else middleButtonPressed();
}

void topButtonPressed() {  
  if(!started) return;
  if(lastButton == TOP) return;  
  lastButton = TOP;  
  Serial.println("EXPLORE");
}

void middleButtonPressed() {
  if(!connected) return;
  if(started) return;
  if(lastButton == MIDDLE) return;
  lastButton = MIDDLE;
  Serial.println("START");
  started = true;  
}

void bottomButtonPressed() {
  if(!started) return;
  if(lastButton == BOTTOM) return;  
  lastButton = BOTTOM;  
  Serial.println("ADVANCE");
}
