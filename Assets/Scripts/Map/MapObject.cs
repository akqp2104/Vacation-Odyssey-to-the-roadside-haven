using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapObject : Map
{
    [SerializeField] private TextAsset inkJSON;

    void OnMouseDown()
    {
        if (!DialogueManager.GetInstance().DialogueIsPlaying())
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            StartCoroutine(ObtainMap());
        }
    }

    private IEnumerator ObtainMap()
    {
        yield return new WaitUntil (() => !DialogueManager.GetInstance().DialogueIsPlaying());
        yield return new WaitForSeconds(0.5f);
        mapObtained = true;
        ShowMapButton();
        gameObject.SetActive(false);
    }
}