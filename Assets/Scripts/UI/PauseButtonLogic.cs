using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonLogic : MonoBehaviour
{
    [SerializeField] private GameObject pauseButton;

    public void ShowOptionsButton()
    {
        pauseButton.SetActive(true);
    }
}
