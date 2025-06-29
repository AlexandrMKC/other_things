using Godot;
using System;
using System.Collections.Generic;

public abstract partial class TransitionGuard<T> : Transition<T> where T : Node
{
    protected List<Guard<T>> _guards = new List<Guard<T>>();
    protected override void SetTrigger()
    {
        _guards.Clear();
        foreach (var child in GetChildren())
        {
            if (child is Guard<T>)
            {
                var guard = (Guard<T>)child;
                _guards.Add(guard);
                GD.Print("          Transition " + this.Name + ": add guard " + guard.Name);
            }
        }
    }

    public override void Check()
    {
        foreach (var guard in _guards) {
            if (guard.Check())
            {
                _stateMachine.ChangeState(transitionTo);
            }
        }
    }
}