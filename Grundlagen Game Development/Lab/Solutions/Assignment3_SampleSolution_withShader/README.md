# Grundlagen Game Development Engine

![C#](https://img.shields.io/badge/C%23-9cf?style=for-the-badge&logo=c-sharp)
![SFML](https://img.shields.io/badge/SFML-9cf?style=for-the-badge&logo=sfml)
![License: FHS](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge)

A game development engine built using C# and SFML. It includes various game objects, managers, and utilities to create a simple game.

## Table of Contents

- [Grundlagen Game Development Engine](#grundlagen-game-development-engine)
  - [Table of Contents](#table-of-contents)
  - [Project Structure](#project-structure)
  - [Key Components](#key-components)
    - [Game Objects](#game-objects)
    - [Managers](#managers)
    - [Utilities](#utilities)
  - [Getting Started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Building the Project](#building-the-project)
    - [Running the Game](#running-the-game)
  - [License](#license)

## Project Structure
## Key Components

### Game Objects

- [`CrystalSpawner`](GameObjects/CrystalSpawner.cs): Spawns crystals in the game.
- [`Player`](GameObjects/Player.cs): Represents the player character.
- [`ProjectileEmitter`](GameObjects/ProjectileEmitter.cs): Emits projectiles.
- [`Hud`](GameObjects/Hud.cs): Displays the heads-up display.
- [`Obstacle`](GameObjects/Obstacle.cs): Represents obstacles in the game.
- [`Projectile`](GameObjects/Projectile.cs): Represents projectiles in the game.

### Managers

- [`AssetManager`](AssetManager.cs): Manages game assets like sounds, music, and shaders.
- [`InputManager`](InputManager.cs): Handles input from the player.
- [`PhysicsManager`](PhysicsManager.cs): Manages physics calculations and collisions.

### Utilities

- [`HighscoreParser`](HigscoreParser.cs): Reads and writes high scores to a file.
- [`Utils`](Utils.cs): Contains utility functions used throughout the project.
- [`WorldParser`](WorldParser.cs): Parses the game world from a file.

## Getting Started

### Prerequisites

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SFML.Net](https://www.sfml-dev.org/download/sfml.net/)

### Building the Project

1. Clone the repository:
    ```sh
    git clone https://github.com/yourusername/grundlagen-game-development-engine.git
    cd grundlagen-game-development-engine
    ```

2. Restore the dependencies:
    ```sh
    dotnet restore
    ```

3. Build the project:
    ```sh
    dotnet build
    ```

### Running the Game

1. Run the project:
    ```sh
    dotnet run --project GGD_Template.csproj
    ```


## License

This project is licensed by FH Salzburg.
