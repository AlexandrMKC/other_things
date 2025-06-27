using Godot;
using System;

public partial class StaticAsteroid : StaticBody2D
{
	private Polygon2D _polygon2d;
	private CollisionPolygon2D _collisionPolygon2d;
	

	public override void _Ready()
	{
		_polygon2d = GetNode<Polygon2D>("Polygon2D");

		_collisionPolygon2d = GetNode<CollisionPolygon2D>("CollisionPolygon2D");
		_collisionPolygon2d.Polygon = _polygon2d.Polygon;
	}


	public override void _Process(double delta)
	{
	}
}
