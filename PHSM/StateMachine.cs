using Godot;
using System;
using System.Collections.Generic;

public abstract partial class StateMachine<T> : Node where T : Node
{
	public PHSM<T> _phsm { get; private set; }

	public T _target { get; private set; }

	[Export]
	public State<T> initialState;

	public State<T> _currentState { get; private set; }

	// set up method
	public void Build()
	{
		// get phsm
		var parent = GetParent();
		if (parent is PHSM<T>)
		{
			_phsm = (PHSM<T>)parent;
		}
		else
		{
			GD.PrintErr("	State Machine " + this.Name + ": not found phsm");
			return;
		}

		// get target
		_target = (T)_phsm._target;

		// start state
		if (initialState != null)
		{
			_currentState = initialState;
		}
		else
		{
			GD.PrintErr("	State Machine " + this.Name + ": not selected initial state");
		}

		Init();
	}

	public abstract void Init();

	public abstract void ChangeState(State<T> state);

	public abstract void PhysicsUpdate(double delta);

	public abstract void Update(double delta);
}