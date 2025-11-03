using Godot;
using System;

public partial class Selector : Node2D
{
	private String _focusedCharacter;

	public override void _Ready()
	{
		GetNode<Area2D>("Area2D").AreaEntered += LetterCollision;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("LeftEnter"))
			CustomSignals._Instance.EmitSignal("AddLetterToResult", _focusedCharacter);
		
		if (@event.IsActionPressed("Back"))
			CustomSignals._Instance.EmitSignal("RemoveLetterFromResult");
	}

	public void LetterCollision(Area2D characterCollider)
	{
		Character character = characterCollider.GetParent<Character>();
		_focusedCharacter = character._character;
	}
}
