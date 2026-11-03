
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("UI References")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Toggle fullscreenToggle;

    [Header("Default Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float defaultMasterVolume = 0.8f;

    [Range(0f, 1f)]
    [SerializeField] private float defaultMusicVolume = 0.7f;

    [Range(0f, 1f)]
    [SerializeField] private float defaultSFXVolume = 0.8f;

    private const string MasterKey = "MasterVolume";
    private const string MusicKey = "MusicVolume";
    private const string SFXKey = "SFXVolume";
    private const string FullscreenKey = "Fullscreen";

    private void Start()
    {
        LoadSettings();

        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    private void LoadSettings()
    {
        float master = PlayerPrefs.GetFloat(
            MasterKey, defaultMasterVolume);

        float music = PlayerPrefs.GetFloat(
            MusicKey, defaultMusicVolume);

        float sfx = PlayerPrefs.GetFloat(
            SFXKey, defaultSFXVolume);

        bool fullscreen = PlayerPrefs.GetInt(
            FullscreenKey, Screen.fullScreen ? 1 : 0) == 1;

        masterVolumeSlider.SetValueWithoutNotify(master);
        musicVolumeSlider.SetValueWithoutNotify(music);
        sfxVolumeSlider.SetValueWithoutNotify(sfx);
        fullscreenToggle.SetIsOnWithoutNotify(fullscreen);

        ApplyMasterVolume(master);
        ApplyMusicVolume(music);
        ApplySFXVolume(sfx);
        ApplyFullscreen(fullscreen);
    }

    public void SetMasterVolume(float value)
    {
        ApplyMasterVolume(value);
        PlayerPrefs.SetFloat(MasterKey, value);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float value)
    {
        ApplyMusicVolume(value);
        PlayerPrefs.SetFloat(MusicKey, value);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float value)
    {
        ApplySFXVolume(value);
        PlayerPrefs.SetFloat(SFXKey, value);
        PlayerPrefs.Save();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        ApplyFullscreen(isFullscreen);
        PlayerPrefs.SetInt(FullscreenKey, isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void ApplyMasterVolume(float value)
    {
        audioMixer.SetFloat(
            "MasterVolume", ConvertToDecibels(value));
    }

    private void ApplyMusicVolume(float value)
    {
        audioMixer.SetFloat(
            "MusicVolume", ConvertToDecibels(value));
    }

    private void ApplySFXVolume(float value)
    {
        audioMixer.SetFloat(
            "SFXVolume", ConvertToDecibels(value));
    }

    private float ConvertToDecibels(float value)
    {
        return value <= 0.0001f
            ? -80f
            : Mathf.Log10(value) * 20f;
    }

    private void ApplyFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void ResetSettings()
    {
        masterVolumeSlider.SetValueWithoutNotify(defaultMasterVolume);
        musicVolumeSlider.SetValueWithoutNotify(defaultMusicVolume);
        sfxVolumeSlider.SetValueWithoutNotify(defaultSFXVolume);

        fullscreenToggle.SetIsOnWithoutNotify(true);

        SetMasterVolume(defaultMasterVolume);
        SetMusicVolume(defaultMusicVolume);
        SetSFXVolume(defaultSFXVolume);
        SetFullscreen(true);
    }

    private void OnDestroy()
    {
        if (masterVolumeSlider != null)
            masterVolumeSlider.onValueChanged.RemoveListener(SetMasterVolume);

        if (musicVolumeSlider != null)
            musicVolumeSlider.onValueChanged.RemoveListener(SetMusicVolume);

        if (sfxVolumeSlider != null)
            sfxVolumeSlider.onValueChanged.RemoveListener(SetSFXVolume);

        if (fullscreenToggle != null)
            fullscreenToggle.onValueChanged.RemoveListener(SetFullscreen);
    }
}
