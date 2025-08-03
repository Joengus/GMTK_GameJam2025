using Godot;

public partial class TentBehavoir : StaticBody3D, IInteractable
{
    [Export] Node3D SleepPrompt;
    private InteractableType iType = InteractableType.Activate;
    private bool _canSleep = false;


    public void interact()
    {
        if (_canSleep)
        {
            GameManager.Instance.ResetDay();
        }
    }

    public void showPrompt()
    {
        if (_canSleep) SleepPrompt.Visible = true;
    }


    public void hidePrompt()
    {
        SleepPrompt.Visible = false;
    }

    public InteractableType getIType()
    {
        return iType;
    }

    public void AllowSleep()
    {
        GD.Print("allowing sleep");
        _canSleep = true;
    }
}
