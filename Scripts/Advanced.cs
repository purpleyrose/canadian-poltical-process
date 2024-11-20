using Godot;
using System;

public partial class Advanced : Control
{
	private HSlider ApprovalSlider;
	private Label ApprovalLabel;
	private HSlider DisapprovalSlider;
	private Label DisapprovalLabel;
	private Label NetApprovalLabel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ApprovalSlider = GetNode<HSlider>("ApprovalRatingSlider");
		ApprovalLabel = GetNode<Label>("ApprovalLabel");
		DisapprovalSlider = GetNode<HSlider>("DisapprovalSlider");
		DisapprovalLabel = GetNode<Label>("DisappovalLabel");
		NetApprovalLabel = GetNode<Label>("NetApprovalLabel");
	}

	public void _on_approval_rating_slider_value_changed(float value)
	{
		ApprovalLabel.Text = "Approval Rating: " + ApprovalSlider.Value;
		if ((ApprovalSlider.Value - DisapprovalSlider.Value) > 0)
		{
			NetApprovalLabel.Text = "Net Approval Rating: +" + (ApprovalSlider.Value - DisapprovalSlider.Value);
		}
		else
		{
			NetApprovalLabel.Text = "Net Approval Rating: " + (ApprovalSlider.Value - DisapprovalSlider.Value);
		}
	}
	public void _on_disapproval_slider_value_changed(float value)
	{
		DisapprovalLabel.Text = "Disapproval Rating: " + DisapprovalSlider.Value;
		if ((ApprovalSlider.Value - DisapprovalSlider.Value) > 0)
		{
			NetApprovalLabel.Text = "Net Approval Rating: +" + (ApprovalSlider.Value - DisapprovalSlider.Value);
		}
		else
		{
			NetApprovalLabel.Text = "Net Approval Rating: " + (ApprovalSlider.Value - DisapprovalSlider.Value) ;
		}
	}

	

}
