using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public abstract partial class StateMachine : Node
{
	// controlled object
	[Export]
	public Node context;

	[Export]
	public State startState;

	protected State _currentState;

	public abstract void ChangeState(State newState);
}
