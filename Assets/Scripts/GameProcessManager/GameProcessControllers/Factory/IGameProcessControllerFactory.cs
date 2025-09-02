public interface IGameProcessControllerFactory {
    public TimeScaleController TimeScaleController {get;}
    public ScenesContoller ScenesContoller {get;}
    public PauseMenuController PauseMenuController {get;}
    public PauseMenuFadeController PauseMenuFadeController {get;}
    public GameplayFadeConroller GameplayFadeController {get;}
    public SceneFadeController SceneFadeController {get;}
    public DialogController DialogController {get;}
    public AudioController AudioController {get;}
    public PlayerInputController PlayerInputController {get;}
}
