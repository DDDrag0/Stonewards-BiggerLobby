# Bigger Lobby for Stonewards

This mod increases the maximum lobby size from 4 to 20 players.  

## Requirements

- [BepInEx 5.4.23.5+](https://github.com/BepInEx/BepInEx/releases)

## Installation

1. **Install BepInEx**  
   - Download the latest **BepInEx 5.x** from the [releases page](https://github.com/BepInEx/BepInEx/releases).  
   - Extract all files into your `Stonewards` game folder (where `Stonewards.exe` is located).  
   - Run the game once to let BepInEx generate its configuration files, then close the game.

2. **Install the mod**  
   - Copy `BiggerLobbyMod.dll` from [releases](https://github.com/DDDrag0/Stonewards-BiggerLobby/releases) into `BepInEx/plugins/` inside the game folder.

3. **Launch the game** – the mod will be loaded automatically.

## Building from source (optional)

If you want to compile the mod yourself:

- Clone the repository.
- Open a terminal in the project folder.
- Run the build command, specifying the path to your game's `Managed` folder:

```bash
dotnet build -c Release -p:GameManagedDir="C:\Program Files (x86)\Steam\steamapps\common\Stonewards\Stonewards_Data\Managed\"