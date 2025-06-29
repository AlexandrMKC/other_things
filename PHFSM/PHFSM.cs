using Godot;
using System;
using System.Collections.Generic;

public abstract partial class PHFSM<T> : StateMachine<T> where T: Node
{
    public override void _Ready(){

    }

	public override void _PhysicsProcess(double delta)
	{
	}

	public override void _Process(double delta)
	{
	}

}