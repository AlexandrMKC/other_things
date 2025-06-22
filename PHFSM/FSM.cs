using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class FSM : Node
{
	[Export]
	public Node context;

	[Export]
	public State startState;

	public void TransitionTo(State newState)
	{
		if(newState != null){
			_currentState.Exit();
			GD.Print("FSM: Exit " + _currentState.name + " state");
			_currentState = newState;
			_currentState.Enter();
			GD.Print("FSM: Enter to " + _currentState.name + " state");
		}
	}

	// states
	private Dictionary<string, State> _states = new Dictionary<string, State>();

	private State _currentState;

	public override void _Ready(){

		foreach(var child in GetChildren()){
			if(child is State){
				var state = (State)child;
				_states[state.name] = state;
				state.Init();
				GD.Print("FSM: add state " + state.name);
			}
		}

		if (startState != null)
		{
			if (!_states.ContainsKey(startState.name))
			{
				GD.Print("FSM: Error: start state was not found.");
				return;
			}
			startState.Enter();
			_currentState = startState;
			GD.Print("FSM: Enter to " + _currentState.name + " state");
		}
		else
		{
			GD.Print("FSM: Error: the initial state is not selected.");
			return;
		}
	}

	
	public override void _PhysicsProcess(double delta)
	{
		_currentState.test(delta);
	}

	public override void _Process(double delta)
	{
		_currentState.Process(delta);
	}

}
