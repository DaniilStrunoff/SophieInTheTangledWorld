using UnityEngine;


[CreateAssetMenu(fileName = "PauseMenuLocalizationStayState", menuName = "Game Process States/Pause Menu SubStates/Pause Menu Stay SubStates/Pause Menu Localization SubStates/Pause Menu Localization Stay State")]
public class PauseMenuLocalizationStayState : GameProcessBaseState {

    override public void UpdateState(IGameProcessManager manager) {}

    override public void OnStateEnter(IGameProcessManager manager) {}

    override public void OnStateExit(IGameProcessManager manager) {
        manager.ControllerFactory.PlayerInputController.SetSelectedGameObjectToNull();
    }

    override public void Act(IGameProcessManager manager) {
        manager.ControllerFactory.PlayerInputController.SetSelectedGameObjectOnDeviceChange(
            manager.ControllerFactory.PlayerInputController.LastUsedDevice,
            manager.ControllerFactory.PauseMenuController.LocalizationMenuSelectedObject
        );
    }
}
