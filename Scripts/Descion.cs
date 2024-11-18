using Godot;
using System;

public partial class Descion : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	public void _on_create_new_game_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/CharacterCreation.tscn"); 
	}
	public void _on_load_game_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/LoadGame.tscn"); // Change scene to Main.tscn
	}

	public void _on_back_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/splash_screen.tscn"); // Change scene to Main.tscn
	}
}
