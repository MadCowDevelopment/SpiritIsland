void setup() {
  pinMode(SER, OUTPUT);
  pinMode(LATCH, OUTPUT);
  pinMode(CLK, OUTPUT);

  cycleColors(500);
  turnAllOff();

  Serial.begin(9600);
}
