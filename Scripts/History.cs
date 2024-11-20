using Godot;
using System;

public partial class History : Control
{
	private Panel AddHistoryPopupPanel;
	private Button AddHistoryButtonPanel; // Add button inside the panel
	private LineEdit HistoryTitleLineEdit; // LineEdit for the title
	private SpinBox StartYearSpinBox;
	private SpinBox EndYearSpinBox;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddHistoryPopupPanel = GetNode<Panel>("AddHistoryPanel");
		AddHistoryPopupPanel.Visible = false;
		AddHistoryButtonPanel = GetNode<Button>("AddHistoryPanel/AddButton");
		StartYearSpinBox = GetNode<SpinBox>("AddHistoryPanel/StartYearBox");
		EndYearSpinBox = GetNode<SpinBox>("AddHistoryPanel/EndYearSpinBox");
		HistoryTitleLineEdit = GetNode<LineEdit>("AddHistoryPanel/HistoryTitle");

	}

	public void _on_add_history_button_pressed()
	{
		AddHistoryPopupPanel.Visible = true;
	}
	
	public void _on_add_button_pressed()
	{
		AddHistoryPopupPanel.Visible = false;
		// Add history to the history list by creating an Hbox with the title and years
		HBoxContainer historyHBox = new HBoxContainer();
		Label historyTitle = new Label();
		historyTitle.Text = HistoryTitleLineEdit.Text;
		Label historyYears = new Label();
		historyYears.Text = StartYearSpinBox.Value + " - " + EndYearSpinBox.Value;
		historyHBox.AddChild(historyTitle);
		historyHBox.AddChild(historyYears);
		GetNode<VBoxContainer>("HistoryListBox").AddChild(historyHBox);
	}
}
