using Godot;
using System;

public partial class GeneralBox : Control
{
    private CheckButton femaleButton;
    private CheckButton otherButton;
    private CheckButton maleButton;
    private HSlider ageSlider;
    private Label AgeLabel;
    private CheckButton LPCButton;
    private CheckButton NDPButton;
    private CheckButton CPCButton;
    private CheckButton GPCButton;
    private CheckButton PPCButton;

    // Flag to prevent recursive toggling
    private bool isUpdating = false;

    public override void _Ready()
    {
        femaleButton = GetNode<CheckButton>("GenderControl/FemaleButton");
        otherButton = GetNode<CheckButton>("GenderControl/NonBinaryButton");
        maleButton = GetNode<CheckButton>("GenderControl/MaleButton");
        ageSlider = GetNode<HSlider>("AgeSlider");
        AgeLabel = GetNode<Label>("AgeLabel");
        LPCButton = GetNode<CheckButton>("PolitcalPartyControl/LPCButton");
        NDPButton = GetNode<CheckButton>("PolitcalPartyControl/NDPButton");
        CPCButton = GetNode<CheckButton>("PolitcalPartyControl/CPCButton");
        GPCButton = GetNode<CheckButton>("PolitcalPartyControl/GPCButton");
        PPCButton = GetNode<CheckButton>("PolitcalPartyControl/PPCButton");
    }

    public void _on_male_button_toggled(bool button_pressed)
    {
        if (isUpdating) return;

        isUpdating = true; // Prevent recursion
        if (button_pressed)
        {
            femaleButton.SetPressedNoSignal(false);
            otherButton.SetPressedNoSignal(false);
        }
        isUpdating = false; // Re-enable toggling
    }

    public void _on_female_button_toggled(bool button_pressed)
    {
        if (isUpdating) return;

        isUpdating = true;
        if (button_pressed)
        {
            maleButton.SetPressedNoSignal(false);
            otherButton.SetPressedNoSignal(false);
        }
        isUpdating = false;
    }

    public void _on_non_binary_button_toggled(bool button_pressed)
    {
        if (isUpdating) return;

        isUpdating = true;
        if (button_pressed)
        {
            maleButton.SetPressedNoSignal(false);
            femaleButton.SetPressedNoSignal(false);
        }
        isUpdating = false;
    }

    public void _on_age_slider_value_changed(float value)
    {
        AgeLabel.Text = "Age: " + value;
    }

    public void _on_lpc_button_toggled(bool button_pressed)
    {
        if (isUpdating) return;

        isUpdating = true;
        if (button_pressed)
        {
            NDPButton.SetPressedNoSignal(false);
            CPCButton.SetPressedNoSignal(false);
            GPCButton.SetPressedNoSignal(false);
            PPCButton.SetPressedNoSignal(false);
        }
        isUpdating = false;
    }

    public void _on_ndp_button_toggled(bool button_pressed)
    {
        if (isUpdating) return;

        isUpdating = true;
        if (button_pressed)
        {
            LPCButton.SetPressedNoSignal(false);
            CPCButton.SetPressedNoSignal(false);
            GPCButton.SetPressedNoSignal(false);
            PPCButton.SetPressedNoSignal(false);
        }
        isUpdating = false;
    }

    public void _on_cpc_button_toggled(bool button_pressed)
    {
        if (isUpdating) return;

        isUpdating = true;
        if (button_pressed)
        {
            LPCButton.SetPressedNoSignal(false);
            NDPButton.SetPressedNoSignal(false);
            GPCButton.SetPressedNoSignal(false);
            PPCButton.SetPressedNoSignal(false);
        }
        isUpdating = false;
    }

    public void _on_gpc_button_toggled(bool button_pressed)
    {
        if (isUpdating) return;

        isUpdating = true;
        if (button_pressed)
        {
            LPCButton.SetPressedNoSignal(false);
            NDPButton.SetPressedNoSignal(false);
            CPCButton.SetPressedNoSignal(false);
            PPCButton.SetPressedNoSignal(false);
        }
        isUpdating = false;
    }

    public void _on_ppc_button_toggled(bool button_pressed)
    {
        if (isUpdating) return;

        isUpdating = true;
        if (button_pressed)
        {
            LPCButton.SetPressedNoSignal(false);
            NDPButton.SetPressedNoSignal(false);
            CPCButton.SetPressedNoSignal(false);
            GPCButton.SetPressedNoSignal(false);
        }
        isUpdating = false;
    }
}
