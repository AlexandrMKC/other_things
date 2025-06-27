using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public float accelerationForward { get; set; } = 100.0F;
	[Export]
	public float accelerationBackward { get; set; } = 50.0F;
	[Export]
	public float accelerationSide{ get; set; } = 50.0F;
	[Export]
	public float maxForwardSpeed { get; set; } = 200.0F;
	[Export]
	public float maxBackwardSpeed { get; set; } = 100.0F;
	[Export]
	public float maxSpeedSide { get; set; } = 100.0F;
	[Export]
	public float speedRotation { get; set; } = 5.0F;
	[Export]
	public float deceleration { get; set; } = 500.0F;
	[Export]
	public float minSpeed { get; set; } = 1.0F;

	public KinematicCollision2D collisionInfo;

	public override void _PhysicsProcess(double delta)
	{
		collisionInfo = MoveAndCollide(Velocity*(float)delta);
	}

	public void Collision(){
		Vector2 velocity = Velocity;
		velocity = velocity.Bounce(collisionInfo.GetNormal());
		Velocity = velocity;
	}

	public void MouseRotation(double delta)
	{
		Vector2 mousePos = GetGlobalMousePosition();
		float targetAngle = (mousePos - GlobalPosition).Angle();
		Rotation = Mathf.LerpAngle(Rotation, targetAngle, speedRotation * (float)delta);
	}

	public void Move(double delta)
	{
		Vector2 mousePosition = GetGlobalMousePosition();
		Vector2 parallelDirection = (mousePosition - GlobalPosition).Normalized();
		Vector2 perpendicularDirection = new Vector2(-parallelDirection.Y, parallelDirection.X);
		Vector4 inputVector = new Vector4(Input.GetActionStrength("move_forward"), Input.GetActionStrength("move_backward"), Input.GetActionStrength("move_right"), Input.GetActionStrength("move_left"));

		Vector2 velocity = Velocity;

		Vector2 i = new Vector2(1.0F, 0.0F);
		Vector2 j = new Vector2(0.0F, 1.0F);
		float x1 = parallelDirection.Dot(i)*velocity[0] + parallelDirection.Dot(j)*velocity[1];
		float y1 = perpendicularDirection.Dot(i)*velocity[0] + perpendicularDirection.Dot(j)*velocity[1];
		Vector2 velocity_ = new Vector2(x1, y1);


		velocity_[0] = velocity_[0] + accelerationForward * inputVector[0] * (float)delta;
		velocity_[0] = velocity_[0] - accelerationBackward * inputVector[1] * (float)delta;
		velocity_[1] = velocity_[1] + accelerationSide * inputVector[2] * (float)delta;
		velocity_[1] = velocity_[1] - accelerationSide * inputVector[3] * (float)delta;
		


		if (inputVector[0] == 0.0F && velocity_[0] > 0.0F)
		{
			velocity_[0] = velocity_[0] - deceleration * (float)delta;
			if(velocity_[0] < minSpeed){
				velocity_[0] = 0.0F;
			}
		}
		if (inputVector[1] == 0.0F && velocity_[0] < 0.0F)
		{
			velocity_[0] = velocity_[0] + deceleration * (float)delta;
			if(velocity_[0] > -minSpeed){
				velocity_[0] = 0.0F;
			}
		}

		if(inputVector[2] == 0.0F && velocity_[1] > 0.0F){
			velocity_[1] = velocity_[1] - deceleration*(float)delta;
			if(velocity_[1] < minSpeed){
				velocity_[1] = 0.0F;
			}
		}
		if(inputVector[3] == 0.0F && velocity_[1] < 0.0F){
			velocity_[1] = velocity_[1] + deceleration*(float)delta;
			if(velocity_[1] > -minSpeed){
				velocity_[1] = 0.0F;
			}
		}
		

		if (velocity_[0] >= maxForwardSpeed)
		{
			velocity_[0] = maxForwardSpeed;
		}

		if (velocity_[0] <= -maxBackwardSpeed)
		{
			velocity_[0] = -maxBackwardSpeed;
		}

		if (velocity_[1] >= maxSpeedSide)
		{
			velocity_[1] = maxSpeedSide;
		}

		if (velocity_[1] <= -maxSpeedSide)
		{
			velocity_[1] = -maxSpeedSide;
		}

		float x = i.Dot(parallelDirection)*velocity_[0] + i.Dot(perpendicularDirection)*velocity_[1];
		float y = j.Dot(parallelDirection)*velocity_[0] + j.Dot(perpendicularDirection)*velocity_[1];

		Velocity = new Vector2(x, y);
	}
}
