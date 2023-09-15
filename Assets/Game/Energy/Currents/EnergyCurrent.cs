using System;

public class EnergyCurrent
{
    public CurrentContainer Sender { get; private set; }
    public CurrentContainer Receiver { get; private set; }
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
    
    public EnergyCurrent(CurrentContainer from, CurrentContainer to)
    {
        Sender = from;
        Receiver = to;
        SendCharge();
    }
    
    public event Action OnChargeChanged = delegate { }; 
    public event Action<EnergyCurrent> OnShutDown = delegate { };

    private void SendCharge()
    {
        Sender.AddCurrent(this);
        Receiver.AddCurrent(this);
    }

    public void SetCharge(float value)
    {
        Charge = value;
    }

    public void ShutDown()
    {
        OnShutDown.Invoke(this);
    }
}