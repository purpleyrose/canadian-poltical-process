using Godot;
using System;
using System.Collections.Generic;
using System.IO;

public partial class CharacterCreation : Node2D
{
	
	private CheckButton LPCButton;
	private CheckButton NDPButton;
	private CheckButton CPCButton;
	private CheckButton GPCButton;
	private CheckButton PPCButton;
	private Color LPCColor = RGBToColor(234,109,106);
	private Color NDPColor = RGBToColor(244,164,96);
	private Color CPCColor = RGBToColor(100,149,237);
	private Color GPCColor = RGBToColor(153,201,85);
	private Color PPCColor = RGBToColor(111,93,154);
	private Panel Background;
	private ColorRect CharacterPortrait;

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
        LPCButton = GetNode<CheckButton>("TabContainer/General/GeneralBox/PolitcalPartyControl/LPCButton");
		LPCButton.SetPressedNoSignal(true);
		NDPButton = GetNode<CheckButton>("TabContainer/General/GeneralBox/PolitcalPartyControl/NDPButton");
		CPCButton = GetNode<CheckButton>("TabContainer/General/GeneralBox/PolitcalPartyControl/CPCButton");
		GPCButton = GetNode<CheckButton>("TabContainer/General/GeneralBox/PolitcalPartyControl/GPCButton");
		PPCButton = GetNode<CheckButton>("TabContainer/General/GeneralBox/PolitcalPartyControl/PPCButton");
		Background = GetNode<Panel>("Background");
		CharacterPortrait = GetNode<ColorRect>("Background/CharacterPortrait");
		CharacterPortrait.Color = LPCColor;

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

}
