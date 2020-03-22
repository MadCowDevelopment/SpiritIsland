void setup() {
  pinMode(PIN_SER, OUTPUT);
  pinMode(PIN_LATCH, OUTPUT);
  pinMode(PIN_CLK, OUTPUT);  
  sr.interrupt(ShiftRegisterPWM::UpdateFrequency::Medium);

  initLightSensor();

  cycleColors(500);

  turnAllOff();

  analogWrite(PIN_ESCALATE, 0);

  Serial.begin(9600);  
}
