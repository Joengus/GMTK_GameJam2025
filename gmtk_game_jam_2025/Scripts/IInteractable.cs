using Godot;

public interface IInteractable
{
    public void showPrompt();

    public void hidePrompt();

    public void interact();

    public InteractableType getIType();
}

public enum InteractableType
{
    Hold,
    Activate,
    Move
}