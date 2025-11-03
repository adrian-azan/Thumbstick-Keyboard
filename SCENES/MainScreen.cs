using Godot;

public partial class MainScreen : Node2D
{
	private Node2D _leftSelector;
	private Node2D _rightSelector;
	private Label _output;
	private int _initialOutputSize;
	
	private Vector2 _screenSize;
	
	private Label DEBUG_HorizontalMovement;
	private Label DEBUG_VerticalMovement;

	public override void _Ready()
	{
		_leftSelector = GetNode<Node2D>("LeftKeys/Selector");
		_rightSelector = GetNode<Node2D>("RightKeys/Selector");
		_output = GetNode<Label>("Output");
		_screenSize = GetViewport().GetVisibleRect().Size;
		
		_output.GlobalPosition = new Vector2(_screenSize.X * .1f, _screenSize.Y * .8f);
		_initialOutputSize = _output.Text.Length;
		
		GetNode<Sprite2D>("LeftKeys").GlobalPosition =  new Vector2(_screenSize.X * .25f, _screenSize.Y * .4f);
		GetNode<Sprite2D>("RightKeys").GlobalPosition =  new Vector2(_screenSize.X * .75f, _screenSize.Y * .4f);

		CustomSignals._Instance.AddLetterToResult += UpdateText;
		CustomSignals._Instance.RemoveLetterFromResult += RemoveText;
		
		
		//---- Debug inits: Make DEBUG Nodes visible in editor ----
		DEBUG_HorizontalMovement = GetNode<Label>("DEBUG_Horizontal");
		DEBUG_VerticalMovement = GetNode<Label>("DEBUG_Vertical");

		DEBUG_HorizontalMovement.GlobalPosition = new Vector2(_screenSize.X * .25f, _screenSize.Y * .75f);
		DEBUG_VerticalMovement.GlobalPosition = new Vector2(_screenSize.X * .25f,( _screenSize.Y * .75f) + (_screenSize.Y * .05f));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float selectorRadius = 25;
		float leftStickMovement_Horizontal = Input.GetAxis("Left_Left", "Left_Right");
		float leftStickMovement_Vertical = Input.GetAxis("Left_Up", "Left_Down");
		float rightStickMovement_Horizontal = Input.GetAxis("Right_Left", "Right_Right");
		float rightStickMovement_Vertical = Input.GetAxis("Right_Up", "Right_Down");

		_leftSelector.Position = Vector2.Zero;
		var sp = _leftSelector.Position;
		sp.X += leftStickMovement_Horizontal * selectorRadius;
		sp.Y += leftStickMovement_Vertical * selectorRadius;
		_leftSelector.Position = sp;

		_rightSelector.Position = Vector2.Zero;
		sp = _rightSelector.Position;
		sp.X += rightStickMovement_Horizontal * selectorRadius;
		sp.Y += rightStickMovement_Vertical * selectorRadius;
		_rightSelector.Position = sp;

		
		DEBUG_HorizontalMovement.Text = (leftStickMovement_Horizontal*selectorRadius).ToString();
		DEBUG_VerticalMovement.Text = (leftStickMovement_Vertical*selectorRadius).ToString();
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("Back"))
			RemoveText();
	}

	public void UpdateText(string letter)
	{
		_output.Text += letter;
	}

	public void RemoveText()
	{
		if (_output.Text.Length <= _initialOutputSize)
			return;
		
		_output.Text = _output.Text.Remove(_output.Text.Length-1);
	}
}
