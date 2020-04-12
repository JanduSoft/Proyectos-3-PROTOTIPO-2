using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingsMenuScript : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioMixer AM;

    Resolution[] resolutions;
    public Dropdown resDrop;

    private void Start()
    {
        resolutions = Screen.resolutions;

        int currResInd = 0;
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) currResInd = i;
        }

        resDrop.ClearOptions();
        resDrop.AddOptions(options);
        resDrop.value = currResInd;
        resDrop.RefreshShownValue();

        EventSystem.current.SetSelectedGameObject(resDrop.gameObject);
    }



    public void setResolution(int resInd)
    {
        Resolution res = resolutions[resInd];
        Screen.SetResolution(res.width,res.height,Screen.fullScreen);
    }

    public void setMusicVolume(float musicVol)
    {
        AM.SetFloat("AudioMixerVolume", musicVol);
    }
    
    public void setSoundVolume(float soundVol)
    {
        AM.SetFloat("GameSoundsVolume", soundVol);
    }

    public void setQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex); 
    }

    public void setFullscreen(bool TOF)
    {
        Screen.fullScreen = TOF;
    }
}
