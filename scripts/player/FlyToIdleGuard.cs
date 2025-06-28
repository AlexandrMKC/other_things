using Godot;
using System;

[GlobalClass]
public partial class FlyToIdleGuard : Guard<Player>
{

    public override bool Check(){
        Vector4 inputVector = new Vector4(Input.GetActionStrength("move_up"), Input.GetActionStrength("move_down"), Input.GetActionStrength("move_right"), Input.GetActionStrength("move_left"));
        return _target.Velocity.Length() == 0.0F && inputVector.Length() == 0.0F;
    }
}