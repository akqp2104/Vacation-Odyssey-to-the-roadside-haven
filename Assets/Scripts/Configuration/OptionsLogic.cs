using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsLogic : MonoBehaviour
{
    public OptionsController optionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        optionsMenu = GameObject.FindGameObjectWithTag("Options").GetComponent<OptionsController>();
    }

    public void ShowOptionsMenu()
    {
        optionsMenu.optionsScreen.SetActive(true);
    }
}
