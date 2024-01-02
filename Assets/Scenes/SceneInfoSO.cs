using UnityEngine;

[CreateAssetMenu(menuName = "Defaults/SceneInfo")]
public class SceneInfoSO : ScriptableObject
{
    [SerializeField] private int sceneIndex;
    public int SceneIndex => sceneIndex;
}