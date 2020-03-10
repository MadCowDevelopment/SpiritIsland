int TOP = 1;
int MIDDLE = 2;
int BOTTOM = 3;
int lastButton = 0;

bool started = false;

void topButtonPressed() {  
  if(!started) return;
  if(lastButton == TOP) return;  
  lastButton = TOP;  
  Serial.print("EXPLORE");
}

void middleButtonPressed() {
  if(started) return;
  if(lastButton == MIDDLE) return;
  lastButton = MIDDLE;
  Serial.print("START");
  started = true;  
}

void bottomButtonPressed() {
  Serial.print("I'm here");
  if(!started) return;
  if(lastButton == BOTTOM) return;  
  lastButton = BOTTOM;  
  Serial.print("ADVANCE");
}
