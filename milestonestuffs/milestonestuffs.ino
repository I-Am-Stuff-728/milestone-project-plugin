/*

1. Get esp32 
2. Connect esp32 to wifi
3. Receive commands (???) 4. profit
4. Commands = methods to control thing  yess
5. Buttons yess (temporary)
6. Motor control with PWM to rotate board  YES (stepper no pwm even better)
7. 3D print platform 4 board (???)  
8. Attach
9. Buzzers with PWM  NO PWM!!! and no buzzers for silence but i can add it latr
10. Light = TASER TASER TASER    yess

*/

#include <WiFi.h>
#include <WiFiUdp.h>
WiFiUDP udp;

IPAddress local_IP(192,168,0,103);
IPAddress gateway(192,168,0,105);
IPAddress subnet(255,255,255,0);

#include <Stepper.h>
#define STEPS 2048

const char* ssid = "MDA-Set-05";
const char* password = "Milestonesys!";


const unsigned int udpPort = 11000;  //NOTA FINAL PORT

int currentSteps = 0;

unsigned char packetBuffer[255];

  const int goLeftPacket = 0;
  const int goRightPacket = 0;
  const int toggleControlPacket = 0;
  const int triggerPacket = 0;
  const int getControllerPacket = 0;
  
                          // CHANGE 11, 10 TO ANALOG TO USE PWM!!!!!!!                            
const int buttonPins[] = {17, 18, 19, 16};
                  //   trig   l  r  contr
                      //9 10  11  8
const int ledPins[] = {600, 700, 130};
const int sonarTrig = 300;
const int sonarEcho = 200;
const int pirPin = 120;
const int buzzerPins[] = {400, 500};
//const int motorPins[] = {10, 11};
const int stepperPins[] = {100,100,900,110};

Stepper stepper(STEPS, stepperPins[0], stepperPins[1], stepperPins[2], stepperPins[3]);

bool armed = false;
int armedTicksMax = 22;
int armedTicks = armedTicksMax;
int cooldownTicksMax = 36;
int cooldownTicks = cooldownTicksMax;
float duration, distance;

bool canToggle=true;
bool autoControl = false;

void setup() {
    Serial.begin(9600);
    Serial.println("Start");
    if (!WiFi.config(local_IP, gateway, subnet)){
      Serial.println("FUCK YOU");      
    }
  WiFi.begin(ssid, password);
  Serial.println(WiFi.localIP());

while (WiFi.status() != WL_CONNECTED){
  delay(500);
  Serial.println("...");
}
    Serial.println("connected");
  Serial.println(WiFi.localIP());


  udp.begin(udpPort);
 
   stepper.setSpeed(15);
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
}

bool freeze = false;

void loop() {
 //   Serial.println("aaa");
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
 // delay(50);


  if (distance>=300){
       freeze=false;
   // Serial.println("Too far!!");
       digitalWrite(buzzerPins[0], HIGH);
       digitalWrite(buzzerPins[1], HIGH);

  } else {
      freeze=true;
     // frontalDetectedAlarm();
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


 /* if (digitalRead(buttonPins[0]) == LOW){
      trigger();
      Serial.println("POW");
  }

  if (digitalRead(buttonPins[1]) == LOW && !autoControl){
      rotateLeft();
      Serial.println("LEFT");
  }else{
   //  digitalWrite(motorPins[0], LOW); 
  }*/

 /* if (digitalRead(buttonPins[2]) == LOW && !autoControl){
      rotateRight();
      Serial.println("RIGHT");
  }else{
   //  digitalWrite(motorPins[1], LOW);
  }

  if (digitalRead(buttonPins[3]) == LOW){
      toggleControl();
      Serial.println("TOGGLE");
  }else{
    canToggle=true;
  }*/

  if (autoControl){
    currentSteps+=30;
 
      if (currentSteps>=2048){
        currentSteps=0;
      }else if (currentSteps>=1024){
        if (!freeze){
          stepper.step(-60);
        }
      }else{
        if (!freeze){
          stepper.step(60);
        }
      }
   // Serial.println(currentSteps);
  }

  int packet = udp.parsePacket();
  if (packet){
    
    int len = udp.read(packetBuffer, 255);
    if (len > 0) {
              String packetCommand = String ((char*)packetBuffer).substring(0, len);
              Serial.println(packetCommand);

              if (packetCommand.equals("left")){
                    rotateLeft();
                 }
              if (packetCommand.equals("right")){
                    rotateRight();
              }
               if (packetCommand.equals("trigger")){
                    trigger();
                 }
                if (packetCommand.equals("toggle")){
                    toggleControl();
                  }
                if (packetCommand.equals("moveAlarm")){
                 //   toggleControl();
                  }
                if (packetCommand.equals("frontAlarm")){
                 //   toggleControl();
                  }
                if (packetCommand.equals("whoControls")){
               //     getController();
                  }   
                 packetBuffer[len] = 0; 
              }  
          }

  Serial.println(packet);

  /*
    Udp.beginPacket(Udp.remoteIP(), Udp.remotePort());
    Udp.write("Packet testing");
    Udp.endPacket();
  */
  /*
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
  //    Serial.println("YUP");
      motionDetectedAlarm();
  }else{
      digitalWrite(ledPins[2], HIGH);
    //  Serial.println("NOP");
  }

}

  //TODO: no pwm so do other shit 

void rotateLeft(){
  Serial.println("i go to the left");
  stepper.step(-60);
}

void rotateRight(){
    Serial.println("i go to the right");
  stepper.step(60);
}
// rotate left/ right, 
void trigger(){
    Serial.println("i trigger");
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
    Serial.println("i toggle");
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


void motionDetectedAlarm(){
  // TODO:
  //SEND PACKET
}


void frontalDetectedAlarm(){
  // TODO:
  //SEND PACKET
}
