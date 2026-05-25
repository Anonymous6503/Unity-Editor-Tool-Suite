# Unity Bulk Renamer & Organizer Tool

<img width="318" height="504" alt="image" src="https://github.com/user-attachments/assets/840bd0c1-4c75-4575-bd85-f71c2a3b9813" />


## 🚀 Overview
A custom Unity Editor Window built in C# that allows developers to batch-rename, auto-parent, and modify properties for multiple GameObjects in the hierarchy simultaneously, drastically reducing manual pipeline friction.

## ⚙️ Features
* **Batch Renaming:** Applies prefix, base name, and sequential numbering conventions.
* **Hierarchy Organization:** Instantly parents selected objects under a defined target transform.
* **Property Assignment:** Batch assign Tags, Layers, and Static flags in a single click.
* **Safety First:** Full Unity "Undo" integration to easily revert accidental mass-changes.

## 💻 Technical Highlights
* Utilizes "Selection.gameObjects" for dynamic state reading.
* Implements robust string manipulation using "StringBuilder" for memory efficiency.
* Custom editor GUI utilizing "EditorGUILayout".

## 🛠️ Installation
1. Download the 'BulkRenamerWindow.cs' script.
2. Place it inside any folder named "Editor" within your Unity project's "Assets" folder.
3. Click "Tools > Bulk Rename" in the top Unity toolbar.
