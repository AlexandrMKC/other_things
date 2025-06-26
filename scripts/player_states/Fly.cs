using Godot;
using System;

[GlobalClass]
public partial class Fly : State
{
    [Export]
    public Player player;

    public override void Enter(){

    }

    public override void Exit(){

    }

    public override void InternalProcesses(double delta){

    }

    public override void InternalPhysicalProcesses(double delta){
        player.MouseRotation(delta);
        player.Move(delta);
    }
}