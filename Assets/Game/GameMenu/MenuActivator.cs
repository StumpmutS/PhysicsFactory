using UnityEngine;

public class MenuActivator : MonoBehaviour
{
    [SerializeField] private EGameMenuType menuType;

    public void Activate()
    {
        GameMenuManager.Instance.ActivateMenu(menuType);
    }
    
    public void Deactivate()
    {
        GameMenuManager.Instance.DeactivateMenu(menuType);
    }
}