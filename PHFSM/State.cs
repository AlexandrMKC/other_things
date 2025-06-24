using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public abstract partial class State : Node
{
    [Export]
    public string name;

    protected StateMachine _stateMachine;

    public Node context;

    protected List<Transition> _transitions = new List<Transition>();

    public void Init()
    {
        if(GetParent() is StateMachine _stateMachine)

        context = _stateMachine.context;

        _transitions.Clear(); 
        foreach (var child in GetChildren())
        {
            if (child is Transition)
            {
                var transition = (Transition)child;
                transition.Init();
                _transitions.Add(transition);
                GD.Print("State " + this.Name + ": add transition " + transition.Name);
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
        CheckTransitions();
        InternalPhysicalProcesses(delta);
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
