using UnityEngine;

public abstract class BuildingPreview : MonoBehaviour
{
    [SerializeField] protected GameObject buildingPrefab;
#pragma warning disable CS0108, CS0114
    [SerializeField] private Renderer renderer;
#pragma warning restore CS0108, CS0114
    [SerializeField] private ColorInfo previewColors;

    public abstract void StretchTo(Vector3 origin, Vector3 destination, int cellSize);

    public abstract void Place();
    
    public void Pass()
    {
        SetMeshColor(previewColors.Colors[0]);
    }

    public void Deny()
    {
        SetMeshColor(previewColors.Colors[1]);
    }

    private void SetMeshColor(Color color)
    {
        var mats = renderer.materials;
        mats[0].color = color;
        renderer.materials = mats;
    }
}