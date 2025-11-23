using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [Header("Door locked message and variable name to unlock")]
    [SerializeField] private TextAsset lockedMessage;
    [SerializeField] private string variableName;

    [Header("Crossfade")]
    [SerializeField] private GameObject crossfadeObject;

    void OnMouseDown()
    {
        if (!DialogueManager.GetInstance().DialogueIsPlaying())
        {
            bool locked = (Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState(variableName);

            if (locked)
                DialogueManager.GetInstance().EnterDialogueMode(lockedMessage);
            else
            {
                Crossfade crossfade = crossfadeObject.GetComponent<Crossfade>();
                crossfade.LoadNextLevel();
            }
        }
    }
}
