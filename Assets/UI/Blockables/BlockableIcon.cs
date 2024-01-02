using UnityEngine;
using UnityEngine.UI;
using Utility.Scripts;

public class BlockableIcon : MonoBehaviour, IUIBlockable, IInitializableComponent<IconController>
{
    [SerializeField] private IconController icon;

    public void Init(IconController iconPrefab)
    {
        icon = Instantiate(iconPrefab, transform, false);
    }
    
    public void Block(UIBlockableInfo info)
    {
        icon.SetIcon(info.MainIcon);
    }

    public void Unblock()
    {
        icon.ResetIcon();
    }
}
