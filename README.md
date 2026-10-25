# Space Quests — First-person Unity game prototype

**Unity 2020.1.4f1 · C# · 3D gameplay systems**

> **Status:** Historical learning/game-development project. Source assets and scripts are available, but a clean build and complete playthrough have not been independently verified.

## Project overview

Space Quests is a first-person Unity game project combining exploration, collectible objectives, interactable objects, enemies and an inventory interface. It illustrates how different gameplay systems communicate through Unity components and managers.

## Systems found in the repository

- **First-person movement:** walking, sprinting, jumping and camera controls, implemented in `Assets/Scripts/Player/`.
- **Inventory:** item types, backpack slots, stacking, dragging items between slots and an equipment quickslot system.
- **Enemy AI:** patrol, chase and attack decisions driven by distance checks and a Unity NavMeshAgent.
- **Items and puzzles:** collectible counting and object interactions.
- **Combat:** scripts for projectiles, gun interactions and targets.
- **Scene flow:** main menu, game and game-over scenes.
- **Player interface:** inventory display, cursor state and configurable controls.

These are **source-level observations**, not a guarantee that every system is integrated and working in a complete build.

## Technology and layout

| Topic | Implementation |
| --- | --- |
| Engine | Unity **2020.1.4f1**, per `ProjectSettings/ProjectVersion.txt` |
| Language | C# |
| Gameplay | Unity CharacterController, NavMesh, colliders, physics |
| UI and state | Unity UI, inventory/quickslot scripts, GameManager |
| Key source directory | `Assets/Scripts/` |
| Scenes | `Assets/Scenes/` |

## Explore the project

1. Clone the repository. This is a **large historical Unity repository** containing generated `Library/`, `Logs/` and IDE files; plan for significant disk usage.
2. Open the folder with Unity Hub and select **2020.1.4f1** where practical.
3. Allow Unity to import assets and review missing/obsolete package warnings.
4. Open the game scene in `Assets/Scenes/` and inspect the Build Settings scene list before trying Play mode.
5. Confirm imported third-party assets and package licences before redistributing or rebuilding.

A current build has not been validated. Unity upgrades can introduce compatibility changes.

## Technical observations and limitations

- Some inventory operations assume UI objects and singleton references are present, so null references should be tested.
- Inventory stack handling and full-inventory behaviour merit edge-case tests.
- Enemy behaviour requires valid NavMesh baking, layers and scene references.
- Generated Unity and IDE artifacts are committed. A careful repository-size cleanup would require inspection before deleting anything.
- Several script directories/assets appear to incorporate third-party systems; do not interpret every file in this repository as original work by one developer.

## QA / testing ideas

Try movement collisions, jumping near ledges, inventory overflow, stacking limits, drag/drop on incompatible slots, item duplication/loss, enemy detection near range boundaries, scene transitions and input focus while inventory is open. Record expected/actual results and evidence.

See [Interview and QA notes](docs/INTERVIEW_PREP.md). They are test design ideas, **not claims of executed tests**.

## Portfolio context

An academic-era game development exercise that demonstrates interaction between multiple gameplay subsystems, useful for discussing debugging, state transitions, integration testing and practical technical debt.