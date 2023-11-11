public class SingleLevelTransparentizable : Transparentizable
{
    protected override float Epsilon => 1f;

    protected override void TransparentizeZWrite(float min, float max)
    {
        if (mainTransform.position.y > max || mainTransform.position.y < min)
        {
            materialManager.ZWriteOff();
            return;
        }

        materialManager.ZWriteOn();
    }
}