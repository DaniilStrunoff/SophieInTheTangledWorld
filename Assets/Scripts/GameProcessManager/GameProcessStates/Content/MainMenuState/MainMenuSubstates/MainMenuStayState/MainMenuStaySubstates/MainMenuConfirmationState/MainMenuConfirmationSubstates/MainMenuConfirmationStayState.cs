using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "MainMenuConfirmationStayState", menuName = "Game Process States/Main Menu SubStates/Main Menu Stay SubStates/Main Menu Confirmation SubStates/Main Menu Confirmation Stay State")]
public class MainMenuConfirmationStayState : GameProcessBaseState {

    override public void UpdateState(IGameProcessManager manager) {}

    override public void OnStateEnter(IGameProcessManager manager) {}

    override public void OnStateExit(IGameProcessManager manager) {
        manager.ControllerFactory.PlayerInputController.SetSelectedGameObjectToNull();
    }

    override public void Act(IGameProcessManager manager) {
        manager.ControllerFactory.PlayerInputController.SetSelectedGameObjectOnDeviceChange(
            manager.ControllerFactory.PlayerInputController.LastUsedDevice,
            manager.ControllerFactory.MainMenuController.ConfirmationMenuSelectedObject
        );
    }
}
