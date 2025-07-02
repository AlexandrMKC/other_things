using Godot;
using System;
using System.Collections.Generic;

public abstract partial class State<T>: Node where T: Node
{
    [Export]
    public string name { get; private set; }

    public StateMachine<T> _stateMachine { get; private set; }

    public T _target { get; private set; }
    
    protected List<Transition<T>> _transitions = new List<Transition<T>>();

    public void Init()
    {
        // get state machine 
        var parent = GetParent();
        if (parent is StateMachine<T>)
        {
            _stateMachine = (StateMachine<T>)parent;
        }
        else
        {
            // это случай пока не рассматривается
            GD.PrintErr("       State " + name + ": not found state machine");
        }

        _target = _stateMachine._target;

        // get transitions
        _transitions.Clear();
        foreach (var child in GetChildren())
        {
            if (child is Transition<T>)
            {
                var transition = (Transition<T>)child;
                transition.Init();
                _transitions.Add(transition);
                GD.Print("       State " + name + ": add transition " + transition.Name);
            }
        }
    }

    public void CheckTransitions()
    {
        foreach (var transition in _transitions)
        {
            transition.Check();
        }
    }

    public void PhysicsUpdate(double delta)
    {
        InternalPhysicalProcesses(delta);
        CheckTransitions();
    }

    public void Update(double delta)
    {
        InternalProcesses(delta);
    }

    public abstract void Enter();

    public abstract void Exit();

    public abstract void InternalProcesses(double delta);

    public abstract void InternalPhysicalProcesses(double delta);
}
