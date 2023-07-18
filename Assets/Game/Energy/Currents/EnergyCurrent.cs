using System;

public class EnergyCurrent
{
    public EnergyGenerator Sender { get; private set; }
    public EnergyContainer Receiver { get; private set; }
    private float _charge;
    public float Charge
    {
        get => _charge;
        set
        {
            if (value == _charge) return;
            _charge = value;
            OnChargeChanged.Invoke();
        }
    }
    
    public EnergyCurrent(EnergyGenerator from, EnergyContainer to)
    {
        Sender = from;
        Receiver = to;
        SendCharge();
    }
    
    public event Action OnChargeChanged = delegate { }; 

    private void SendCharge()
    {
        Sender.RequestEnergy(this);
        Receiver.AddCurrent(this);
    }

    public void SetCharge(float value)
    {
        Charge = value;
        OnChargeChanged.Invoke();
    }
}