using Godot;
using System;

public partial class GeneralBox : Control
{
    private CheckBox femaleButton;
    private CheckBox otherButton;
    private CheckBox maleButton;
    private HSlider ageSlider;
    private Label AgeLabel;
    private CheckBox LPCButton;
    private CheckBox NDPButton;
    private CheckBox CPCButton;
    private CheckBox GPCButton;
    private CheckBox PPCButton;

    // Flag to prevent recursive toggling
    private bool isUpdating = false;

    public override void _Ready()
    {
        femaleButton = GetNode<CheckBox>("GenderControl/FemaleButton");
        otherButton = GetNode<CheckBox>("GenderControl/NonBinaryButton");
        maleButton = GetNode<CheckBox>("GenderControl/MaleButton");
        ageSlider = GetNode<HSlider>("AgeSlider");
        AgeLabel = GetNode<Label>("AgeLabel");
        LPCButton = GetNode<CheckBox>("PolitcalPartyControl/LPCButton");
        NDPButton = GetNode<CheckBox>("PolitcalPartyControl/NDPButton");
        CPCButton = GetNode<CheckBox>("PolitcalPartyControl/CPCButton");
        GPCButton = GetNode<CheckBox>("PolitcalPartyControl/GPCButton");
        PPCButton = GetNode<CheckBox>("PolitcalPartyControl/PPCButton");
    }

   

    public void _on_age_slider_value_changed(float value)
    {
        AgeLabel.Text = "Age: " + value;
    }

}
