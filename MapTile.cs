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
	private Sprite2D hoverOverlay;
	private Sprite2D selected;
	private static MapTile currentlySelectedTile;
	private TileMenu tileMenu;
	private Sprite2D sprite;

	private PackedScene tileMenuScene =
	GD.Load<PackedScene>("res://TileMenu.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		 sprite = GetNode<Sprite2D>("Sprite2D");
		UpdateAppearance();


		//When the mouse hovers over a tile, a transparent white box appears over it
		hoverOverlay = GetNode<Sprite2D>("HoverOverlay");

		hoverOverlay.Visible = false;

		MouseEntered += OnMouseEntered;
		MouseExited += OnMouseExited;


		selected = GetNode<Sprite2D>("Selected");
		selected.Visible = false;
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
			//If this tile is already selected, do nothing
			if (currentlySelectedTile == this)
			{
				return;
			}
			
			//Hide the previously selected tile
			if (currentlySelectedTile != null && currentlySelectedTile != this)
			{
				currentlySelectedTile.Deselect();
			}

			currentlySelectedTile = this;
			selected.Visible = true;

			//create the Menu
			tileMenu = tileMenuScene.Instantiate<TileMenu>();
			tileMenu.Visible = true;
			AddChild(tileMenu);

			//Sets the position of the menu
			tileMenu.Position = new Vector2(-20, -35);

			//send this tiles info to the menu
			tileMenu.Setup(this);
		}
		//Deselects tile when right button is pressed
		if (@event is InputEventMouseButton mouseButtonR &&
			mouseButtonR.ButtonIndex == MouseButton.Right &&
			mouseButtonR.Pressed)
			{
				if (currentlySelectedTile == this)
				{
					Deselect();
					currentlySelectedTile = null;
				}
			}
	}

	public void Deselect()
	{
		selected.Visible = false;

		if (tileMenu != null)
		{
			tileMenu.Visible = false;
			tileMenu.QueueFree();
			tileMenu = null;
		}

		if (currentlySelectedTile == this)
		{
			currentlySelectedTile = null;
		}
	}

	public void ChangeType(TileType newType)
	{
		Type = newType;
		UpdateAppearance();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
