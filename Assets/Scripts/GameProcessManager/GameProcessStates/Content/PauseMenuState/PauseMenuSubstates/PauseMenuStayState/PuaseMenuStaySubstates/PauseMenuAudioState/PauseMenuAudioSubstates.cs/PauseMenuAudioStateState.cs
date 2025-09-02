using UnityEngine;


[CreateAssetMenu(fileName = "PauseMenuAudioStayState", menuName = "Game Process States/Pause Menu SubStates/Pause Menu Stay SubStates/Pause Menu Audio SubStates/Pause Menu Audio Stay State")]
public class PauseMenuAudioStayState : GameProcessBaseState {

    override public void UpdateState(IGameProcessManager manager) {}

    override public void OnStateEnter(IGameProcessManager manager) {}

    override public void OnStateExit(IGameProcessManager manager) {
        manager.ControllerFactory.PlayerInputController.SetSelectedGameObjectToNull();
    }

    override public void Act(IGameProcessManager manager) {
        manager.ControllerFactory.PlayerInputController.SetSelectedGameObjectOnDeviceChange(
            manager.ControllerFactory.PlayerInputController.LastUsedDevice,
            manager.ControllerFactory.PauseMenuController.AudioMenuSelectedObject
        );
    }
}
