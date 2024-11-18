using Godot;
using System;

public partial class Descion : Node2D
{
	private const string CharacterCreationScenePath = "res://Scenes/CharacterCreation.tscn";
	private const string LoadGameScenePath = "res://Scenes/LoadGame.tscn";
	private const string SplashScreenScenePath = "res://Scenes/splash_screen.tscn";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	public void _on_create_new_game_button_pressed()
	{
		GetTree().ChangeSceneToFile(CharacterCreationScenePath); // Change scene to CharacterCreation.tscn
	}
	public void _on_load_game_button_pressed()
	{
		GetTree().ChangeSceneToFile(LoadGameScenePath); // Change scene to LoadGame.tscn
	}

	public void _on_back_button_pressed()
	{
		GetTree().ChangeSceneToFile(SplashScreenScenePath); // Change scene to splash_screen.tscn
	}
}
