using UnityEngine;

public class ConveyorMatDriver : MonoBehaviour
{
    [SerializeField] private MaterialManager materialManager;
    [SerializeField] private Transform scaleReference;
    [SerializeField] private Conveyor conveyor;

    private Material _material;
    private static readonly int FixedTime = Shader.PropertyToID("_FixedTime");
    private static readonly int Speed = Shader.PropertyToID("_Speed");
    private static readonly int Scale = Shader.PropertyToID("_Scale");

    private void Awake()
    {
        if (conveyor == null) return;
        conveyor.OnSpeedChanged.AddListener(SetMaterial);
    }

    private void Start()
    {
        SetMaterial();
    }

    public void UpdateMaterial()
    {
        SetMaterial();
    }
    
    private void SetMaterial()
    {
        materialManager.ModifyMaterial(Scale, 
            (id, material) => material.SetFloat(id, scaleReference.localScale.z));
        materialManager.ModifyMaterial(Speed,
            (id, material) => material.SetFloat(id, conveyor == null ? 0 : conveyor.CurrentSpeed));
    }

    private void FixedUpdate()
    {
        materialManager.ModifyMaterial(FixedTime, (id, material) => material.SetFloat(id, Time.fixedTime));
    }

    private void OnDestroy()
    {
        if (conveyor != null) conveyor.OnSpeedChanged.RemoveListener(SetMaterial);
    }
}