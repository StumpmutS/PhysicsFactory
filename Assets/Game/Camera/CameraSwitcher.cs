using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    private static readonly int Inspecting = Animator.StringToHash("Inspecting");

    public void Inspect(bool value)
    {
        animator.SetBool(Inspecting, value);
    }
}
