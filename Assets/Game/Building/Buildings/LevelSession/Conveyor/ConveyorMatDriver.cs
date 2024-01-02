using UnityEngine;

public class ConveyorMatDriver : MonoBehaviour
{
    [SerializeField] private MaterialManager materialManager;
    [SerializeField] private Transform scaleReference;
    [SerializeField] private Conveyor conveyor;

    private Material _material;
    private float Speed => conveyor == null ? 0 : conveyor.CurrentSpeed;
    private static readonly int FixedTimeProperty = Shader.PropertyToID("_FixedTime");
    private static readonly int SpeedProperty = Shader.PropertyToID("_Speed");
    private static readonly int ScaleProperty = Shader.PropertyToID("_Scale");

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
        materialManager.ModifyMaterial(ScaleProperty, 
            (id, material) => material.SetFloat(id, scaleReference.localScale.z));
        materialManager.ModifyMaterial(SpeedProperty,
            (id, material) => material.SetFloat(id, Speed));
    }

    private void FixedUpdate()
    {
        materialManager.ModifyMaterial(FixedTimeProperty, (id, material) => material.SetFloat(id, Time.fixedTime));
    }

    private void OnDestroy()
    {
        if (conveyor != null) conveyor.OnSpeedChanged.RemoveListener(SetMaterial);
    }
}