using UnityEngine;


[CreateAssetMenu(fileName = "LookAtNullState", menuName = "Look At States/Look At Null State")]
public class LookAtNullState : LookAtBaseState {
    override public void UpdateState(ILookAtManager manager) {}

    override public void OnStateEnter(ILookAtManager manager) {}

    override public void OnStateExit(ILookAtManager manager) {}

    override public void Act(ILookAtManager manager) {}
}
