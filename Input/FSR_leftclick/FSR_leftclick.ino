/* FSR testing sketch. 
Connect one end of FSR to 5V, the other end to Analog 0.
Then connect one end of a 10K resistor from Analog 0 to ground
Connect LED from pin 11 through a resistor to ground 
*/
 
#define fsrAnalogPin1  1 // FSR is connected to analog 1
#define threshold 100
//#define LEDpin  5      // connect Red LED to pin 5 (debug purpose)
int fsrReading1;      // the analog reading from the FSR resistor divider


 
void setup(void) {
  Serial.begin(115200);   
  //pinMode(LEDpin, OUTPUT);
}
 
void loop(void) {
  fsrReading1 = analogRead(fsrAnalogPin1);
  //Serial.print("Analog reading = "); //(debug purpose)
  if (fsrReading1 > threshold){
    Serial.println(1);
    //analogWrite(LEDpin, 255); // (debug purpose)
  }
  else{ 
    Serial.println(0);
    //analogWrite(LEDpin, 0); //(debug purpose)
  }
  // we'll need to change the range from the analog reading (0-1023) down to the range
  // used by analogWrite (0-255) with map!
  //LEDbrightness = map(fsrReading, 0, 1023, 0, 255);
  // LED gets brighter the harder you press
  delay(100);
}
