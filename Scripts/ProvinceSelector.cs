using Godot;
using System;

public partial class ProvinceSelector : Control
{
	private Button OntarioButton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	private void _on_ontario_pressed()
	{
		GD.Print("Ontario Button Pressed");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
