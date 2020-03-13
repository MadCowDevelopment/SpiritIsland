void setup() {
  pinMode(SER, OUTPUT);
  pinMode(LATCH, OUTPUT);
  pinMode(CLK, OUTPUT);
  
  sr.interrupt(ShiftRegisterPWM::UpdateFrequency::Medium);
  
  cycleColors(500);
  turnAllOff();
  analogWrite(ESCALATE, 0);

  Serial.begin(9600);  
}
