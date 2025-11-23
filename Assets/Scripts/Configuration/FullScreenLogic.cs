using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FullScreenLogic : MonoBehaviour
{
    public Toggle toggle;
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions = new Resolution[5];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < resolutions.Length-1; i++)
        {
            resolutions[i] = new Resolution();
            SetResolution(640*(i+1), 360*(i+1), ref resolutions[i]);
        }
        SetResolution(3840, 2160, ref resolutions[4]);

        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }

        CheckResolution();
    }

    void SetResolution(int width, int height, ref Resolution resolution)
    {
        resolution.width = width;
        resolution.height = height;
    }

    public void ActivateFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    public void CheckResolution()
    {
        int currentResolution = 2;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolution = i;
            }
        }
        resolutionDropdown.value = currentResolution;
        resolutionDropdown.value = PlayerPrefs.GetInt("resolutionIndex", 2);
    }

    public void ChangeResolution(int resolutionIndex)
    {
        PlayerPrefs.SetInt("resolutionIndex", resolutionDropdown.value);
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
