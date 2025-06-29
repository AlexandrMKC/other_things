using Godot;
using System;

[GlobalClass]
public partial class PlayerFlyToIdleGuard : Guard<Player>
{
	public override bool Check()
	{
		return true;
	}
}
