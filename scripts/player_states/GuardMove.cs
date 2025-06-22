using Godot;
using System;

public partial class GuardMove : Node, IGuard
{
    [Export]
    public CharacterBody2D context;

    public bool Guard()
    {
        return Input.IsActionPressed("move_forward");
    }
}
