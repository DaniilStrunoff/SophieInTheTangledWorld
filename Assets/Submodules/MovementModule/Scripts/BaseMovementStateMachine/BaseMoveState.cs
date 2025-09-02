using UnityEngine;

public class BaseMoveState : BaseState {
    override public void UpdateState(BaseStateManager manager){
        if (Mathf.Abs(manager.characterController.Forward) < 0.1) {
            manager.SwitchState(manager.idleState);
            return;
        }
        if (manager.characterController.IsRunning && manager.characterController.PhysicVelocity >= 0.85) { 
            manager.SwitchState(manager.runState);
            return;
        }
    }

    public override void Act(BaseStateManager manager) {
        manager.characterController.Move();
        manager.animator.UpdateMoveAnimationSpeed(manager.characterController.MoveSpeed);
    }

    public override void LateAct(BaseStateManager manager) {
        manager.animator.LateUpdateMoveAnimation();
    }

    public override void OnStateEnter(BaseStateManager manager) {
        manager.animator.StartMoveAnimation();
    }

    public override void OnStateExit(BaseStateManager manager) {
    }

}