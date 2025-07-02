using Godot;
using System;

[GlobalClass]
public partial class PlayerFlyState : State<Player>
{
    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void InternalProcesses(double delta)
    {
        _target.ControllMove(delta);
    }

    public override void InternalPhysicalProcesses(double delta)
    {
        
    }
}
