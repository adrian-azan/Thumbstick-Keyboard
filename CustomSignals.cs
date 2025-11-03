using Godot;
using System;

public partial class CustomSignals : Node
{
    public static CustomSignals _Instance;

    public override void _Ready()
    {
        base._Ready();

        _Instance = this;
    }

    [Signal]
    public delegate void AddLetterToResultEventHandler(string letter);
    
    [Signal]
    public delegate void RemoveLetterFromResultEventHandler();
}