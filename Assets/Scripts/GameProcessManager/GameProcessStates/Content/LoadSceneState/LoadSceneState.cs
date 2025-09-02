using UnityEngine;

[CreateAssetMenu(fileName = "LoadSceneState", menuName = "Game Process States/Load Scene State")]
public class LoadSceneState : SuperState {

    public SceneField NextScene;

    override public GameProcessControllerFactory ControllerFactory => controllerFactory;

    [SerializeField]
    private LoadSceneStateFactory stateFactory;
    override public IGameProcessStateFactory StateFactory => stateFactory;

    override public void OnStateEnter(IGameProcessManager manager) {
        base.OnStateEnter(manager);
        stateFactory.CurrentState = stateFactory.LoadSceneEnterState;
        controllerFactory.PlayerInputController.DisableAll();
        controllerFactory.ScenesContoller.SaveCurrentPlayerScene(NextScene);
        StateFactory.CurrentState.OnStateEnter(this);
    }

    public void OnEnable() {
        if (stateFactory != null) return;
        stateFactory = CreateInstance<LoadSceneStateFactory>();
    }
}
