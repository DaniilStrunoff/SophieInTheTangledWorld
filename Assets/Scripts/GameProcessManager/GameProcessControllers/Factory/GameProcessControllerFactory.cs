using UnityEngine;


public class GameProcessControllerFactory : MonoBehaviour, IGameProcessControllerFactory {

    [SerializeField]
    private TimeScaleController timeScaleController; 
    public TimeScaleController TimeScaleController => timeScaleController;

    [SerializeField]
    private ScenesContoller scenesContoller;
    public ScenesContoller ScenesContoller => scenesContoller;

    [SerializeField]    
    private PauseMenuController pauseMenuController;
    public PauseMenuController PauseMenuController => pauseMenuController;

    [SerializeField]
    private PauseMenuFadeController pauseMenuFadeConroller;
    public PauseMenuFadeController PauseMenuFadeController => pauseMenuFadeConroller;

    [SerializeField]
    private PauseMenuLocalizationFadeController pauseMenuLocalizationFadeController;
    public PauseMenuLocalizationFadeController PauseMenuLocalizationFadeController => pauseMenuLocalizationFadeController;

    [SerializeField]
    private PauseMenuDefaultFadeController pauseMenuDefaultFadeController;
    public PauseMenuDefaultFadeController PauseMenuDefaultFadeController => pauseMenuDefaultFadeController;

    [SerializeField]
    private PauseMenuScreenFadeController pauseMenuScreenFadeController;
    public PauseMenuScreenFadeController PauseMenuScreenFadeController => pauseMenuScreenFadeController;

    [SerializeField]
    private PauseMenuDialogsFadeController pauseMenuDialogsFadeController;
    public PauseMenuDialogsFadeController PauseMenuDialogsFadeController => pauseMenuDialogsFadeController;

    [SerializeField]
    private PauseMenuAudioFadeController pauseMenuAudioFadeController;
    public PauseMenuAudioFadeController PauseMenuAudioFadeController => pauseMenuAudioFadeController;

    [SerializeField]
    private MainMenuController mainMenuController;
    public MainMenuController MainMenuController => mainMenuController;

    [SerializeField]
    private MainMenuFadeController mainMenuFadeController;
    public MainMenuFadeController MainMenuFadeController => mainMenuFadeController;

    [SerializeField]
    private MainMenuConfirmationFadeController mainMenuConfirmationFadeController;
    public MainMenuConfirmationFadeController MainMenuConfirmationFadeController => mainMenuConfirmationFadeController;

    [SerializeField]
    private MainMenuDefaultFadeController mainMenuDefaultFadeController;
    public MainMenuDefaultFadeController MainMenuDefaultFadeController => mainMenuDefaultFadeController;

    [SerializeField]
    private GameplayFadeConroller gameplayFadeController;
    public GameplayFadeConroller GameplayFadeController => gameplayFadeController;

    [SerializeField]
    private SceneFadeController sceneFadeController;
    public SceneFadeController SceneFadeController => sceneFadeController;

    [SerializeField]
    private DialogController dialogController;
    public DialogController DialogController => dialogController;

    [SerializeField]
    private DialogFadeController dialogFadeController;
    public DialogFadeController DialogFadeController => dialogFadeController;

    [SerializeField]
    private AutoButtonController autoButtonController;
    public AutoButtonController AutoButtonController => autoButtonController;

    [SerializeField]
    private DialogContinueButtonFadeController dialogContinueButtonFadeController;
    public DialogContinueButtonFadeController DialogContinueButtonFadeController => dialogContinueButtonFadeController;

    [SerializeField]
    private PlayerInputController playerInputController;
    public PlayerInputController PlayerInputController => playerInputController;

    [SerializeField]
    private AudioController audioController;
    public AudioController AudioController => audioController;
}
