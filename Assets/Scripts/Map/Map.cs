using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Map : MonoBehaviour
{
    [SerializeField] protected GameObject mapButton;
    protected static bool mapObtained;

    private void Start()
    {
        mapObtained = false;
    }

    public void ShowMapButton()
    {
        if (mapObtained)
        {
            mapButton.SetActive(true);
        }
    }
}