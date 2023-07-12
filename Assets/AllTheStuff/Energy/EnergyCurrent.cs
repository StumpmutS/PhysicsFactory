using System;

public class EnergyCurrent
{
    public EnergyGenerator Sender { get; private set; }
    public EnergyContainer Receiver { get; private set; }
    private int _charge;
    public int Charge
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

    public void SetCharge(int value)
    {
        Charge = value;
        OnChargeChanged.Invoke();
    }
}