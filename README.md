<h1 align="center">✈️ SkyWings</h1>

<p align="center">
  <b>A 3D open-world flight simulator — pilot a fixed-wing aircraft, explore vast landscapes, and master the skies.</b>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Engine-Unity%202022.3-blue?logo=unity" alt="Engine"/>
  <img src="https://img.shields.io/badge/Language-C%23-239120?logo=csharp" alt="Language"/>
  <img src="https://img.shields.io/badge/Platform-Windows%20%7C%20Linux-lightgrey" alt="Platform"/>
  <img src="https://img.shields.io/badge/Status-In%20Development-yellow" alt="Status"/>
  <img src="https://img.shields.io/badge/License-MIT-green" alt="License"/>
</p>

---

## 📖 Description

- **Genre:** 3D Flight Simulator / Open World Explorer
- **Theme:** Realistic aviation across open natural landscapes (valley, forest, desert, or city)
- **Gameplay:** Take control of a fixed-wing aircraft in a fully explorable 3D open world. Master realistic take-off and landing, control pitch, roll, and yaw for lifelike flight dynamics, and complete mission-style activities — drop balloons at designated spots, capture aerial photographs by flying over targets, and switch to a nose-mounted cockpit camera for an immersive first-person view. The world is navigable via a third-person follow camera during free exploration.
- **Target Audience:** Casual players & aviation/simulation enthusiasts

---

## 🛠️ Tech Stack / Tools

| Category        | Tool / Technology          |
| --------------- | -------------------------- |
| Game Engine     | Unity 2022.3 LTS |
| Language        | C# |
| IDE / Editor    | Visual Studio 2022 / VS Code |
| 3D Modeling     | Blender |
| Audio Tool      | Audacity |
| UI Framework    | Unity UI Toolkit / Unity Canvas |
| Physics         | Unity Rigidbody + Custom Flight Physics |
| Version Control | Git + GitHub |

---

## 📁 Project Structure

### Assets — By Type with Grouped Utilities

```
📦 project-root/
├── 📂 Assets/
│   ├── 📂 _ThirdParty/             # External libraries (DOTween, TextMeshPro, etc.)
│   │   └── 📂 Plugins/             #   Asset Store plugins, SDKs, native plugins
│   ├── 📂 _Shared/                 # Cross-feature shared assets
│   │   ├── 📂 Fonts/               #   Font files (.ttf, .otf)
│   │   └── 📂 Shaders/             #   Custom shaders & shader graphs
│   ├── 📂 Animations/              # Animation clips & controllers
│   ├── 📂 Art/
│   │   ├── 📂 Materials/           #   Materials & shaders
│   │   ├── 📂 Models/              #   3D models (.fbx, .obj)
│   │   ├── 📂 Sprites/             #   2D sprites & sprite sheets
│   │   ├── 📂 Textures/            #   Textures, UI graphics, icons
│   │   └── 📂 VFX/                 #   Particle systems, visual effects
│   ├── 📂 Audio/
│   │   ├── 📂 Music/               #   Background music tracks
│   │   ├── 📂 SFX/                 #   Sound effects
│   │   └── 📂 Ambience/            #   Ambient/environment sounds
│   ├── 📂 Prefabs/                 # Reusable game objects
│   │   ├── 📂 Characters/          #   Player, enemies, NPCs
│   │   ├── 📂 Environment/         #   Props, obstacles, platforms
│   │   ├── 📂 Projectiles/         #   Bullets, arrows, spells
│   │   ├── 📂 UI/                  #   UI prefabs (panels, popups)
│   │   └── 📂 VFX/                 #   Particle prefabs (hit, explosion)
│   ├── 📂 Scenes/                  # All game scenes
│   │   ├── 📂 Levels/              #   Gameplay levels
│   │   ├── 📂 UI/                  #   Menu scenes (MainMenu, Loading)
│   │   └── 📂 Test/                #   Sandbox / test scenes (exclude from build)
│   ├── 📂 Scripts/                 # ← See Script Structure below
│   ├── 📂 Resources/               # Assets loaded via Resources.Load() (use sparingly!)
│   │   └── 📂 Data/                #   ScriptableObjects, configs, DataAssets
│   └── 📂 StreamingAssets/         # Files copied as-is to build (JSON, CSV, video)
├── 📂 Docs/                        # Design documents, GDD
├── 📂 Packages/                    # Unity Package Manager overrides
├── 📄 .gitignore
├── 📄 LICENSE
└── 📄 README.md
```

