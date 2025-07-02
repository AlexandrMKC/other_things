using Godot;
using System;
using System.Collections.Generic;

public abstract partial class Transition<T> : Node where T : Node
{
    [Export]
    public State<T> transitionTo;

    public StateMachine<T> _stateMachine { get; private set; }
    public T _target { get; private set; }

    public void Init()
    {
        var parenet = GetParent();
        if (parenet is State<T>)
        {
            var state = (State<T>)parenet;
            _stateMachine = state._stateMachine;
            _target = state._target;
        }

        SetTrigger();
    }

    protected abstract void SetTrigger();

    public abstract void Check();
}