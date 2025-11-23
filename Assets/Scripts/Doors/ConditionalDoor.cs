using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConditionalDoor : Door
{
    [Header("Door locked message and variable name to unlock")]
    [SerializeField] private TextAsset lockedMessage;
    [SerializeField] private string variableName;

    void OnMouseDown()
    {
        if (!DialogueManager.GetInstance().DialogueIsPlaying())
        {
            bool locked = (Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState(variableName);

            if (locked)
                DialogueManager.GetInstance().EnterDialogueMode(lockedMessage);
            else
                ChangeRoom();
        }
    }
}
