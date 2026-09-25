using Godot;
using System;

public partial class MapTile : Area2D
{
	public enum TileType
	{
		Grass,
		Forest
	}

	public TileType Type { get; private set; }
	public Vector2I GridPosition { get; private set; }

	private Sprite2D sprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		 sprite = GetNode<Sprite2D>("Sprite2D");
		UpdateAppearance();
	}

	public void Setup(TileType type, Vector2I gridPosition)
	{
		Type = type;
		GridPosition = gridPosition;

		if (sprite != null)
			UpdateAppearance();
	}

	private void UpdateAppearance()
	{
		if (Type == TileType.Grass)
		{
			sprite.Texture = GD.Load<Texture2D>("res://Grass.png");
		}
		else if (Type == TileType.Forest)
		{
			sprite.Texture = GD.Load<Texture2D>("res://Forest.png");
		}
	}

	public override void _InputEvent(
		Viewport viewport,
		InputEvent @event,
		int shapeIndex)
	{
		if (@event is InputEventMouseButton mouseButton &&
			mouseButton.ButtonIndex == MouseButton.Left &&
			mouseButton.Pressed)
		{
			GD.Print($"Clicked {Type} at {GridPosition}");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
