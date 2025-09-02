# [Sophie in the Tangled World](https://store.steampowered.com/app/3769000/Sophie_in_the_Tangled_World_Demo/) - Level 1 Showcase

[![Unity](https://img.shields.io/badge/Made%20with-Unity-000000?style=for-the-badge&logo=unity&logoColor=white)](https://unity.com/)
[![Steam](https://img.shields.io/badge/Steam-Play%20Demo-1b2838?style=for-the-badge&logo=steam&logoColor=white)](https://store.steampowered.com/app/3769000/Sophie_in_the_Tangled_World_Demo/)


A repository for the first level of the puzzle adventure [**Sophie in the Tangled World**](https://store.steampowered.com/app/3769000/Sophie_in_the_Tangled_World_Demo/). The goal is to showcase code structure and app architecture on a compact slice.

## Architecture

The project uses a Hierarchical State Machine (HSM). The entire game behaviour is defined by the current state.

This approach allows you to explicitly define the system's behavior at any given moment,  
removing the risk of unexpected side effects and making the game flow easy to reason about.

Start exploring the architecture from file:
  ```
  Assets/Scripts/GameProcessManager/GameProcessManager.cs
  ```

Each game state controls the game via a set of controllers located in
  ```
  Assets/Scripts/GameProcessManager/GameProcessControllers/Content
  ```
For example, the `TimeScaleController` exposes the method `SetTimeScaleToZero`, which is called in the `PauseMenuState` to pause gameplay.

Controllers provide a set of events that allow states to react to game conditions and transition from one state to another.
For instance, the `PauseMenuController` provides a `PauseEvent` so that the `GameplayState` knows when to exit into the pause menu.

## Quick Start

Entry point scene:
   ```
   Assets/Scenes/MainMenu/Scenes/MainMenu.unity
   ```

## Contact

If you feel that my skills could be useful for your team or project, feel free to reach out: 

[![Telegram](https://img.shields.io/badge/Telegram-2CA5E0?style=for-the-badge&logo=telegram&logoColor=white)](https://t.me/DaniilStrunov)
[![Email](https://img.shields.io/badge/Email-daniil.strunoff%40gmail.com-EA4335?style=for-the-badge&logo=gmail&logoColor=white)](mailto:daniil.strunoff@gmail.com)
