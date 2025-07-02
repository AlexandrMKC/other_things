using Godot;
using System;

[GlobalClass]
public partial class PlayerFlyToIdleGuard : Guard<Player>
{
	public override bool Check()
	{
		Vector4 inputVector = new Vector4(Input.GetActionStrength("move_up"), Input.GetActionStrength("move_down"), Input.GetActionStrength("move_right"), Input.GetActionStrength("move_left"));
		if (_target == null)
		{
			GD.Print("ERROR");
		}
        return _target.Velocity.Length() == 0.0F && inputVector.Length() == 0.0F;
	}
}
