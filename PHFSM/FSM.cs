using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class FSM : StateMachine
{
	private Dictionary<string, State> _states = new Dictionary<string, State>();


	public override void _Ready(){
		foreach(var child in GetChildren()){
			if(child is State){
				var state = (State)child;
				_states[state.name] = state;
				state.Init();
				GD.Print("FSM " + this.Name + ": add state " + state.name);
			}
		}

		if (startState != null)
		{
			if (!_states.ContainsKey(startState.name))
			{
				GD.Print("FSM " + this.Name + " error: start state was not found.");
				return;
			}
			startState.Enter();
			_currentState = startState;
			GD.Print("FSM " + this.Name + ": enter to " + _currentState.name + " state");
		}
		else
		{
			GD.Print("FSM " + this.Name + ": error the initial state is not selected.");
			return;
		}
	}

	public override void ChangeState(State newState)
	{
		if(newState != null){
			_currentState.Exit();
			GD.Print("FSM " + this.Name + ": exit " + _currentState.name + " state");
			_currentState = newState;
			_currentState.Enter();
			GD.Print("FSM " + this.Name + ": enter to " + _currentState.name + " state");
		}
	}
	
	public override void _PhysicsProcess(double delta)
	{
		_currentState.PhysicsUpdate(delta);
	}

	public override void _Process(double delta)
	{
		_currentState.Update(delta);
	}

}
