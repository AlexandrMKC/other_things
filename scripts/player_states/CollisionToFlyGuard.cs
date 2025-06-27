using Godot;
using System;

[GlobalClass]
public partial class CollisionToFlyGuard : Guard
{

    public override bool Check(){
        return true;
    }
}