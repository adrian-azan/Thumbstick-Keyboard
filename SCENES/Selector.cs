using Godot;
using System;

public partial class Selector : Node2D
{
	private String _focusedCharacter;

	[Export]
	private String EnterActionName;
	
	public override void _Ready()
	{
		GetNode<Area2D>("Area2D").AreaEntered += LetterCollision;
		GetNode<Area2D>("Area2D").AreaExited += LetterExitedCollision;
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed(EnterActionName))
		{
			CustomSignals._Instance.EmitSignal("AddLetterToResult", _focusedCharacter);
		}
	}

	public void LetterCollision(Area2D characterCollider)
	{
		Character character = characterCollider.GetParent<Character>();
		_focusedCharacter = character._character;
	}

	public void LetterExitedCollision(Area2D characterCollider)
	{
		_focusedCharacter = null;
	}
}
