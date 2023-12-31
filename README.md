# CSE 450A Final Project  Submarine Adventure Game

## Demo
![image](https://github.com/YingXu001/CSE-450A-Final-Project/assets/50728665/74e11bfa-014e-45cb-951b-ae100fe4b1f5)

## Description
In this game, players navigate a submarine through challenging underwater environments. Each level is filled with obstacles and hostile sea creatures. Players must find the exit portal, resembling a well, to progress. Ammo is limited, and players can engage in combat with fish and sea monsters. Strategic use of resources and managing enemy encounters are key to survival.

## Controls
- Move Forward: `W`
- Move Backward: `S`
- Rotate Left: `A`
- Rotate Right: `D`
- Shoot: Left Mouse Button
- Tracking Torpedo: `Space`
- Change Ammo Type: Right Mouse Button
- Change Speed (slow, medium, fast): `1`, `2`, `3`
- Mecha Form: Ultimate - `R`
- Pause Menu: `ESC`

## User Interface
- **Start Menu**: Options to play the game, adjust volume, and toggle full-screen mode.
- **Instruction Pages**: Two pages accessible after clicking the play button.
- **In-Game Death Menu**: Allows restarting the level or returning to the main menu.
- **Pause Menu**: Access controls, change settings, or continue the game.
- **Victory Screen**: Appears after beating the final boss, with an option to return to the start menu.

## Enemies
- **FishEnemy**: -10 health, +30 energy on hit. Destroyed with 1 bullet.
- **MonsterEnemy1 (Grey)**: -20 health, +20 energy on hit. Takes 3 bullets to destroy. Shoots randomly.
- **MonsterEnemy2 (Green)**: -15 health, +15 energy on hit. Takes 3 bullets to destroy. Shoots directly at the submarine.
- **BossEnemy**: -50 health on hit. Attacks in eight directions.
- **FinalBossEnemy**: -50 health on hit. Attacks in eight directions, appears randomly. Spawns small enemies when at half health.

## Resources
- **Red Dot**: +2 ammo.
- **Crystal Orb**: +15 health.
- **Shield**: Restores shield if deactivated.
- **Green Circle (Final Boss Level)**: 15 seconds of infinite ammo.
- **Torpedo**: +5 torpedoes.

## Additional Features
- **Shield**: Blocks the first hit, then deactivates.
- **Energy System**: Destroy enemies to gain energy. At 100 energy, transform into Mecha: slower speed, doubled health. Ultimate consumes 50 energy. Transforms back to submarine at 0 energy.
- **Torpedo Use**: Limited to 5 per level, with pickups only in the final boss level.

## Play the Game
Dive into the adventure [here](https://yingxu.itch.io/450finalproject)!

Enjoy navigating the treacherous depths and defeating formidable sea monsters in your submarine adventure!
