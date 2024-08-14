# ML2-AchorViz
 
AnchorViz is a Unity-based Augmented Reality project that allows users to place 4 anchor points and visualize 3D models in that plane. This project leverages Magic Leap, Unity's AR Foundation, and OpenXR subsystems to create an immersive experience where users can select different models from a dropdown menu and place them in the real world using anchor points.

<h2> Features </h2>
<ul>
 <li> Create and manage anchor points in AR. </li>
 <li>Visualize different 3D models</li>
 <li>Easily add and manage models through a dropdown menu.</li>
</ul>

<h2> Requirements </h2>
<ul>
 <li>Unity 2022.3.34f1 or later</li>
 <li>OpenXR plugin </li>
 <li>AR Foundation 5.0 or later</li>
 <li>Magic Leap SDK</li>
 <li>A Magic Leap 2 device</li>
</ul>

<h2> Getting Started </h2>
<b> 1. Clone the repository </b> 
<p>To clone this repository, use the following command:</p>

```bash
git clone https://github.com/username/ML2-AnchorViz
```
<b><p>2. Import the Project into Unity</p></b>
<ul> 
 <li>Open Unity Hub. </li>
 <li>Click on <b>Open</b> and select the cloned repository folder. </li>
 <li>Let Unity import all necessary assets. </li>
</ul>

<b><p> 3. Adding Your Own Models </p></b>
<b>Step 1: Import Your Model </b>
<ol>
  <li> Import your 3D model into Unity: </li>
  <ul>
     <li> Drag and drop your model files (.fbx, .obj, etc.) into the Assets folder or a prefabs subfolder within it. </li>
  </ul>
</ol>

<b><p>Step 2: Create a Prefab</p></b>
<ol>
 <li>Right click in the Unity file explorer and navigate to <b>Create > Prefab </b> </li>
 <li>Rename it and double click it top open</li>
 <li>Drag the 3d model you imported in the prefab Hierarchy window and it will appear in the viewer</li>
</ol>

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
