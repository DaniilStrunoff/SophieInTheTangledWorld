using UnityEngine;

public class BaseIdleState : BaseState {
    override public void UpdateState(BaseStateManager manager) {
        if (Mathf.Abs(manager.characterController.Forward) > 0.1) {
            if (manager.characterController.IsRunning) manager.SwitchState(manager.runState);
            else manager.SwitchState(manager.moveState);
            return;
        }
    }

    public override void Act(BaseStateManager manager) {
        manager.characterController.Idle();
    }

    public override void LateAct(BaseStateManager manager) {
        manager.animator.LateUpdateIdleAnimation();
    }

    public override void OnStateEnter(BaseStateManager manager) {
        manager.animator.StartIdleAnimation();
    }

    public override void OnStateExit(BaseStateManager manager) {
    }
}
