public class SpenderNode : EnergyNode
{
    public override bool CanConnect(EnergyNode other, out CurrentContainer sender, out CurrentContainer receiver)
    {
        sender = null;
        receiver = null;
        return other is GeneratorNode generatorNode && generatorNode.CanConnect(this, out sender, out receiver);
    }
}