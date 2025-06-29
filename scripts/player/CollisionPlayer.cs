using Godot;
using System;

[GlobalClass]
public partial class CollisionPlayer : State <Player>
{
    public Timer timer;

    public override void Enter(){
        _target.Collision();
        // timer = new Timer();
        // AddChild(timer);

        // timer.WaitTime = 1.0f;
        // timer.OneShot = true;
        // timer.Start();
    }

    public override void Exit(){
    }

    public override void InternalProcesses(double delta){

    }

    public override void InternalPhysicalProcesses(double delta){

    }
}