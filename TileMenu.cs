using Godot;
using System;

public partial class TileMenu : Panel
{
	private Label typeLabel;
	private Label positionLabel;
	private Button closeMenu;
	private Button upgradeButton;

	//Set the tile variables
	MapTile.TileType forest = MapTile.TileType.Forest;
	MapTile.TileType grass = MapTile.TileType.Grass;
	MapTile.TileType mountain = MapTile.TileType.Mountain;
	MapTile.TileType town = MapTile.TileType.Town;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		typeLabel = GetNode<Label>("TypeLabel");
		positionLabel = GetNode<Label>("PositionLabel");
		closeMenu = GetNode<Button>("CloseButton");
		upgradeButton = GetNode<Button>("Upgrade");

		closeMenu.Pressed += ButtonPressed;
		//upgradeButton.Pressed += UpgradePressed();

		upgradeButton.Visible = false;
	}

	public void Setup(
		MapTile.TileType tileType,
		Vector2I gridPosition)
	{
		typeLabel.Text = $"{tileType}";
		//positionLabel.Text = $"Position: {gridPosition}";

		if (tileType == forest ||
			tileType == grass ||
			tileType == mountain ||
			tileType == town)
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
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