### Scripts — Modular Feature System


```
📂 Scripts/
├── 📂 _Core/                            # Shared infrastructure (no game logic)
│   ├── 📂 Events/                       #   Event channels & listeners (OnBalloonDropped, OnPhotoTaken)
│   ├── 📂 Interfaces/                   #   IPhotoTarget, IBalloonZone, ICameraMode
│   ├── 📂 Patterns/                     #   Singleton, ObjectPool, StateMachine
│   ├── 📂 Extensions/                   #   C# extension methods / helpers
│   └── 📂 Constants/                    #   GameConstants, Tags, Layers, InputNames
│
├── 📂 FlightSystem/                     # System: Fixed-Wing Aircraft & Flight Physics
│   ├── 📂 Core/                         #   FlightController, TakeoffLanding, PitchRollYaw
│   ├── 📂 Data/                         #   FlightConfig, FlightStats, AircraftState
│   ├── 📂 ScriptableObjects/            #   AircraftSO (lift, drag, thrust params)
│   ├── 📂 Input/                        #   FlightInputHandler, InputActions
│   ├── 📂 UI/                           #   FlightHUD, SpeedAltitudeDisplay
│   └── 📄 README.md
│
├── 📂 CameraSystem/                     # System: Camera Modes (Third-Person & Nose Cam)
│   ├── 📂 Core/                         #   CameraController, CameraModeSwitcher
│   ├── 📂 Data/                         #   CameraConfig, CameraMode (enum)
│   ├── 📂 UI/                           #   CameraModeIndicatorUI
│   └── 📄 README.md
│
├── 📂 BalloonSystem/                    # System: Balloon Drop at Specific Spots
│   ├── 📂 Core/                         #   BalloonDropper, BalloonZoneManager
│   ├── 📂 Data/                         #   BalloonZoneData, BalloonDropRecord
│   ├── 📂 ScriptableObjects/            #   BalloonZoneSO (position, radius, balloon type)
│   ├── 📂 Trigger/                      #   BalloonZoneTrigger (scene-placed zone colliders)
│   ├── 📂 UI/                           #   BalloonHUD, ZoneMarkerUI
│   └── 📄 README.md
│
├── 📂 PhotoSystem/                      # System: Aerial Photo Capture
│   ├── 📂 Core/                         #   PhotoCapture, PhotoTargetDetector, PhotoGallery
│   ├── 📂 Data/                         #   PhotoData, PhotoTargetData
│   ├── 📂 ScriptableObjects/            #   PhotoTargetSO (target location, description)
│   ├── 📂 Trigger/                      #   PhotoZoneTrigger (area that activates target)
│   ├── 📂 UI/                           #   PhotoViewfinderUI, GalleryUI, CaptureFlashUI
│   └── 📄 README.md
│
├── 📂 WorldSystem/                      # System: 3D Open World & Environment
│   ├── 📂 Core/                         #   WorldManager, TerrainManager, LandmarkManager
│   ├── 📂 Data/                         #   WorldConfig, LandmarkData
│   ├── 📂 ScriptableObjects/            #   WorldSettingsSO, LandmarkSO
│   ├── 📂 UI/                           #   MinimapUI, CompassUI
│   └── 📄 README.md
│
├── 📂 AudioSystem/                      # System: Audio (Engine, SFX, Ambience)
│   ├── 📂 Core/                         #   AudioManager, EngineAudioController, AmbiencePlayer
│   ├── 📂 Data/                         #   SFXLibrary, EngineAudioConfig
│   └── 📄 README.md
│
├── 📂 MainMenuSystem/                   # System: Main Menu & Settings
│   ├── 📂 Core/                         #   MainMenuManager, SceneLoader
│   ├── 📂 UI/                           #   MainMenuUI, SettingsUI, PauseMenuUI
│   └── 📄 README.md
│
└── 📂 SaveSystem/                       # System: Save & Load Game State
    ├── 📂 Core/                         #   SaveManager, SaveSerializer
    ├── 📂 Data/                         #   SaveData (balloon progress, photo gallery, position)
    └── 📄 README.md
```

**Sub-folder per System:**

