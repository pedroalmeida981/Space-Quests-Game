# Interview preparation and QA review

> These are source-review notes and **suggested** test scenarios. They do not represent executed test cases, successful builds or approved production requirements.

## Project in one sentence

Unity C# first-person exploration and inventory systems.

## Technical interview questions and grounded answers

### 1. How does enemy AI change behaviour?

EnemyAi checks detection and attack ranges: patrol when the player is outside, chase when in sight, attack when within both ranges. Unity NavMeshAgent supplies pathfinding.

### 2. How does inventory management work?

The Inventory script manages slots, stackable quantities, dragging between slots and item types. It also toggles player controls/cursor while the interface is displayed.

### 3. What would you test at system boundaries?

Inventory full conditions, maximum stack size, dragging into incompatible slots, scene changes, missing object references, enemy detection edges, and asset load issues.

### 4. Are all scripts original?

The repository has various asset/plugin directories. Without attribution history it would be inaccurate to claim that every script or asset was written personally; distinguish custom work from external content.

### 5. Why is the repo so large?

Historical generated Unity and IDE directories were committed. Cleanup would involve checking Unity's recommended tracked inputs, licences and large files before removing items.

## Suggested functional and accessibility test cases

| ID | Scenario | Expected / verification objective |
| --- | --- | --- |
| INV-01 | Fill all available inventory slots then pick up another item | Item is not silently lost; intended overflow response occurs |
| INV-02 | Stack items at maximum capacity | Counts stay within maximum and remainder is handled |
| INV-03 | Drag item to incompatible slot | Invalid move is blocked and state remains intact |
| AI-01 | Cross enemy sight and attack range boundaries | State transitions follow defined detection rules |
| CTRL-01 | Open inventory during movement | Player control/cursor states are consistent |
| GAME-01 | Restart or change scene | Managers, items and HUD initialise correctly |
| A11Y-01 | Inspect subtitles/UI contrast, remapping and menu keyboard navigation | Accessibility risks and unsupported features are accurately documented |

## Limitations to disclose

Unity editor version is 2020.1.4f1; do not claim modern compatibility, successful build, complete playthrough or independently verified performance.

## How to discuss the project

Explain what the code currently contains, which components you actually developed, the difference between intent and implementation, one defect or risk you identified, and how you would validate a fix. Avoid claiming tests have passed unless you can provide reproducible evidence.

## Suggested next improvements

- Verify a clean checkout/build with a documented environment.
- Record reproducible bugs or missing features as GitHub Issues.
- Add evidence-backed screenshots and a short demo only after confirming they match the actual project.
- Add focused automated tests where the architecture makes them worthwhile; keep manual exploratory and accessibility testing visible.
