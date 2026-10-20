# 😈 Karma Defence

<div align="center">

🇺🇸 English | [🇰🇷 한국어](./README.ko.md)

</div>

A defense game where you fight off demons to protect a stone statue  
Solely responsible for all programming — including object pooling, quest system, and custom editor

[![Karma Defence Demo](https://img.youtube.com/vi/o5AaV3zBt2k/0.jpg)](https://www.youtube.com/watch?v=o5AaV3zBt2k)

[▶ Google Play Store](https://play.google.com/store/apps/details?id=com.nineteengames.karmadefence&hl=en_US&gl=US) `Service Termination (Android Version Not Supported)`


| | |
|------|------|
| Genre | Tower Defense |
| Engine | Unity (C#) |
| Platform | Android |
| Period | Jul 2020 – Mar 2021 |

## Key Features

- **Object Pooling**: Multiple unit types are interleaved under a single parent object and activated via an index formula, improving memory efficiency
- **Quest System**: Quest completion per stage is persisted via PlayerPrefs with duplicate prevention logic to ensure star count integrity
- **Custom Editor**: Hides irrelevant inspector fields based on unit type (melee / ranged) and displays numeric values as sliders for faster iteration
- **Permanent & Temporary Upgrades**: Permanent upgrades are unlocked by accumulated quest stars; temporary upgrades are purchased with in-stage currency
- **Three-Resource Loop**: Time-regenerating currency buys units and skills, kill gold buys in-stage upgrades, and quest stars buy permanent upgrades across stages
- **Unit Combat AI**: A single component drives both melee and ranged units; each unit targets only the nearest enemy ahead of it and checks range as `attack range + target size`
- **Release Log Stripping**: A global `Debug` wrapper marks `Log` with `[Conditional("UNITY_EDITOR")]`, so log calls are removed from builds at compile time

## Code Structure

```
Karma-Defence-Codes/
├── Common/         # Units (movement, combat, health, spawn pooling) and damage (projectiles, skills)
├── Editor/         # Custom inspector per unit type
├── Manager/        # Game flow & quests, skills, scene transitions
├── UI/             # Upgrade buttons, currency, stage select, sound and other UI
├── PlayerMove.cs   # Touch movement for the archangel
├── FollowCam.cs    # Camera follow logic
├── NameSpace.cs    # Shared data structure definitions
├── Debug.cs        # Release-safe debug wrapper
├── enableOff.cs    # Object activation control
└── readOnly.cs     # ReadOnly custom attribute for inspector
```

## Implementation Details

### 1. Multi-Type Object Pooling

A pooling system that efficiently manages multiple unit types under a single parent object.

**Core Design**

- On initialization, units are instantiated in alternating order (e.g. 3 types → `0 1 2 0 1 2 ...`)
- On spawn, the system starts at the requested unit's index and steps by the number of unit types, checking only slots of the same type for an inactive one
- If the pool is exhausted, its size doubles dynamically via reallocation

Spawn cooldowns are managed with a single 1D array using the formula:  
`spawnTime[(arrayLength / unitTypes) * unitIndex + (currentStage - 1)]`  
This maps each combination of unit type and stage to a unique cooldown without nested structures.

### 2. Per-Stage Quest System

Up to 3 quests are available per stage, and completing them awards stars (★).

**Core Design**

- Quest completion is persisted via `PlayerPrefs`
- Key format: `stageNumber + questIndex` (quest index starts at 0, e.g. Stage 1, third quest → `"12"`)
- Duplicate completion is prevented to maintain accurate star counts

**Quest Types (10)**: stage clear, units spawned, kills (total / archangel skill / warrior / archer), skill hits on the boss, upgrade count, statue health kept, units kept alive

The kill cause is counted from the damage source passed with each hit (`"Sword"`, `"Bow"`, `"Skill"`), based on the final blow.

### 3. Unit Type–Based Custom Editor

Irrelevant inspector fields are automatically hidden based on unit type (`Shooter` / `Fighter`), reducing editor clutter.

| Unit Type | Visible Fields |
|-----------|----------------|
| Shooter (Ranged) | Projectile, ShootPos |
| Fighter (Melee) | AttackSound |
| Common | Damage, AttackRange, AttackSpeed, Speed, IncHp, IncDam, DecAttSp |

Shared numeric values are displayed as sliders for intuitive editing.

**How to Use**

1. Select the unit type from the `Class` field on the `Unit` script
2. Only the relevant fields for that type will appear in the inspector
3. Numeric values such as Damage can be adjusted directly via slider

## Technical Document

Internal design details (multi-type unit pooling, quest & star economy, two-tier upgrades, unit combat AI, editor tools) are documented in the [technical document](docs/TechnicalDesign.md) (Korean).

## License

[MIT License](LICENSE)
