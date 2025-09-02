using UnityEngine;


public class InteractionManagerControllerFactory : MonoBehaviour, IInteractionManagerControllerFactory {

    [SerializeField]
    private UIElementController uielementController; 
    public UIElementController UIElementController => uielementController;

    [SerializeField]
    private InteractableMarkController interactableMarkController; 
    public InteractableMarkController InteractableMarkController => interactableMarkController;
}
