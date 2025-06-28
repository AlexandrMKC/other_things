using Godot;
using System;
using System.Collections.Generic;

public abstract partial class Transition<T> : Node where T: Node
{
	[Signal]
	public delegate void TransitionToEventHandler(State<T> newState);

	[Export]
	public string transitionToStateName;

	public State<T> transitionTo;

	public StateMachine<T> _stateMachine { get; set; }
	
	private List<Guard<T>> _guards = new List<Guard<T>>();

	public void Init()
	{
		var stateNode = GetParent();
		if(stateNode is State<T>){
			var state = (State<T>)stateNode;
			_stateMachine = state._stateMachine;
			transitionTo = _stateMachine.GetStateName(transitionToStateName);
			GD.Print("Transition " + this.Name + " : add to transition to " + transitionTo.Name);
		}

		_guards.Clear();
		foreach (var child in GetChildren())
		{
			if (child is Guard<T>)
			{
				var guard = (Guard<T>)child;
				guard.Init();
				_guards.Add(guard);
				GD.Print("Transition " + this.Name + " : add guard " + guard.Name);
			}
		}
	}

	public void Check()
	{
		foreach (var guard in _guards)
		{
			
			if (guard.Check())
			{
				_stateMachine.ChangeState(transitionTo);
				//EmitSignal(SignalName.TransitionTo, transitionTo);
			}
		}
	}
}
