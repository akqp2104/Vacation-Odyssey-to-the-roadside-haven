using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NormalObject : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSON;

    void OnMouseDown()
    {
        if (!DialogueManager.GetInstance().DialogueIsPlaying())
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        }
    }
}