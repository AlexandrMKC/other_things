using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class Transition : Node
{
	[Signal]
	public delegate void TransitionToEventHandler(State newState);

	[Export]
	public State transitionTo;
	
	private List<Guard> _guards = new List<Guard>();

	public void Init()
	{
		_guards.Clear();
		foreach (var child in GetChildren())
		{
			if (child is Guard)
			{
				var guard = (Guard)child;
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
				EmitSignal(SignalName.TransitionTo, transitionTo);
			}
		}
	}
}
