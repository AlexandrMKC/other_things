using Godot;
using System;
using System.Collections.Generic;

public abstract partial class FSM<T> : StateMachine<T> where T: Node
{
	private Dictionary<string, State<T>> _states = new Dictionary<string, State<T>>();


	public override void _Ready(){

		base._Ready();

		foreach(var child in GetChildren()){
			if(child is State<T>){
				var state = (State<T>)child;
				_states[state.name] = state;
			}
		}

		foreach(var state in _states){
			state.Value.Init();
			GD.Print("State Machine " + this.Name + ": add state " + state.Value.name);
		}

		if (!_states.ContainsKey(initialState.name))
		{
			GD.Print("State Machine " + this.Name + " error: start state was not found.");
			return;
		}
		initialState.Enter();
		_currentState = initialState;
		GD.Print("State Machine " + this.Name + ": enter to " + _currentState.name + " state");
	}

	public override void ChangeState(State<T> newState)
	{
		if(newState != null){
			_currentState.Exit();
			GD.Print("State Machine " + this.Name + ": exit " + _currentState.name + " state");
			_currentState = newState;
			_currentState.Enter();
			GD.Print("State Machine " + this.Name + ": enter to " + _currentState.name + " state");
		}
	}

	public override State<T> GetStateName(string name){
		return _states[name];
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
