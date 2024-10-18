using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAndVibroUI : MonoBehaviour
{
    [SerializeField] private GameObject _auidoOn;
    [SerializeField] private GameObject _auidoOff;
    [SerializeField] private GameObject _vibroOn;
    [SerializeField] private GameObject _vibroOff;

    private bool _canVibro;

    private void Start()
    {
        int audio = PlayerPrefs.GetInt("AudioVolume", 1);
        if (audio == 1) AudioOn();
        else AudioOff();

        int vibro = PlayerPrefs.GetInt("VibroCan", 1);
        if (vibro == 1) VibroOn();
        else VibroOff();
    }

    public void AudioOff()
    {
        _auidoOn.SetActive(false);
        _auidoOff.SetActive(true);
        AudioListener.volume = 0;
        PlayerPrefs.SetInt("AudioVolume", 0);
    }

    public void AudioOn()
    {
        _auidoOff.SetActive(false);
        _auidoOn.SetActive(true);
        AudioListener.volume = 1;
        PlayerPrefs.SetInt("AudioVolume", 1);
    }

    public void VibroOff()
    {
        _vibroOn.SetActive(false);
        _vibroOff.SetActive(true);
        _canVibro = false;
        PlayerPrefs.SetInt("VibroCan", 0);
    }

    public void VibroOn()
    {
        _vibroOff.SetActive(false);
        _vibroOn.SetActive(true);
        _canVibro = true;
        PlayerPrefs.SetInt("VibroCan", 1);
    }
}