using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SpecialObject : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSON;

    void OnMouseDown()
    {
        if (!DialogueManager.GetInstance().DialogueIsPlaying())
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            gameObject.SetActive(false);
        }
    }
}