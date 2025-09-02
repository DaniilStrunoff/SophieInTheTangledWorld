using UnityEngine;


[CreateAssetMenu(fileName = "MainMenuDefaultStayState", menuName = "Game Process States/Main Menu SubStates/Main Menu Stay SubStates/Main Menu Default SubStates/Main Menu Default Stay State")]
public class MainMenuDefaultStayState : GameProcessBaseState {

    override public void UpdateState(IGameProcessManager manager) {}

    override public void OnStateEnter(IGameProcessManager manager) {}

    override public void OnStateExit(IGameProcessManager manager) {
        manager.ControllerFactory.PlayerInputController.SetSelectedGameObjectToNull();
    }

    override public void Act(IGameProcessManager manager) {
        manager.ControllerFactory.PlayerInputController.SetSelectedGameObjectOnDeviceChange(
            manager.ControllerFactory.PlayerInputController.LastUsedDevice,
            manager.ControllerFactory.MainMenuController.DefaultMenuSelectedObject
        );
    }
}
