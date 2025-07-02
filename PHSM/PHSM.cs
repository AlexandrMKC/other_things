using Godot;
using System;
using System.Collections.Generic;

public abstract partial class PHSM<T> : Node where T : Node
{
    // general variables for all State Machine
    protected Dictionary<string, object> _variables = new Dictionary<string, object>();

    // controlled object
    public T _target { get; private set; }

    // State Machines
    protected List<StateMachine<T>> _stateMachines = new List<StateMachine<T>>();


    public override void _Ready()
    {
        // set up target
        var parent = GetParent();
        if (parent is T)
        {
            _target = (T)parent;
            GD.Print("PHSM " + this.Name + ": CREATE PHSM for monitoring condition " + _target.Name);
        }
        else
        {
            GD.PrintErr("PHSM: not found target");
            return;
        }

        // set up state machine layers
        _stateMachines.Clear();
        foreach (var child in GetChildren())
        {
            if (child is StateMachine<T>)
            {
                var stateMachine = (StateMachine<T>)child;
                GD.Print("PHSM " + this.Name + ": add state machine " + stateMachine.Name);
                stateMachine.Build();
                _stateMachines.Add(stateMachine);
            }
        }

    }

    // method for work events
    public void SendEvent(string e)
    {

    }

    // method for general variables work
    public void AddVariable(string nameVariable, object newVariable)
    {
        _variables[nameVariable] = newVariable;
    }

	public override void _PhysicsProcess(double delta)
	{
        foreach (var stateMachine in _stateMachines)
        {
            stateMachine.PhysicsUpdate(delta);
        }
	}

	public override void _Process(double delta)
	{
		foreach (var stateMachine in _stateMachines)
        {
            stateMachine.Update(delta);
        }
	}

}