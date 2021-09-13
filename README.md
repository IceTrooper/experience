# experience-recruitment
Infinite One-button Rotational Shooting Game in Cardboard

# Installation
Unpack .zip and run .exe file

# Mechanics
The game is inspired by the movies from the Matrix series.

## Instructions
- Rotate with the Cardboard
- Touch the screen to shoot a projectile
- Shoot the enemies that appear
- Protect your three bases as they automatically heal you.
- When you lose your main life the game ends

## Tips
- For three minutes the difficulty level increases to the maximum (the frequency of appearance of enemies increases)
- First protect towers, then defend yourself

# Main features
- Cardboard VR support
- Project architecture based on ScriptableObjects (modular, loosy coupled system for interaction between components)
- Enemy spawning system is prepared for many more types of enemies
- Damagable system
- Score system
- Smooth UI system with animations based on tweening
- ProBuilder used to prototype many things
- Pool system
- Enemies animations are ready to implement
- Mouse support for better testing in Editor

# Possible improvements (and things I didn't have time to add)
Music and sounds

I wanted to add three types of opponents:
- Angry PC - implemented
- Small robot - faster, but dealing less damage with short delay
- Worm - spawned on walls, big damage, more health, focused on player (not towers)

More shader/effect/particles:
- Bullet shader
- TV hologram shader
- Particles when tower is destroyed
- Black walls (fog) on platform

# Some developer thoughts
- I tried to design a modular system for interaction between components. This system is located in Utilities->Atoms. The components were also created with a single purpose in mind.
- There are no Singletons or static classes (with temporary data) in the project, so the project is easily testable. I try to avoid them for this very reason.

# Classes
All classes are located in the Scripts folder:

SO - ScriptableObject
SMB - StateMachineBehavior

## Directories
### Common
- Damagable - adds the ability to receive damage
- Poolable - added to an object allows it to be managed by Pooler
- Pooler - responsible for creating objects or using old ones
- Scorable - adds the ability to trigger an event for getting points

### Editor
- RoundSpawnerEditor - editor script for RoundSpawnerEditor; displaying helpful visual Handles

### Enemy
- EnemyBehavior - main script for simple enemy behavior
- EnemySpawnData (SO) - data for spawning enemy, contains prefab and delay
- HackingSMB (SMB) - controls flow of hacking (dealing damage) operation by enemy
- RoundSpawner - spawns enemies on platform on random position inside annulus

### Managers
- CardboardStartup - main script for controlling Cardboard VR (copied from sample)
- InputController - controls if any input triggered, contains mouse support for easier testing in Editor
- InputReader - layer of abstraction between InputController and receiving inputs, contains actions
- LevelController - controls level difficulty over time and game over state

### Player
- BasicGun - Gun shooting with simple balls
- Bullet - script for Bullet prefab
- DefensiveTower - heals Player over time
- Gun - basic abstract class for different types of guns
- PlayerController - main script resposible for Player behavior

### UI
- UI_DisplayInt - Display Int variable with optional delay
- UI_DisplayTime - Display time since level load
- UI_GameOver - Game over overlay
- UI_HealthBar - controls HealthBar, receive events and has optional delay with second damage bar
- UI_Points - Display points in World coordinates with scale animation above enemies

### Utilities
Atoms directory contains ScriptableObject architecture (events and runtime lists)

- ProjectConstants - static class with constants
- RandomizedElement - wrapper class for T
- RandomizedList - base class for all Randomized lists
- RandomList - class for retreiving random object
- WeightedList - class for tereiving random objects based on their weights