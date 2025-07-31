using Godot;

public partial class SettingsMenu : Control
{
	private string _masterAudioBusName = "Master";
	private string _musicAudioBusName = "Music";
	private string _sfxAudioBusName = "SFX";
	private int _masterAudioBus;
	private int _musicAudioBus;
	private int _sfxAudioBus;

	private HSlider _masterSlider;
	private HSlider _musicSlider;
	private HSlider _sfxSlider;

	public override void _Ready()
	{
		_masterAudioBus = AudioServer.GetBusIndex(_masterAudioBusName);
		_musicAudioBus = AudioServer.GetBusIndex(_musicAudioBusName);
		_sfxAudioBus = AudioServer.GetBusIndex(_sfxAudioBusName);
		AudioServer.SetBusVolumeLinear(_masterAudioBus, (float)GetNode<HSlider>("Panel/MasterSlider").Value);
		AudioServer.SetBusVolumeLinear(_masterAudioBus, (float)GetNode<HSlider>("Panel/MusicSlider").Value);
		AudioServer.SetBusVolumeLinear(_sfxAudioBus, (float)GetNode<HSlider>("Panel/SfxSlider").Value);
	}

	public void OnMasterVolumeChanged(float value)
	{
		AudioServer.SetBusVolumeLinear(_masterAudioBus, value);
	}

	public void OnMusicVolumeChanged(float value)
	{
		AudioServer.SetBusVolumeLinear(_musicAudioBus, value);
	}

	public void OnSfxVolumeChanged(float value)
	{
		AudioServer.SetBusVolumeLinear(_sfxAudioBus, value);
	}

	public void OnReturnButtonPressed()
	{
		Visible = false;
	}
}
