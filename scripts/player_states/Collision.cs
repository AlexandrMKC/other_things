using Godot;
using System;

[GlobalClass]
public partial class Collision : State
{
    [Export]
    public Player player;

    public override void Enter(){

    }

    public override void Exit(){
        player.Collision();
        GD.Print("Collision");
    }

    public override void InternalProcesses(double delta){

    }

    public override void InternalPhysicalProcesses(double delta){
        
    }
}