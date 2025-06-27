using Godot;
using System;

[GlobalClass]
public partial class ToCollisionGuard : Guard
{
    [Export]
    public Player player;

    public override bool Check(){
        return player.collisionInfo != null;
    }
}