using System;
using System.Collections.Generic;

public class StateMachine
{
    private List<Transition> _transitions = new();
    private State _defaultState;
    private State _currentState;

    public StateMachine(State defaultState)
    {
        _defaultState = defaultState;
    }

    public void Init()
    {
        _currentState = _defaultState;
        _currentState.Enter();
    }

    public void AddTransition(State from, State to, Func<bool> decision)
    {
        _transitions.Add(new Transition(from, to, decision));
    }

    private void SetState(State from, State to)
    {
        if (_currentState.GetType() != from.GetType()) return;

        _currentState.Exit();
        _currentState = to;
        _currentState.Enter();
    }

    public void Tick()
    {
        CheckTransitions();
        _currentState.Tick();
    }

    private void CheckTransitions()
    {
        foreach (var transition in _transitions)
        {
            if (!transition.Decision()) continue;
            
            SetState(transition.From, transition.To);
            return;
        }
    }

    private class Transition
    {
        public State From;
        public State To;
        public Func<bool> Decision;
     
        public Transition(State from, State to, Func<bool> decision)
        {
            From = from;
            To = to;
            Decision = decision;
        }
    }
}