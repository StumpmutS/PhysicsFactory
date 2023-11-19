using System;

public interface IRefreshable
{
    public event Action OnRefresh;
}