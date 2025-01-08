using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;

public partial class CharacterCreation : Node2D
{
	
	private CheckBox LPCButton;
	private CheckBox NDPButton;
	private CheckBox CPCButton;
	private CheckBox GPCButton;
	private CheckBox PPCButton;
	private CheckBox BQButton;
	private Color LPCColor = RGBToColor(234,109,106);
	private Color NDPColor = RGBToColor(244,164,96);
	private Color CPCColor = RGBToColor(100,149,237);
	private Color GPCColor = RGBToColor(153,201,85);
	private Color PPCColor = RGBToColor(111,93,154);
	private Color BQColor = RGBToColor(63,183,191);
	private Panel Background;
	private ColorRect CharacterPortrait;
	private Button ContinueButton;
	

	private static Color RGBToColor(int red, int green, int blue, float alpha = 1.0f)
    {
        // Ensure the RGB values are in the valid range (0-255)
        red = Mathf.Clamp(red, 0, 255);
        green = Mathf.Clamp(green, 0, 255);
        blue = Mathf.Clamp(blue, 0, 255);

        // Normalize the values to 0-1
        float r = red / 255.0f;
        float g = green / 255.0f;
        float b = blue / 255.0f;

        // Return a new Godot Color
        return new Color(r, g, b, alpha);
    }
    public override void _Ready()
    {
        LPCButton = GetNode<CheckBox>("TabContainer/General/GeneralBox/PolitcalPartyControl/LPCButton");
		LPCButton.SetPressedNoSignal(true);
		NDPButton = GetNode<CheckBox>("TabContainer/General/GeneralBox/PolitcalPartyControl/NDPButton");
		CPCButton = GetNode<CheckBox>("TabContainer/General/GeneralBox/PolitcalPartyControl/CPCButton");
		GPCButton = GetNode<CheckBox>("TabContainer/General/GeneralBox/PolitcalPartyControl/GPCButton");
		PPCButton = GetNode<CheckBox>("TabContainer/General/GeneralBox/PolitcalPartyControl/PPCButton");
		BQButton = GetNode<CheckBox>("TabContainer/General/GeneralBox/PolitcalPartyControl/BQButton");
		Background = GetNode<Panel>("Background");


		BQButton.Connect("toggled", new Callable(this, nameof(_on_bq_button_toggled)));
		CharacterPortrait = GetNode<ColorRect>("Background/CharacterPortraitBG");
		CharacterPortrait.Color = LPCColor;
		ContinueButton = GetNode<Button>("ContinueButton");


    }
	public void _on_lpc_button_toggled(bool button_pressed)
	{
		if (button_pressed)
		{
			// Change the character portrait to the LPC color
			CharacterPortrait.Color = LPCColor;
		}
	}
	public void _on_ndp_button_toggled(bool button_pressed)
	{
		if (button_pressed)
		{
			// Change the character portrait to the NDP color
			CharacterPortrait.Color = NDPColor;
		}
	}
	public void _on_cpc_button_toggled(bool button_pressed)
	{
		if (button_pressed)
		{
			// Change the character portrait to the CPC color
			CharacterPortrait.Color = CPCColor;
		}
	}
	public void _on_gpc_button_toggled(bool button_pressed)
	{
		if (button_pressed)
		{
			// Change the character portrait to the GPC color
			CharacterPortrait.Color = GPCColor;
		}
	}
	public void _on_ppc_button_toggled(bool button_pressed)
	{
		if (button_pressed)
		{
			// Change the character portrait to the PPC color
			CharacterPortrait.Color = PPCColor;
		}
	}
		public void _on_bq_button_toggled(bool button_pressed)
	{
		if (button_pressed)
		{
			// Change the character portrait to the BQ color
			CharacterPortrait.Color = BQColor;
		}
	}
	private List<HistoryEntry> getAllHistoryEntries()
	{
		var historyEntries = new List<HistoryEntry>();
		var historyListBox = GetNode<VBoxContainer>("TabContainer/History/HistoryListBox");
		foreach (var child in historyListBox.GetChildren())
		{
			var historyHBox = (HBoxContainer)child;
			var historyTitle = ((Label)historyHBox.GetChildren()[0]).Text;
			var historyYears = ((Label)historyHBox.GetChildren()[1]).Text;
			var years = historyYears.Split('-');
			var startYear = int.Parse(years[0].Trim());
			var endYear = int.Parse(years[1].Trim());
			var entry = new HistoryEntry
			{
				HistoryTitle = historyTitle,
				StartYear = startYear,
				EndYear = endYear
			};
			historyEntries.Add(entry);
		}
		return historyEntries;
	}
	public Dictionary<string, object> GetCurrentPolicyState()
	{
		Dictionary<string, object> policyState = new Dictionary<string, object>();
		foreach (Node node in GetTree().GetNodesInGroup("PolicyGroup"))
		{
			if (node is CheckBox checkBox)
			{
				string PolicyName = checkBox.GetParent().GetParent().Name;
				if (!policyState.ContainsKey(PolicyName))
				{
					if (checkBox.Text == "Yes" || checkBox.Text == "No")
					{
						policyState[PolicyName] = checkBox.ButtonPressed && checkBox.Text == "Yes";

					}
					else  // Maintain, Decrease & Increase Cases
					{
						if (checkBox.ButtonPressed)
						{
							policyState[PolicyName] = checkBox.Text;
						}
					}
				}
			}
		}
		GD.Print("Current Policy States: ");
		foreach (var state in policyState)
		{
			GD.Print(state.Key + ": " + state.Value);
		}
		return policyState;
	}

	public void _on_continue_button_pressed()
	{
		var GlobalState = new GameData();
		GlobalState.FirstName = GetNode<LineEdit>("TabContainer/General/GeneralBox/FirstName").Text;
		GlobalState.LastName = GetNode<LineEdit>("TabContainer/General/GeneralBox/LastName").Text;
		GlobalState.Age = (int)GetNode<HSlider>("TabContainer/General/GeneralBox/AgeSlider").Value;

		GlobalState.PoliticalPoints = (int)GetNode<SpinBox>("TabContainer/Advanced/PolticalPoints").Value;
		GlobalState.InitialNameRecognition = (int)GetNode<SpinBox>("TabContainer/Advanced/NameRecognitionSpinBox").Value;
		GlobalState.History = getAllHistoryEntries();
		GlobalState.Policies = GetCurrentPolicyState();
		if (LPCButton.ButtonPressed)
		{
			GlobalState.PoliticalParty = "Liberal";
		}
		else if (CPCButton.ButtonPressed)
		{
			GlobalState.PoliticalParty = "Conservative";
		}
		else if (NDPButton.ButtonPressed)
		{
			GlobalState.PoliticalParty = "NDP";
		}
		else if (GPCButton.ButtonPressed)
		{
			GlobalState.PoliticalParty = "Green";
		}
		else if (PPCButton.ButtonPressed)
		{
			GlobalState.PoliticalParty = "PPC";
		}
		else if (BQButton.ButtonPressed)
		{
			GlobalState.PoliticalParty = "Bloc Quebecois";
		}
		if (GlobalState.FirstName == "" || GlobalState.LastName == "")
		{
			Label errorLabel = new Label();
			errorLabel.Text = "Please enter a first and last name.";
			Background.AddChild(errorLabel);

			return;
		}
		GlobalState.SaveToFile($"{GetNode<LineEdit>("TabContainer/General/GeneralBox/FirstName").Text}_{GetNode<LineEdit>("TabContainer/General/GeneralBox/LastName").Text}_save");

		GetTree().ChangeSceneToFile("res://Scenes/SelectLocation.tscn");

		

	}



}
