using Godot;
using System;

public partial class ResourceManager : Node
{
	[Signal]
	public delegate void ResourcesChangedEventHandler();

	public int Timber { get; private set;}
	public int Stone { get; private set;}
	public int Wheat { get; private set;}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	private void NotifyResourcesChanged()
	{
		EmitSignal(SignalName.ResourcesChanged);
	}

	public void AddTimber(int amount)
	{
		Timber += amount;
		NotifyResourcesChanged();
	}

	public void AddStone(int amount)
	{
		Stone += amount;
		NotifyResourcesChanged();
	}

	public void AddWheat(int amount)
	{
		Wheat += amount;
		NotifyResourcesChanged();
	}

	public bool SpendResources(
		int timberCost,
		int stoneCost,
		int wheatCost)
		{
			if (Timber < timberCost ||
				Stone < stoneCost ||
				Wheat < wheatCost)
			{
				return false;
			}

			Timber -= timberCost;
			Stone -= stoneCost;
			Wheat -= wheatCost;

			NotifyResourcesChanged();

			return true;
		}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
