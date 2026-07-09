/*

1. Get esp32
2. Connect esp32 to wifi
3. Receive commands (???) 4. profit
4. Commands = methods to control thing
5. Buttons
6. Motor control with PWM to rotate board
7. 3D print platform 4 board (???)
8. Attach
9. Buzzers with PWM
10. Light = TASER TASER TASER

*/

const int buttonPins[] = {9, 10, 11, 12};
const int ledPins[] = {0, 0};
const int sonarTrig = 0;
const int sonarEcho = 0;
const int pirPin = 0;
const int buzzerPins[] = {0, 0};

bool armed = false;
int armedTicksMax = 15;
int armedTicks = armedTicksMax;
int cooldownTicksMax = 5;
int cooldownTicks = cooldownTicksMax;
float duration, distance;

bool autoControl = false;

void setup() {
  // put your setup code here, to run once:
  pinMode(sonarTrig, OUTPUT);
  pinMode(sonarEcho, INPUT);
  Serial.begin(9600);

}

void loop() {
  // put your main code here, to run repeatedly:
  digitalWrite(sonarTrig, LOW);
  delayMicroseconds(2);
  digitalWrite(sonarTrig, HIGH);
  delayMicroseconds(10);
  digitalWrite(sonarTrig, LOW);

  duration = pulseIn(sonarEcho, HIGH);
  distance = (duration*.0343)/2;
  Serial.print(F("Distance in cm: "));
  Serial.println(distance);
  delay(50);

  if (distance>=300){
    Serial.println("Too far!!");
  } else {
    digitalWrite(buzzerPins[0], HIGH);
    if (distance<=100){
     digitalWrite(buzzerPins[1], HIGH);
      Serial.println(F("VERY"));
    }
      Serial.println(F("close"));
  }
  if (armed){
      cooldownTicks = cooldownTicksMax;
      if (armedTicks>0){
        armedTicks--;
        Serial.println("WAIT");
      }else{
       digitalWrite(ledPins[0], HIGH);
        Serial.println("button ready");
      }
  }else{
      if (cooldownTicks>0){
        cooldownTicks--;
        digitalWrite(ledPins[1], HIGH);
      }  
    }

  if (digitalRead(buttonPins[0]) == LOW){
      trigger();
  }

  if (digitalRead(buttonPins[1]) == LOW){
      rotateLeft();
  }

  if (digitalRead(buttonPins[2]) == LOW){
      rotateRight();
  }

  if (digitalRead(buttonPins[3]) == LOW){
      toggleControl();
  }

}

void rotateLeft(){
  //PWM MOTOR TODO
}

void rotateRight(){
  //PWM MOTOR TODO
}
// rotate left/ right, 
void trigger(){
  if (armed){
    Serial.println(F("pow"));
    armed = false;
    cooldownTicks = cooldownTicksMax;
  }else{
     Serial.println(F("ready to pow"));
    armed = true;
    armedTicks = armedTicksMax;
  }
}

void toggleControl(){
  if (autoControl){
    autoControl=false;
  }else{
    autoControl=true;
  }
  getController();
}

void getController(){
  if (autoControl){
    Serial.println(F("Autocontrol"));
  }else{
    Serial.println(F("Operator control"));
  }
}
