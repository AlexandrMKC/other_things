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

	public override void _PhysicsProcess(double delta)
	{
		_MouseRotation(delta);

		

		Vector2 mousePosition = GetGlobalMousePosition();
		Vector2 parallelDirection = (mousePosition - GlobalPosition).Normalized();
		Vector2 perpendicularDirection = new Vector2(-parallelDirection.Y, parallelDirection.X);
		Vector3 inputVector = new Vector3(Input.GetActionStrength("move_forward"), Input.GetActionStrength("move_backward"), Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left"));

		Vector2 velocity = Velocity;
		//velocity = velocity.Rotated(velocity.AngleTo(parallelDirection));
		Vector2 velocity_ = new Vector2(parallelDirection.Dot(velocity), perpendicularDirection.Dot(velocity));

		velocity_[0] = velocity_[0] + accelerationForward*inputVector[0]*(float)delta;
		velocity_[0] = velocity_[0] - accelerationBackward*inputVector[1]*(float)delta;
		velocity_[1] = velocity_[1] + accelerationSide*inputVector[2]*(float)delta;

		if (inputVector.Length() > 0.0F)
		{
			velocity_[0] = velocity_[0] + accelerationForward * inputVector[0] * (float)delta;
			velocity_[0] = velocity_[0] - accelerationBackward * inputVector[1] * (float)delta;
			velocity_[1] = velocity_[1] + accelerationSide * inputVector[2] * (float)delta;
		}
		else
		{
			if (inputVector[0] == 0.0F && velocity_[0] >= 0.0F)
			{
				velocity_[0] = velocity_[0] - deceleration * (float)delta;
			}
			if (inputVector[1] == 0.0F && velocity_[0] < 0.0F)
			{
				velocity_[0] = velocity_[0] + deceleration * (float)delta;
			}
			if(inputVector[2] == 0.0F){
				velocity_[0] = velocity_[0] - deceleration*(float)delta*perpendicularDirection.Dot(velocity_);
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

		if (Mathf.Abs(velocity_[0]) < 1.0F)
		{
			velocity_[0] = 0.0F;
		}

		velocity = new Vector2();
		Velocity = velocity;
		MoveAndSlide();
	}

	private void _MouseRotation(double delta)
	{
		Vector2 mousePos = GetGlobalMousePosition();
		float targetAngle = (mousePos - GlobalPosition).Angle();
		Rotation = Mathf.LerpAngle(Rotation, targetAngle, speedRotation * (float)delta);
	}

	public void Move(Vector2 direct)
	{
		// Vector2 velocity = _player.Velocity;
		// velocity.X = 0.0f;
		// velocity.Y = 0.0f;
	}
}
