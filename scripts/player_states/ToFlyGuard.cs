using Godot;
using System;

[GlobalClass]
public partial class ToFlyGuard : Guard
{
    [Export]
    public Player player;

    public override bool Check(){
        Vector4 inputVector = new Vector4(Input.GetActionStrength("move_forward"), Input.GetActionStrength("move_backward"), Input.GetActionStrength("move_right"), Input.GetActionStrength("move_left"));
        return inputVector.Length() != 0.0F;
    }
}