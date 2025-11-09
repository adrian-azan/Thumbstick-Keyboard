using Godot;
using System;

public partial class Character : Node2D
{
	[Export]
	public String _character;

	public void SetLetters(string character)
	{
		_character = character;
		GetNode<RichTextLabel>("RichTextLabel").Text = _character;
	}
}
