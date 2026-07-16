# Milestone Project - Controller Plugin for F.R.I.E.N.D. + Driver (Forward Rapid Internet Emitting Nearsearching Device)

  F.R.I.E.N.D. is a revolutionary device here to aid in your security problems. Thanks to the SDK provided by Milestone systems the device can be used remotely from the platform itself in tandem with Cameras.

## 1. Installation
  - Make sure you've installed the X Protect Smart Client.
  - Download and extract the zip file.
  - Locate the .ino SKETCH file. Then, open it in the Arduino Code editor. There, you must input your WiFi SSID and password. Then, connect the Device and upload the code. Once it has finished uploading, make sure to write down the IP Address.
  - Locate the Plugin Folder, containing all the DLLs & others. Then, locate the Milestone Smart Client folder, and inside find the MIPPlugins folder. Simply drag the extracted Plugin Folder in the MIPPlugins folder.
## 2. How to use
  - Once you've opened the client, open a view with the Plugin's name.
  - On the bottom section you'll find a field. Simply input the previously written down IP and then click Connect. You'll know you are connected once a MessageBox appears and the indicator on the right has turned GREEN.
  - Press the button again (now Disconnect) to disconnect. You'll know it worked when the indicator turns RED.
  - Once connected, use Toggle to switch Autocontrol on and off. There's an indicator on the bottom which shows if Autocontrol is turned on or off.
  - The Move and Front indicators turn red when movement and/or something in front of the Device is present accordingly, and green if everything  is still and the sensor is clear.
  - Use Left/Right to rotate the Device. Note that this can only be done after you have revoked control from the Device.
  - Use Activate/Trigger to commence procedure**. When first activated it has an Arming period of a few seconds, after which it can be activated again* to fully trigger the procedure. You must also wait before immediately using it again, as there is a Cooldown period.
  - That's all! You are now a master F.R.I.E.N.D. operator!
  
*Note I: You do not need to revoke control from the Device to activate it. It stops moving on its own if something is in front, which ensures a stable path.
(*Note II: We are not responsible for any bodily harm/property damage caused due to the Procedure or any modifications. Use with caution and only in life-threatening scenarios.)

## 3. Device capabilities
  - The Device is capable of Autocontrol - rotating on its own without a camera operator, sending alerts when detecting motion or something with the front sensor.
  - The Device can activate a custom Procedure.
  - The Device features LEDs to show whether it is armed, cooling down or activated, as well as a motion detector to discourage would-be assailants.
  - The Device has Buzzers (interchangeable with LEDs) to deter anyone close enough to activate the Procedure.
  - The Device can connect to the internet and is programmable and open source, so you may add whatever Procedure you want.
## 4. Technical requirements
  - The Device is reliant on a WiFi connection, so make sure to add it to a network (considering this is a plugin for a Camera network platform, you could have already done this.)
  - The Device requires 5V DC to run, it can be powered by a USB Port, battery, or a charger.
  - A somewhat clear space/location with a radius of about 3 meters, where it wont detect random objects (place near a pathway, entrance).
## 5. Code
  - C# MIPSDK Tools used for the plugin (milestonesystem's VideoOS on NuGet)
  - C++ Arduino IDE used for microcontroller.
