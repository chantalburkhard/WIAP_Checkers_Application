# Checkers Application - A university project
The application uses [Unity 3d](https://unity.com/de), [MRTK](https://www.microsoft.com/en-us/download/details.aspx?id=102778) and [Photon PUN 2](https://www.photonengine.com/pun) to create a AR/VR game for Microsoft HoloLens 2 and the HP Reverb G1 Headset.

There are two play options:
1. Single player - Loads a scene for one player to play locally (with future updates maybe against a checkers ai) 
2. Multi player - Loads a scene for two or more real devices (e.g. 2 HoloLense devices, 2 VR-Headsets, one of each, etc.) to join and play against each other with network synchronization and simple player avatars. In the current state each player spawns a checkers piece when joining the room and is able to move their own piece. The other player is able to see the movement as well as the avatar respresenting the player.

Hint: currently the game mechanics are not completely implememted in the MRTK scenes (but are fully implemented in test scenes in a normal unity 3d surrounding -> in the Folders "MultiplayerCheckersTest")


# Build the project for HoloLens 2
Make sure that your computer and the HoloLens have the developer mode activated.

Since the project is still in development the building process and running the application on HoloLens 2 currently works via Visual Studio remote debugging.

## Build the project in Unity
1. Open the project in Unity
2. Open "Build Settings" and switch to "Universal Windows Platform" change settings as seen in the screenshot below
![Screenshot (145)](https://github.com/chantalburkhard/WIAP_Checkers_Application/assets/73609488/d8bb6a0e-3825-4208-a9b5-21388859dc6e)

4. Click "Build" and choose a directory where you want to save your files

## Set up Visual Studio 19
1. Make sure [Visual Studio 19](https://learn.microsoft.com/de-de/visualstudio/releases/2019/release-notes) is installed with the UWP workload
![Screenshot (114)](https://github.com/chantalburkhard/WIAP_Checkers_Application/assets/73609488/f1fdc586-822d-4835-982b-812c03b867bf)

## Build an run the project in Visual Studio
1. After the building process is finished in unity open the directory
2. Click the .sln file to open the project in Visual Studio

![Screenshot (117)](https://github.com/chantalburkhard/WIAP_Checkers_Application/assets/73609488/c18ff168-d83c-45f0-a883-712eea78bafe)

3. Make sure Debug and ARM64 are set as well as remote device

![Screenshot (119)](https://github.com/chantalburkhard/WIAP_Checkers_Application/assets/73609488/3570ec0c-5322-435b-8137-6eb2f7955c5d)

4. Go to the debug configuration

![Screenshot (120)](https://github.com/chantalburkhard/WIAP_Checkers_Application/assets/73609488/fb548c60-dd4e-4c48-a9ac-e49a7ca44f70)

5. Click on search to find the remote device (make sure the HoloLens is connected to the same network)

![Screenshot (121)](https://github.com/chantalburkhard/WIAP_Checkers_Application/assets/73609488/9a6e32d5-341a-4be1-bfdd-f146288f903a)

6. Select the device (when asked for the device pin you have to open the developer settings on the Hololens and click on pairing enter the shown code in Visual Studio)

![20230716_162709_HoloLens (3)](https://github.com/chantalburkhard/WIAP_Checkers_Application/assets/73609488/b90947d5-fdec-4aac-a16d-df9db2d9950a)

7. Now the device name should show up

![Screenshot (122)](https://github.com/chantalburkhard/WIAP_Checkers_Application/assets/73609488/4c7034dd-33dd-4039-8ada-41a4f4f11624)

8. Click on ok
9. Make sure that the [Server_Start_Application](https://github.com/chantalburkhard/WIAP_Server_Start_Application) is running an the room "Checkers" was created
11. Start the debugging process via the green arrow on the remote device
12. After the building was successful the application should be running on the Hololens

# Build the project for the HP Reverb Headset

## Build the project in unity
1. Open the project in unity
2. Open "Build Settings" and switch to "Windows, Mac, Linux"
3. Click "Build" or "Build And Run"
