using Godot;

public partial class MainMenu : Control
{
	public static void OnPlayButtonPressed()
	{
		GD.Print("Play button pressed");
		// AudioServer.GetBusIndex("Master");
	}

	public void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}

	public void OnVolumeSliderChanged(float value)
	{
		AudioServer.SetBusVolumeLinear(AudioServer.GetBusIndex("Master"), value);
		GD.Print("Master Decibels: " + AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Master")));
		GD.Print("Master Linear: " + AudioServer.GetBusVolumeLinear(AudioServer.GetBusIndex("Master")));
	}
}
