using UnityEngine;

public abstract class DolboidListener : MonoBehaviour
{
    [SerializeField] private Dolboid dolboid;

    private void Awake()
    {
        dolboid.OnDolboidChanged += HandleDolboidChanged;
    }

    protected abstract void HandleDolboidChanged(DolboidInfo info);
}