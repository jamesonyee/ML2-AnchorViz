# ML2-AchorViz
 
AnchorViz is a Unity-based Augmented Reality project that allows users to place 4 anchor points and visualize 3D models in that plane. This project leverages Magic Leap, Unity's AR Foundation, and OpenXR subsystems to create an immersive experience where users can select different models from a dropdown menu and place them in the real world using anchor points.

<h2> Features </h2>
<ul>
 <li>Create and manage anchor points in AR.</li>
 <li>Visualize different 3D models.</li>
 <li>Easily add and manage models through a dropdown menu.</li>
</ul>
<h2> Requirements </h2>
<ul>
 <li>Unity 2022.3.34f1 or later.</li>
 <li>OpenXR plugin.</li>
 <li>AR Foundation 5.0 or later.</li>
 <li>Magic Leap SDK.</li>
 <li>A Magic Leap 2 device.</li>
</ul>
<h2> Getting Started </h2>
<b> 1. Clone the repository </b>

<p>To clone this repository, use the following command:</p>

```bash
git clone https://github.com/username/ML2-AnchorViz
```
<h3> 2. Import the Project into Unity </h3>
<ul>
 <li>Open Unity Hub.</li>
 <li>Click on <b>Open</b> and select the cloned repository folder.</li>
 <li>Let Unity import all necessary assets.</li>
</ul>
<h3> 3. Adding Your Own Models </h3>
<h4>Step 1: Import Your Model</h4>
<ul>
 <li>Import your 3D model into Unity:</li>
 <ul>
    <li>Drag and drop your model files (.fbx, .obj, etc.) into the <b>Assets</b> folder or a <b>Prefabs</b> subfolder within it.</li>
 </ul>
</ul>
<h4>Step 2: Create a Prefab</h4>
<ul>
 <li>Right-click in the Unity file explorer and navigate to <b>Create > Prefab</b>.</li>
 <li>Rename it and double-click it to open.</li>
 <li>Drag the 3D model you imported into the prefab's Hierarchy window, and it will appear in the viewer.</li>
</ul>
<h4>Step 3: Add the Model to the Array</h4>
<ul>
 <li>In the Hierarchy window, select the <b>ML Rig</b> object.</li>
 <li>In the Inspector window, locate the <b>Spatial Anchors Test</b> script component.</li>
 <li>Under <b>Model Prefabs</b>, click the <b>+</b> button to add a new element to the array.</li>
 <li>Drag your newly created prefab from the <b>Assets/Prefabs</b> folder into the new element slot.</li>
</ul>
![image](https://github.com/user-attachments/assets/d102eb4d-0d9d-4197-9ada-079bc20dc0b9)
<h4>Step 4: Update the Dropdown Menu</h4>
<ol>
 <li>In the Hierarchy window, find the <b>UI</b> object and expand it to locate the <b>Dropdown</b> component.</li>
 <li>Click on the <b>Dropdown</b> object to open its properties in the Inspector window.</li>
 <li>In the Inspector, under <b>Options</b>, update the list to include your model's name.</li>
 <ul>
  <li>Add a new option that corresponds to the name of your model.</li>
  <li>Ensure the order of options matches the order of the prefabs in the <b>Model Prefabs</b> array.</li>
 </ul>
</ol>
![image](https://github.com/user-attachments/assets/e97e56be-b870-4ec4-8c16-2ec2dc4dd60f)
<h3> 4. Running the Application </h3>
<ul>
 <li>Build and run the application on your Magic Leap device.</li>
 <li>Use the dropdown menu to select your model.</li>
 <li>Press the controller bumper to place anchor points and visualize your model in the AR space.</li>
</ul>
