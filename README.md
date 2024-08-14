# ML2-AchorViz
 
AnchorViz is a Unity-based augmented reality (AR) project that allows users to place anchor points and visualize 3D models at those points. This project leverages Magic Leap and Unity's AR Foundation to create an immersive experience where users can select different models from a dropdown menu and place them in the real world using anchor points.

Features
Create and manage anchor points in AR.
Visualize different 3D models at anchor points.
Easily add and manage models through a dropdown menu.
Requirements
Unity 2022.3.34f1 or later
AR Foundation 5.0 or later
Magic Leap SDK
A Magic Leap 2 device
Getting Started

1. Clone the Repository
Clone this repository to your local machine using:

git clone https://github.com/yourusername/AnchorViz.git

2. Import the Project into Unity
Open Unity Hub.
Click on Open and select the cloned repository folder.
Let Unity import all necessary assets.
3. Adding Your Own Models
Step 1: Import Your Model
Import your 3D model into Unity:
Drag and drop your model files (.fbx, .obj, etc.) into the Assets folder or a subfolder within it.
Step 2: Create a Prefab
After importing your model, drag it into the scene.
Adjust the position, rotation, and scale as needed.
Once you're satisfied with the model's setup, drag the model from the Hierarchy window back into the Assets/Prefabs folder to create a prefab.
Step 3: Add the Model to the Array
In the Hierarchy window, select the ML Rig object.
In the Inspector window, locate the Spatial Anchors Test script component.
Under Model Prefabs, click the + button to add a new element to the array.
Drag your newly created prefab from the Assets/Prefabs folder into the new element slot.
Step 4: Update the Dropdown Menu
In the Hierarchy window, find the UI object and expand it to locate the Dropdown component.
Click on the Dropdown object to open its properties in the Inspector window.
In the Inspector, under Options, update the list to include your model's name.
Add a new option that corresponds to the name of your model.
Ensure the order of options matches the order of the prefabs in the Model Prefabs array.
4. Running the Application
Build and run the application on your Magic Leap device.
Use the dropdown menu to select your model.
Press the controller bumper to place anchor points and visualize your model in the AR space.
