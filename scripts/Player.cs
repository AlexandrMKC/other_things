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

		if(inputVector.Length() > 0.0F){
			velocity_[0] = velocity_[0] + accelerationForward*inputVector[0]*(float)delta;
			velocity_[0] = velocity_[0] - accelerationBackward*inputVector[1]*(float)delta;
			velocity_[1] = velocity_[1] + accelerationSide*inputVector[2]*(float)delta;
		} else {
			if(inputVector[0] == 0.0F && velocity_[0] > 0.0F){
				velocity_[0] = velocity_[0] - deceleration*(float)delta;
			}
		}
		// if(inputVector[0] > 0.0F){
		// 	velocity = velocity + parallelDirection*(accelerationForward*inputVector[0])*(float)delta;
		// 	velocity = velocity + parallelDirection*(accelerationBackward*inputVector[1])*(float)delta;
		// } else {
		// 	velocity = velocity - parallelDirection*(deceleration)*(float)delta;
		// }

		// if(velocity.Dot(parallelDirection) >= maxForwardSpeed){
		// 	velocity = parallelDirection*maxForwardSpeed; 
		// }

		if(Mathf.Abs(velocity_[0]) < 5.0F){
			velocity_[0] = 0.0F;         
		}

		
		//Vector2 velocity = directionParall*(speedForward*inputData[0] - speedBackward*inputData[1]) + directionPerpendicular*speedSide*inputData[2];

		// Vector2 mousePosition = GetGlobalMousePosition();
		// Vector2 toMouse = (mousePosition - GlobalPosition).Normalized();
		// Vector2 perpendicular = new Vector2(-toMouse.Y, toMouse.X);
		// float moveForward = Input.GetActionStrength("move_forward") - Input.GetActionStrength("move_backward");
		// float strafe = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
		// Vector2 movement = toMouse * moveForward * speedMovement + perpendicular * strafe * speedMovement;
		//velocity = new Vector2(Vector2.Right.Dot(parallelDirection)*velocity_[0], -Vector2.Up.Dot(perpendicularDirection)*velocity_[1]);
		velocity = velocity_.Rotated(Vector2.Right.AngleTo(parallelDirection));
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
