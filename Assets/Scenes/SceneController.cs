using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void SetScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
