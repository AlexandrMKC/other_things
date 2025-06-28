using Godot;
using System;

[GlobalClass]
public partial class CollisionPlayer : State <Player>
{

    public override void Enter(){

    }

    public override void Exit(){
        _target.Collision();
    }

    public override void InternalProcesses(double delta){

    }

    public override void InternalPhysicalProcesses(double delta){

    }
}