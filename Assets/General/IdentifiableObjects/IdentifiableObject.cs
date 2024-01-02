using UnityEngine;
using UnityEngine.Serialization;

public class IdentifiableObject : MonoBehaviour, ISaveable<IdentifiableObjectSaveData>, ILoadable<IdentifiableObjectSaveData>
{
    public int Id { get; private set; } = -1;

    private void Start()
    {
        if (Id == -1) Id = ObjectIdManager.Instance.AddObject(this);
    }

    private void OnDestroy()
    {
        ObjectIdManager.Instance.RemoveObject(this);
    }

    public void Save(IdentifiableObjectSaveData data, AssetRefCollection _)
    {
        data.Id = Id;
    }

    public LoadingInfo Load(IdentifiableObjectSaveData data, AssetRefCollection _)
    {
        Id = data.Id;
        ObjectIdManager.Instance.IdentifyObject(this, Id);

        return LoadingInfo.Completed(Id, ELoadCompletionStatus.Succeeded);
    }
}