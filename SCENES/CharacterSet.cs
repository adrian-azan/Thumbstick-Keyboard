using Godot;
using System;
using System.Linq;
using Godot.Collections;

public partial class CharacterSet : Node2D
{
	[Export] 
	private string _selectableCharacters;

	private Array<Character> _characterSlots;
	
	public override void _Ready()
	{
		_characterSlots = new Array<Character>(
			GetNode("Wheel/Characters").GetChildren().Cast<Character>());

		for (int i = 0; i < _selectableCharacters.Length; i++)
		{
			_characterSlots[i].SetLetters(_selectableCharacters[i].ToString());
		}

		//If there are fewer characters then slots on the wheel
		//Set slots on the wheel to empty, going counter clockwise from 12
		if (_selectableCharacters.Length < _characterSlots.Count)
		{
			for (int i = _characterSlots.Count - 1; i >= _selectableCharacters.Length; i--)
			{
				_characterSlots[i].SetLetters(string.Empty);
			}
		}
	}

	public override void _Process(double delta)
	{
		//Check if the wheel is not visible and disables all character slots
		if (!GetNode<Node2D>("Wheel").Visible && _characterSlots[0].ProcessMode != ProcessModeEnum.Disabled)
		{
			foreach (var character in _characterSlots)
			{
				character.ProcessMode = ProcessModeEnum.Disabled;
				GD.Print($"{character._character} - Disabled");
			}
		}
		
		//Checks if wheel is visiable and enables all character slots
		if (GetNode<Node2D>("Wheel").Visible && _characterSlots[0].ProcessMode == ProcessModeEnum.Disabled)
		{
			foreach (var character in _characterSlots)
			{
				character.ProcessMode = ProcessModeEnum.Inherit;
				GD.Print($"{character._character} - Enabled");
			}
		}
	}
}
