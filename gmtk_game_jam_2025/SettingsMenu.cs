using Godot;

public partial class SettingsMenu : Control
{
	private string _masterAudioBusName = "Master";
	private string _sfxAudioBusName = "SFX";
	private int _masterAudioBus;
	private int _sfxAudioBus;

	public override void _Ready()
	{
		_masterAudioBus = AudioServer.GetBusIndex(_masterAudioBusName);
		_sfxAudioBus = AudioServer.GetBusIndex(_sfxAudioBusName);
	}

	public void OnMusicVolumeSliderChanged(float value)
	{
		AudioServer.SetBusVolumeLinear(_masterAudioBus, value);
	}
}
