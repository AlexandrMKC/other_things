using Godot;
using System;

[GlobalClass]
public partial class PlayerIdleToFlyGuard : Guard<Player>
{
	public override bool Check()
	{
		return true;
	}
}
