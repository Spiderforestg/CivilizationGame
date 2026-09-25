using Godot;
using System;

public partial class MapTile : Area2D
{
	public enum TileType
	{
		Grass,
		Forest,
		Mountain,
		Farm,
		Lumber,
		Mine,
		Town
	}

	public TileType Type { get; private set; }
	public Vector2I GridPosition { get; private set; }
	private Polygon2D hoverOverlay;

	private Sprite2D sprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		 sprite = GetNode<Sprite2D>("Sprite2D");
		UpdateAppearance();


		//When the mouse hovers over a tile, a transparent white box appears over it
		hoverOverlay = GetNode<Polygon2D>("HoverOverlay");

		hoverOverlay.Color = new Color(1, 1, 1, 0.25f);
		hoverOverlay.Visible = false;

		hoverOverlay.Polygon = new Vector2[]
		{
		new Vector2(-8, -8),
		new Vector2(8, -8),
		new Vector2(8, 8),
		new Vector2(-8, 8)
		};

		MouseEntered += OnMouseEntered;
		MouseExited += OnMouseExited;
	}

	private void OnMouseEntered()
	{
		hoverOverlay.Visible = true;
	}

	private void OnMouseExited()
	{
		hoverOverlay.Visible = false;
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
			sprite.Texture = GD.Load<Texture2D>("res://tiles/Grass.png");
		}
		
		if (Type == TileType.Forest)
		{
			sprite.Texture = GD.Load<Texture2D>("res://tiles/Forest.png");
		}

		if (Type == TileType.Mountain)
		{
			sprite.Texture = GD.Load<Texture2D>("res://tiles/Mountain.png");
		}

		if (Type == TileType.Farm)
		{
			sprite.Texture = GD.Load<Texture2D>("res://tiles/Farm.png");
		}

		if (Type == TileType.Lumber)
		{
			sprite.Texture = GD.Load<Texture2D>("res://tiles/Lumber.png");
		}

		if (Type == TileType.Mine)
		{
			sprite.Texture = GD.Load<Texture2D>("res://tiles/Mine.png");
		}

		else if (Type == TileType.Town)
		{
			sprite.Texture = GD.Load<Texture2D>("res://tiles/Town.png");
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
