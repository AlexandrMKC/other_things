using Godot;
using System;
using System.Collections;

[GlobalClass]
public partial class Transition : Node
{
	[Signal]
	public delegate void TransitionToEventHandler(State newState);

	[Export]
	public State newState;

	
	private ArrayList _guards = new ArrayList();

	public void Init()
	{

		_guards.Clear();

		foreach (var child in GetChildren())
		{
			if (child is IGuard)
			{
				var guard = (IGuard)child;
				_guards.Add(guard);
				var guard_ = (Node)guard;
				GD.Print("Transition: add guard " + guard_.Name);
			}
		}
	}

	public void CheckGuards()
	{
		foreach (var guard in _guards)
		{
			var guard_ = (IGuard)guard;
			if (guard_.Guard())
			{
				GD.Print("TRUE");
			}
		}
	}

	public override void _Ready()
	{
	}


	public override void _Process(double delta)
	{
	}
}
