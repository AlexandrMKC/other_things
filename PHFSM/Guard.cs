using Godot;
using System;

[GlobalClass]
public abstract partial class Guard : Node
{
	public abstract bool Check();
}
