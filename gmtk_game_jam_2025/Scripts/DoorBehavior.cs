using Godot;

public partial class DoorBehavior : StaticBody3D, IInteractable
{
    [Export] public Tween.TransitionType TransitionType;
    [Export] Node3D openPrompt;
    [Export] Node3D lockedPrompt;
    [Export] Node3D openPos;
    [Export] Node3D closedPos;
    private bool active = false;
    [Export] bool unlocked = true;
    [Export] private float _tweenDuration = 1.5f; // Example duration
    private TentBehavoir _tent;


    private InteractableType iType = InteractableType.Activate;

    public override void _Ready()
    {
        _tent = GetNode<TentBehavoir>("../../../ClosetRoom/Tent/StaticBody3D");
        if (_tent == null) GD.Print("I don't know da wei");
    }

    public void interact()
    {
        GameManager.Instance.StartGame();
        _tent.AllowSleep();
        if (!active && unlocked)
        {
            active = true;
            hidePrompt();

            Tween newTween = CreateTween();

            newTween.SetTrans(TransitionType);

            newTween.TweenProperty(this, "global_position", openPos.GlobalPosition, _tweenDuration);
            newTween.TweenInterval(3f);
            newTween.Chain().TweenProperty(this, "global_position", closedPos.GlobalPosition, _tweenDuration);
            newTween.Connect("finished", new Callable(this, nameof(OnTweenFinished))); // Connect the finished signal

            newTween.Play();
        }
    }

    private void OnTweenFinished()
    {
        active = false;
        GD.Print("Object reached its target position!");
    }

    public void showPrompt()
    {
        if (!active)
        {
            if (unlocked)
            {
                openPrompt.Visible = true;
            }
            else
            {
                lockedPrompt.Visible = true;
            }
        }
    }

    public void hidePrompt()
    {
        openPrompt.Visible = false;
        lockedPrompt.Visible = false;
    }

    public InteractableType getIType()
    {
        return iType;
    }
}
