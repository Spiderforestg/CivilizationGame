using Godot;
using System;

public partial class Game : Node2D
{
	private PackedScene tileScene = GD.Load<PackedScene>("res://MapTile.tscn");
	private int tileSize = 16 * 4;

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

		string[] layout = {
			"22000",
			"25031",
			"02611",
			"00141",
			"00000"
		};

		for (int y = 0; y < layout.Length; y++)
		{
			for (int x = 0; x < layout[y].Length; x++)
			{
				MapTile tile = tileScene.Instantiate<MapTile>();

				MapTile.TileType tileType = layout[y][x] switch {

					'0' => MapTile.TileType.Grass,
					'1' => MapTile.TileType.Forest,
					'2' => MapTile.TileType.Mountain,
					'3' => MapTile.TileType.Farm,
					'4' => MapTile.TileType.Lumber,
					'5' => MapTile.TileType.Mine,
					'6' => MapTile.TileType.Town,

					_ => MapTile.TileType.Grass
				};

				tile.Setup(tileType, new Vector2I(x, y));
				tile.Position = new Vector2(400 + (x * (tileSize + 2)), 150 + (y * (tileSize + 2)));

				AddChild(tile);
			}
		}
	}
}
