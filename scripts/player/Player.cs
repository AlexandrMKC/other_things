using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public float accelerationMove { get; set; } = 100.0F;
	[Export]
	public float maxSpeedMove { get; set; } = 200.0F;
	[Export]
	public float decelerationMove { get; set; } = 500.0F;
	[Export]
	public float minSpeedMove { get; set; } = 1.0F;

	[Export]
	public float speedRotation { get; set; } = 10.0F;

	[Export]
	public float collision_parameter { get; set; } = 0.5F;

	public KinematicCollision2D collisionInfo;

	public override void _PhysicsProcess(double delta)
	{
		//collisionInfo = MoveAndCollide(Velocity*(float)delta);
		//GD.Print("Physic update Player");
	}

	public void Collision(){
		Vector2 velocity = Velocity*collision_parameter;
		velocity = velocity.Bounce(collisionInfo.GetNormal());
		Velocity = velocity;
	}

	public void Move(double delta)
	{
		Vector2 inputVector = new Vector2(
			Input.GetActionStrength("move_right")-Input.GetActionStrength("move_left"), 
			Input.GetActionStrength("move_down")-Input.GetActionStrength("move_up")
		).Normalized();

		Vector2 velocity = Velocity;
		
		// acceleration and rotation
		if(inputVector.Length() > 0.0F){
			velocity = velocity.Length()*inputVector + accelerationMove*inputVector*(float)delta;
			Rotation = Mathf.LerpAngle(Rotation, inputVector.Angle(), speedRotation * (float)delta);
		}

		if(inputVector.Length() == 0.0F && velocity.Length() >= minSpeedMove){
			velocity = velocity - decelerationMove*velocity.Normalized()*(float)delta;
		}
		
		// zero speed
		if(velocity.Length() < minSpeedMove){
			velocity = new Vector2(0.0F, 0.0F);
		}

		// max speed
		if(velocity.Length() >= maxSpeedMove){
			velocity = maxSpeedMove*velocity.Normalized();
		}
		Velocity = velocity;
		collisionInfo = MoveAndCollide(Velocity*(float)delta);
	}
}
