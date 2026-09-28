using Godot;
using System;

public partial class TileMenu : Panel
{
	private Label typeLabel;
	private Label positionLabel;
	private Button closeMenu;
	private Button upgradeButton;
	private MapTile selectedTile;

	//Set the tile variables
	MapTile.TileType forest = MapTile.TileType.Forest;
	MapTile.TileType grass = MapTile.TileType.Grass;
	MapTile.TileType mountain = MapTile.TileType.Mountain;
	MapTile.TileType town = MapTile.TileType.Town;
	MapTile.TileType farm = MapTile.TileType.Farm;
	MapTile.TileType lumber = MapTile.TileType.Lumber;
	MapTile.TileType mine = MapTile.TileType.Mine;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		typeLabel = GetNode<Label>("TypeLabel");
		positionLabel = GetNode<Label>("PositionLabel");
		closeMenu = GetNode<Button>("CloseButton");
		upgradeButton = GetNode<Button>("Upgrade");

		closeMenu.Pressed += ButtonPressed;
		upgradeButton.Pressed += UpgradePressed;

		upgradeButton.Visible = false;
	}

	public void Setup(MapTile tile)
		//MapTile.TileType tileType,
		//Vector2I gridPosition
	{
		selectedTile = tile;
		UpdateMenu();
		typeLabel.Text = $"{tile.Type}";
		//positionLabel.Text = $"Position: {tile.GridPosition}";

		if (tile.Type == forest ||
			tile.Type == grass ||
			tile.Type == mountain ||
			tile.Type == town)
		{
			upgradeButton.Visible = true;
		} 
		else 
		{
			upgradeButton.Visible = false;
		}
	}

	private void ButtonPressed()
	{
		if (GetParent() is MapTile tile)
		{
			tile.Deselect();
		}
	}

	private void UpgradePressed()
	{
		if (selectedTile == null)
		{
			return;
		}

		if (selectedTile.Type == grass)
		{
			selectedTile.ChangeType(farm);
		}
		if (selectedTile.Type == forest)
		{
			selectedTile.ChangeType(lumber);
		}
		if (selectedTile.Type == mountain)
		{
			selectedTile.ChangeType(mine);
		}

		UpdateMenu();
	}

	private void UpdateMenu()
	{
		typeLabel.Text = $"{selectedTile.Type}";
		//positionLabel.Text = $"Position: {tile.GridPosition}";

		if (selectedTile.Type == forest ||
			selectedTile.Type == grass ||
			selectedTile.Type == mountain ||
			selectedTile.Type == town)
		{
			upgradeButton.Visible = true;
		} 
		else 
		{
			upgradeButton.Visible = false;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
