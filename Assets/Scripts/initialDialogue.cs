using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class initialDialogue : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] Image background;

    void Start()
    {
        background.gameObject.SetActive(true);
        StartCoroutine(StartInitialDialogue());
    }

    private IEnumerator StartInitialDialogue()
    {
        yield return new WaitForSeconds(4f);
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        yield return new WaitUntil(() => !DialogueManager.GetInstance().DialogueIsPlaying());
        yield return new WaitForSeconds(2f);
        background.gameObject.SetActive(false);
    }
}
