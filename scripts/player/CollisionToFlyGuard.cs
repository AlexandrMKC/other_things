using Godot;
using System;

[GlobalClass]
public partial class CollisionToFlyGuard : Guard<Player>
{

    public override bool Check(){
        return true; //_transition.timer.TimeLeft <= 0;
    }
}