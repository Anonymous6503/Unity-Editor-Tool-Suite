# Unity Precision Surface Snapper


## 🚀 Overview
An advanced, editor-only pipeline utility for Unity that automates the spatial alignment of massive GameObject arrays.

When greyboxing levels, scattering foliage, or assembling modular environments, designers often spend hours manually dragging individual props to sit flush against complex, uneven terrains. The **Precision Surface Snapper** eliminates this friction. It instantly projects floating or clipping objects flush against any collision mesh, matching the surface slope and randomizing rotation to create organic, production-ready placements in a single click.

## 🧰 Real-World Use Cases
* **Foliage & Debris Scattering:** Instantly drop hundreds of duplicated trees or rocks onto a newly sculpted mountain range without them floating in the air.
* **Cave & Interior Detailing:** Use the bidirectional raycast to snap stalactites or hanging wires perfectly to irregular ceiling meshes.
* **Rapid Iteration:** When a Lead Level Designer drastically changes the heightmap of a terrain, simply highlight all existing props and click "Execute" to instantly re-align them to the new ground level.

## ⚙️ Core Features & Capabilities

* **Bidirectional Projection:** Intelligently raycasts `Vector3.down` to floors or `Vector3.up` to ceilings, automatically determining the closest physical surface.
* **Surface Normal Alignment:** Extracts the `RaycastHit.normal` data to calculate the exact slope of the impact point, modifying the object's local `Up` vector using internal engine math to sit perfectly flush against hills or angled walls.
* **Organic Scatter (Y-Axis Randomization):** An optional randomizer that spins objects on their local Y-axis (`Space.Self`) upon impact. This breaks up visual repetition, which is critical for making duplicated rocks or grass look natural.
* **Native LayerMasking Integration:** Utilizes Unity's `SerializedObject` architecture to summon a native, multi-select Layer dropdown. This allows users to safely ignore invisible triggers or water layers during the drop calculation.
* **Offset Control:** Allows manual `Vector3` offsets. Useful for sinking objects (like modular walls or ruins) slightly into the terrain to hide bottom edge seams.
* **Non-Destructive Pipeline:** Fully integrated with Unity's `Undo.RecordObject` system. Accidental mass-drops can be instantly reverted with a standard `Ctrl + Z`.

## 💻 Technical Architecture & Notes
* **Physics over Transforms:** Bypasses standard `Transform` position manipulation by utilizing `Physics.Raycast` for accurate spatial data retrieval across complex colliders.
* **Memory Safe UI:** The custom Editor GUI is built using `EditorGUILayout`. It intentionally utilizes `SerializedProperty` tracking rather than standard C# variables to ensure UI state survives Unity's background assembly reloads (preventing the common "ghost variable" editor bug).

## 🛠️ Prerequisites
* Unity 2021.3 LTS or higher (Recommended).
* Target surfaces *must* have active Colliders (MeshCollider, BoxCollider, TerrainCollider, etc.) for the physics raycast to detect them.

## 🚀 Installation & Step-by-Step Usage

1. **Install:** Download the `SurfaceSnapperTool.cs` script and place it inside any folder named `Editor` within your Unity project's `Assets` directory.
2. **Select:** Highlight your target objects (e.g., 50 floating rocks) in the Unity Hierarchy.
3. **Open:** Click `Tools > Surface Snapper Tool` in the top toolbar to open the dockable window.
4. **Configure:** * Set your **Target Layers** (e.g., enable "Terrain", disable "Water").
    * Toggle **Align To Normal** to match the slope.
    * Toggle **Randomize Rotation** for organic variety.
5. **Execute:** Click the `Execute` button and watch the objects snap into place.