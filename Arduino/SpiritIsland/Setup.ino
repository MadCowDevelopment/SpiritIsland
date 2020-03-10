void setup() {
  Serial.begin(9600);
  
  pinMode(SER, OUTPUT);
  pinMode(LATCH, OUTPUT);
  pinMode(CLK, OUTPUT);

  // Self check
  cycleColors(250);
  turnAllOff();
}
