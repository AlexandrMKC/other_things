using Godot;
using System;
using System.Collections.Generic;

public abstract partial class StateMachine<T> : Node where T: Node
{
	[Export]
	public State<T> initialState;

	public T _target;
	protected State<T> _currentState;

	public override void _Ready(){
		_target = (T)GetParent();
		GD.Print("Create State Machine " + this.Name + " target: " + _target.Name);

		if(initialState == null){
			GD.Print("StateMachine " + this.Name + ": error the initial state is not selected.");
			return;
		}
	}

	public abstract void ChangeState(State<T> newState);
	public abstract State<T> GetStateName(string name);
}
