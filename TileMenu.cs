using Godot;
using System;

public partial class TileMenu : Panel
{
	private Label typeLabel;
	private Label positionLabel;
	private Button closeMenu;
	private Button upgradeButton;
	private MapTile selectedTile;
	private Panel upgradeInfo;
	private Label infoLabel;
	private Label stonePrice;
	private Label wheatPrice;
	private Label timberPrice;
	private Sprite2D timberImg;
	private Sprite2D stoneImg;
	private Sprite2D wheatImg;

	//Set the tile variables
	MapTile.TileType forest = MapTile.TileType.Forest;
	MapTile.TileType grass = MapTile.TileType.Grass;
	MapTile.TileType mountain = MapTile.TileType.Mountain;
	MapTile.TileType town = MapTile.TileType.Town;
	MapTile.TileType farm = MapTile.TileType.Farm;
	MapTile.TileType lumber = MapTile.TileType.Lumber;
	MapTile.TileType mine = MapTile.TileType.Mine;

	ResourceManager resources;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		typeLabel = GetNode<Label>("TypeLabel");
		positionLabel = GetNode<Label>("PositionLabel");
		closeMenu = GetNode<Button>("CloseButton");
		upgradeButton = GetNode<Button>("Upgrade");
		upgradeInfo = GetNode<Panel>("UpgradeInfo");
		infoLabel = GetNode<Label>("UpgradeInfo/InfoLabel");

		//resources
		stonePrice = GetNode<Label>("UpgradeInfo/Stone");
		wheatPrice = GetNode<Label>("UpgradeInfo/Wheat");
		timberPrice = GetNode<Label>("UpgradeInfo/Timber");
		stoneImg = GetNode<Sprite2D>("UpgradeInfo/Stone2");
		wheatImg = GetNode<Sprite2D>("UpgradeInfo/Wheat2");
		timberImg = GetNode<Sprite2D>("UpgradeInfo/Timber2");

		//Lets you access the Resource Manager script
		resources = GetNode<ResourceManager>("/root/ResourceManager");

		closeMenu.Pressed += ButtonPressed;
		upgradeButton.Pressed += UpgradePressed;

		upgradeButton.MouseEntered += ShowUpgradeInfo;
		upgradeButton.MouseExited += HideUpgradeInfo;

		upgradeButton.Visible = false;
		upgradeInfo.Visible = false;
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

		stoneImg.Visible = false;
		wheatImg.Visible = false;
		timberImg.Visible = false;
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
			bool success = resources.SpendResources(timberCost: 1, stoneCost: 1, wheatCost: 0);
			if (success) {
			selectedTile.ChangeType(farm);
			selectedTile.Deselect();
			}
			else {
				GD.Print("Not enough resources");

				if (resources.Timber < 1) {FlashRed(timberPrice);}
				if (resources.Stone < 1) {FlashRed(stonePrice);}
				if (resources.Wheat < 0) {FlashRed(wheatPrice);}
			}
		}
		if (selectedTile.Type == forest)
		{
			selectedTile.ChangeType(lumber);
			selectedTile.Deselect();
		}
		if (selectedTile.Type == mountain)
		{
			selectedTile.ChangeType(mine);
			selectedTile.Deselect();
		}
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

	private void ShowUpgradeInfo()
	{
		if (selectedTile == null)
		{
			return;
		}

		if (selectedTile.Type == grass)
		{
			infoLabel.Text =
				"Farm";
			timberPrice.Text =
				"1";
			stonePrice.Text =
				"1";
			upgradeInfo.Visible = true;
			timberImg.Visible = true;
			stoneImg.Visible = true;
		}
		if (selectedTile.Type == forest)
		{
			infoLabel.Text =
				"+Lumber Yard";
			upgradeInfo.Visible = true;
		}
		if (selectedTile.Type == mountain)
		{
			infoLabel.Text =
				"+Mine";
			upgradeInfo.Visible = true;
		}
		if (selectedTile.Type == town)
		{
			infoLabel.Text =
				"lvl 2";
			upgradeInfo.Visible = true;
		}
	}

	private void HideUpgradeInfo()
	{
		upgradeInfo.Visible = false;
		stonePrice.Visible = false;
		wheatPrice.Visible = false;
		timberPrice.Visible = false;
	}

	private void FlashRed(Label label)
	{
		Color originalColor = label.Modulate;
		
		label.Modulate = Colors.Red;

		Tween tween = CreateTween();

		tween.TweenProperty(label, "modulate", originalColor, 0.5f);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
