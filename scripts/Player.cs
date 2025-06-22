using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public float speedMovement { get; set; } = 10.0F;

	[Export]
	public float speedRotation { get; set; } = 5.0F;

	public override void _PhysicsProcess(double delta)
	{
		_MouseRotation(delta);

		Vector2 mousePosition = GetGlobalMousePosition();
		Vector2 toMouse = (mousePosition - GlobalPosition).Normalized();
		Vector2 perpendicular = new Vector2(-toMouse.Y, toMouse.X);
		float moveForward = Input.GetActionStrength("move_forward") - Input.GetActionStrength("move_backward");
		float strafe = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
		Vector2 movement = toMouse * moveForward * speedMovement + perpendicular * strafe * speedMovement;
		Velocity = movement;
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
