using UnityEngine;

public class ChainPreview : BuildingPreview
{
    public override void StretchTo(Vector3 origin, Vector3 destination)
    {
        var direction = destination - origin;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, Mathf.Max(1, direction.magnitude));
        transform.forward = direction;
        transform.position = Vector3.Lerp(destination, origin, .5f);
    }
}