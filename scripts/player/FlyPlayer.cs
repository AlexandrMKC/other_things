using Godot;
using System;

[GlobalClass]
public partial class FlyPlayer : State<Player>
{

    public override void Enter(){

    }

    public override void Exit(){

    }

    public override void InternalProcesses(double delta){

    }

    public override void InternalPhysicalProcesses(double delta){
        _target.ControllMove(delta);
    }
}