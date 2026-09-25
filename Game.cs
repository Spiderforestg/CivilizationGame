using Godot;
using System;

public partial class Game : Node2D
{
	private PackedScene tileScene = GD.Load<PackedScene>("res://MapTile.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CreateTile();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void CreateTile()
	{
		for (int x = 0; x < 10; x++)
		{
			for (int y = 0; y < 10; y++)
			{
				MapTile tile = tileScene.Instantiate<MapTile>();

				MapTile.TileType tileType =
					(x + y) % 3 == 0
					? MapTile.TileType.Forest
					: MapTile.TileType.Grass;

				tile.Setup(tileType, new Vector2I(x, y));

				tile.Position = new Vector2(30 + (x * 64), 30 + (y * 64));

				AddChild(tile);
			}
		}
	}
}
