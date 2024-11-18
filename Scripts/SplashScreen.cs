using Godot;
using System;

public partial class SplashScreen : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	public void _on_start_game_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Descion.tscn"); // Change scene to Main.tscn
	}

	public void _on_quit_game_button_pressed()
	{
		GetTree().Quit(); // Quit the game
	}
	
}
