using Godot;
using System;
using System.Collections.Generic;

public abstract partial class FSM<T> : StateMachine<T> where T: Node
{
    protected Dictionary<string, State<T>> _states = new Dictionary<string, State<T>>();

    public override void Init()
    {
        // get states 
        _states.Clear();
        foreach (var child in GetChildren())
        {
            if (child is State<T>)
            {
                var state = (State<T>)child;
                _states[state.name] = state;
                GD.Print("	State Machine " + this.Name + ": add state " + state.name);
                state.Init();
            }
        }

        // check to contain states
        if (_states.Count == 0)
        {
            GD.PrintErr("   State Machine " + this.Name + ": not contain any states.");
        }
        else
        {
            if (!_states.ContainsKey(_currentState.name))
            {
                GD.PrintErr("   State Machine " + this.Name + ": " + _currentState.name + " not exist");
                return;
            }
            _currentState.Enter();
		    GD.Print("  State Machine " + this.Name + ": enter to " + _currentState.name + " state");
        }
    }

    public override void ChangeState(State<T> state)
    {
        if(state != null){
			_currentState.Exit();
			GD.Print("  State Machine " + this.Name + ": exit " + _currentState.name + " state");
			_currentState = state;
			_currentState.Enter();
			GD.Print("State Machine " + this.Name + ": enter to " + _currentState.name + " state");
		}
    }

    public override void PhysicsUpdate(double delta)
    {
        _currentState.PhysicsUpdate(delta);
    }

    public override void Update(double delta)
    {
        _currentState.Update(delta);
    }
}