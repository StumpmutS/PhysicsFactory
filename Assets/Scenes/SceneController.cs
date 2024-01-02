using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void SetScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void SetScene(SceneInfoSO sceneInfoSo)
    {
        SetScene(sceneInfoSo.SceneIndex);
    }
}
