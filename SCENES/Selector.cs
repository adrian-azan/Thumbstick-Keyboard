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
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed(EnterActionName))
			CustomSignals._Instance.EmitSignal("AddLetterToResult", _focusedCharacter);
	}

	public void LetterCollision(Area2D characterCollider)
	{
		Character character = characterCollider.GetParent<Character>();
		_focusedCharacter = character._character;
	}
}
