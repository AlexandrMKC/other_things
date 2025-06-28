using Godot;
using System;

[GlobalClass]
public partial class FlyToCollisionGuard : Guard<Player>
{

    public override bool Check(){
        return _target.collisionInfo != null;
    }
}