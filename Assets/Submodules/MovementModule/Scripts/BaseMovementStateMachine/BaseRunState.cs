using UnityEngine;

public class BaseRunState : BaseState {

    private float cummulativeTime; 

    override public void UpdateState(BaseStateManager manager) {
        if (Mathf.Abs(manager.characterController.Forward) < 0.1) {
            manager.SwitchState(manager.idleState);
            return;
        }
        cummulativeTime = manager.characterController.PhysicVelocity < 0.75 ? cummulativeTime + Time.deltaTime : 0;
        if (!manager.characterController.IsRunning || cummulativeTime > manager.animator.FadeToRunTime) {
            manager.SwitchState(manager.moveState);
            return;
        }
    }

    public override void Act(BaseStateManager manager) {
        manager.characterController.Run();
        manager.animator.UpdateMoveAnimationSpeed(manager.characterController.MoveSpeed);
    }

    public override void LateAct(BaseStateManager manager) {
        manager.animator.LateUpdateRunAnimation();
    }

    public override void OnStateEnter(BaseStateManager manager) {
        manager.animator.StartRunAnimation();
    }

    public override void OnStateExit(BaseStateManager manager) {
    }
}
