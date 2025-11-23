using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpecialObjecContainer : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private GameObject specialObject;
    private bool found = false;

    void OnMouseDown()
    {
        if (!found)
        {
            specialObject.SetActive(true);
            found = true;
        }
        else
        {
            if (!DialogueManager.GetInstance().DialogueIsPlaying())
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
    }
}