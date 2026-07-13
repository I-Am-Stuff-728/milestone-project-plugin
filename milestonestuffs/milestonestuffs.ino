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
/*
#include <WiFi.h>
#include <WiFiUdp.h>
WiFiUDP udp;
*/

const char* ssid = "TEST_ID_NOT_FINAL";
const char* password = "TEST_PASS_NOT_FINIAL";


const int udpPort = 0000;  //NOTA FINAL PORT
char packet[255];

  const int goLeftPacket = 0;
  const int goRightPacket = 0;
  const int toggleControlPacket = 0;
  const int triggerPacket = 0;
  const int getControllerPacket = 0;
  

const int buttonPins[] = {9, 10, 11, 8};
const int ledPins[] = {6, 7, 13};
const int sonarTrig = 3;
const int sonarEcho = 2;
const int pirPin = 12;
const int buzzerPins[] = {4, 5};
const int motorPins[] = {-1,-1};

bool armed = false;
int armedTicksMax = 50;
int armedTicks = armedTicksMax;
int cooldownTicksMax = 50;
int cooldownTicks = cooldownTicksMax;
float duration, distance;

bool canToggle=true;
bool autoControl = false;

void setup() {
  /*WiFi.begin(ssid, password);
  Serial.println(WiFi.localIP());
  udp.begin(localUdpPort);
 */
  // put your setup code here, to run once:
  pinMode(sonarTrig, OUTPUT);
  pinMode(sonarEcho, INPUT);
  for (int i=0;i<3;i++){
    pinMode(buttonPins[i], INPUT);
    pinMode(ledPins[i], OUTPUT);
  }
    pinMode(buttonPins[3], INPUT_PULLUP);

    pinMode(buzzerPins[0], OUTPUT);
    pinMode(buzzerPins[1], OUTPUT);
    pinMode(pirPin, INPUT);

    pinMode(motorPins[0], OUTPUT);
    pinMode(motorPins[1], OUTPUT);

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
  //Serial.print(F("Distance in cm: "));
  //Serial.println(distance);
  delay(50);

  if (distance>=300){
   // Serial.println("Too far!!");
       digitalWrite(buzzerPins[0], HIGH);
       digitalWrite(buzzerPins[1], HIGH);
  } else {
    digitalWrite(buzzerPins[0], LOW);
    digitalWrite(buzzerPins[1], HIGH);

    if (distance<=100){
     digitalWrite(buzzerPins[1], LOW);
    //  Serial.println(F("VERY"));
    }
    //  Serial.println(F("close"));
  }
 
     if (armed){
      cooldownTicks = cooldownTicksMax;
      if (armedTicks>0){
        armedTicks--;
      //  Serial.println("still arming");
        digitalWrite(ledPins[0], LOW);
        digitalWrite(ledPins[1], HIGH);
      }else{
       //  Serial.println("button ready to shoot");
         digitalWrite(ledPins[0], LOW);
         digitalWrite(ledPins[1], LOW);
      }
  }else{
      if (cooldownTicks>0){
        cooldownTicks--;
       // Serial.println("cooldown");
          if (cooldownTicks % 3 == 0){
            digitalWrite(ledPins[1], LOW);
          }else{
            digitalWrite(ledPins[1], HIGH);
          }
        digitalWrite(ledPins[0], HIGH);
      }else{
      //  Serial.println("ready to arm");
        digitalWrite(ledPins[1], HIGH);
        digitalWrite(ledPins[0], HIGH);
      }  
    }


  if (digitalRead(buttonPins[0]) == LOW){
      trigger();
    //  Serial.println("POW");
  }

  if (digitalRead(buttonPins[1]) == LOW){
      rotateLeft();
    //  Serial.println("LEFT");
  }

  if (digitalRead(buttonPins[2]) == LOW){
      rotateRight();
    //  Serial.println("RIGHT");
  }

  if (digitalRead(buttonPins[3]) == LOW){
      toggleControl();
    //  Serial.println("TOGGLE");
  }else{
    canToggle=true;
  }

 /* int packet = udp.parsePacket();
          //TODO: DO STUFF HERE TO PARSE STUFF?

    switch (packet){
            case goLeftPacket:
              rotateLeft();
      break;
            case goRightPacket:
             rotateRight();
      break;
            case toggleControlPacket:
             toggleControl();
      break;
            case triggerPacket:
             trigger();
      break;
            case getControllerPacket:
             getController();
      break;
    }
*/
    
  if (digitalRead(pirPin) == HIGH){
      digitalWrite(ledPins[2], LOW);
  }else{
      digitalWrite(ledPins[2], HIGH);
  }

}

  //TODO: no pwm so do other shit 
  
void rotateLeft(){
  analogWrite(motorPins[0], 64);
  digitalWrite(motorPins[1], LOW);
}

void rotateRight(){
  analogWrite(motorPins[1], 64); 
  digitalWrite(motorPins[0], LOW);
}
// rotate left/ right, 
void trigger(){
   if (armed){
      if (armedTicks<=0){
       // Serial.println(F("shoot!"));
        armed = false;
        cooldownTicks = cooldownTicksMax;
      }
  }else{
    if (cooldownTicks<=0){
  //  Serial.println(F("begin arming"));
    armed = true;
    armedTicks = armedTicksMax;
    }
  }
}

void toggleControl(){
  if (canToggle){
    canToggle=false;
  if (autoControl){
    autoControl=false;
  }else{
    autoControl=true;
  }
 // getController();
  }
}

void getController(){
  if (autoControl){
    Serial.println(F("Autocontrol"));
  }else{
    Serial.println(F("Operator control"));
  }
  // TODO
  //SEND PACKET FOR WHOS CONTROLLING
}
