using Godot;

public partial class MainScreen : Node2D
{
	private Node2D _selector;
	private Label _output;
	private int _initialOutputSize;
	
	private Vector2 _screenSize;
	
	private Label DEBUG_HorizontalMovement;
	private Label DEBUG_VerticalMovement;

	public override void _Ready()
	{
		_selector = GetNode<Node2D>("LeftKeys/Selector");
		_output = GetNode<Label>("Output");
		_screenSize = GetViewport().GetVisibleRect().Size;
		
		_output.GlobalPosition = new Vector2(_screenSize.X * .1f, _screenSize.Y * .8f);
		_initialOutputSize = _output.Text.Length;
		
		GetNode<Sprite2D>("LeftKeys").GlobalPosition =  new Vector2(_screenSize.X * .25f, _screenSize.Y * .4f);

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

		_selector.Position = Vector2.Zero;
		var sp = _selector.Position;
		sp.X += leftStickMovement_Horizontal * selectorRadius;
		sp.Y += leftStickMovement_Vertical * selectorRadius;
		_selector.Position = sp;


		
		DEBUG_HorizontalMovement.Text = (leftStickMovement_Horizontal*selectorRadius).ToString();
		DEBUG_VerticalMovement.Text = (leftStickMovement_Vertical*selectorRadius).ToString();
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