| Folder | Isi | Contoh |
|---|---|---|
| `Core/` | Manager, main logic, controller | `FlightController.cs`, `BalloonDropper.cs`, `PhotoCapture.cs` |
| `Data/` | Data classes, structs, enums (C# only) | `FlightConfig.cs`, `BalloonZoneData.cs`, `PhotoData.cs` |
| `ScriptableObjects/` | ScriptableObject definitions | `AircraftSO.cs`, `BalloonZoneSO.cs`, `PhotoTargetSO.cs` |
| `Input/` | Input handling (khusus FlightSystem) | `FlightInputHandler.cs` |
| `Trigger/` | Scene-placed collider/zone scripts | `BalloonZoneTrigger.cs`, `PhotoZoneTrigger.cs` |
| `UI/` | UI scripts for this system | `FlightHUD.cs`, `PhotoViewfinderUI.cs`, `ZoneMarkerUI.cs` |
| `Editor/` | Custom editor tools (jika ada) | `BalloonZoneEditor.cs` |

> **Aturan:** Setiap system bersifat **self-contained** — bisa dicabut dari project tanpa merusak structure. `_Core/` tidak boleh depend ke system manapun. Antar-system berkomunikasi via events/interfaces di `_Core/`.

---
## 📏 Coding Standards

### Naming Conventions

| Type           | Convention        | Example                     |
| -------------- | ----------------- | --------------------------- |
| Class          | PascalCase        | `PlayerController`          |
| Method         | PascalCase        | `TakeDamage()`              |
| Variable       | camelCase         | `moveSpeed`                 |
| Private Field  | _camelCase        | `_currentHealth`            |
| Constant       | UPPER_SNAKE_CASE  | `MAX_HEALTH`                |
| Enum           | PascalCase        | `GameState.Playing`         |
| Interface      | I + PascalCase    | `IDamageable`               |
| ScriptableObj  | SO_ + PascalCase  | `SO_WeaponData`             |

### Code Structure (per script)

```csharp
// 1. Using statements
// 2. Namespace (MyGame.FeatureName)
// 3. Class declaration
//    3a. [SerializeField] private fields     ← Inspector-exposed
//    3b. Private fields                      ← Internal state
//    3c. Events (Action, UnityEvent)
//    3d. Properties
//    3e. Unity lifecycle (Awake → OnEnable → Start → Update → LateUpdate → OnDisable)
//    3f. Public methods
//    3g. Private methods
//    3h. Editor-only (#if UNITY_EDITOR)
```

## 🌿 Branching & Workflow

### Branch Strategy

```
main ────────────────────────────────────── (stable release)
  │
  ├── develop ───────────────────────────── (integration branch)
  │     │
  │     ├── feature/player-movement ─────── (new feature)
  │     ├── feature/enemy-ai ────────────── (new feature)
  │     ├── fix/camera-bug ──────────────── (bug fix)
  │     └── art/new-tileset ─────────────── (art update)
  │
  └── release/v0.2.0 ───────────────────── (release candidate)
```

### Branch Naming

| Type      | Format                     | Example                      |
| --------- | -------------------------- | ---------------------------- |
| Feature   | `feature/description`      | `feature/inventory-system`   |
| Bug Fix   | `fix/description`          | `fix/player-fall-through`    |
| Art       | `art/description`          | `art/new-enemy-sprites`      |
| Audio     | `audio/description`        | `audio/boss-theme`           |
| Refactor  | `refactor/description`     | `refactor/input-system`      |
| Docs      | `docs/description`         | `docs/update-readme`         |


### Commit Convention
```
<type>: <short description>
```

| Prefix Type | Usage                                                |
| ----------- | ---------------------------------------------------- |
| `Add:`      | New feature or content                               |
| `Fix:`      | Bug fix                                              |
| `Update:`   | Improvement or refactor                              |
| `Remove:`   | Removed feature or file                              |
| `Docs:`     | Documentation changes                                |
| `Art:`      | Art/asset changes                                    |
| `Audio:`    | Audio-related changes                                |
| `Refactor:` | Code restructuring without new features or bug fixes |
| `Test:`     | Adding or modifying tests                            |

**Examples:**
```
Add: basic enemy patrol behavior
Fix: player clipping through walls on slopes
Art: new idle animation for main character (8 frames)
Refactor: extract health system into reusable component
```


## 🚀 Getting Started

### Installation

```bash
# 1. Clone the repository
git clone https://github.com/princenizroh/sky-wings.git

# 2. Navigate to the project folder
cd sky-wings

```

---

## 📜 License

This project is licensed under the **MIT License** — see the [LICENSE](LICENSE) file for details.

---

<p align="center">
  Made with ❤️ and ☕ by <a href="https://github.com/princenizroh">princenizroh</a>
</p>