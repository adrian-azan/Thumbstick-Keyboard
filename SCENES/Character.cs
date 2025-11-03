using Godot;
using System;

public partial class Character : Node2D
{
	[Export]
	public String _character;
	public override void _Ready()
	{
		GetNode<RichTextLabel>("RichTextLabel").Text = _character;
	}
}
