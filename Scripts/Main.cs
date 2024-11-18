using Godot;
using System;

public partial class Main : Node2D
{
	public const int StartingYear = 2020;
	public const int StartingWeek = 1;
	private Label PoliticalPointsLabel;
	private Label DateLabel;
	public int PoliticalPoints = 0;
	public Button NextWeekButton;
	private int CurrentWeek = StartingWeek;
	public int CurrentYear = StartingYear;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NextWeekButton = GetNode<Button>("Panel/NextWeekButton");
		NextWeekButton.Connect("pressed", new Callable(this, "_on_next_week_button_pressed"));
		DateLabel = GetNode<Label>("Panel/DateLabel");
		DateLabel.Text = "Year " + StartingYear + ", Week " + StartingWeek;

		PoliticalPointsLabel = GetNode<Label>("Panel/PoliticalPointsLabel");
		PoliticalPointsLabel.Text = "Political Points: " + PoliticalPoints;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public void _on_next_week_button_pressed()
	{
		CurrentWeek++;
		if (CurrentWeek > 52)
		{
			CurrentWeek = 1;
			CurrentYear++;
		}
		DateLabel.Text = "Year " + CurrentYear + ", Week " + CurrentWeek;
	}
	public override void _Process(double delta)
	{
	}
}
