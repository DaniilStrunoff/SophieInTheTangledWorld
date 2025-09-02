using UnityEngine;


public class LookAtManagerControllerFactory : MonoBehaviour, ILookAtManagerControllerFactory {
    [SerializeField]
    private LookAtController lookAtController; 
    public LookAtController LookAtController {get => lookAtController; set => lookAtController = value;}
}
