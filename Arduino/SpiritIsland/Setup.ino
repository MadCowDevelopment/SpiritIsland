void setup() {
  pinMode(SER, OUTPUT);
  pinMode(LATCH, OUTPUT);
  pinMode(CLK, OUTPUT);

  cycleColors(500);
  turnAllOff();
  analogWrite(ESCALATE, 0);  
  
  Serial.begin(9600);
}
