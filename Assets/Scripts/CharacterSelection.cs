using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class CharacterSelection : MonoBehaviour, IPointerClickHandler
{
    private bool chosen = false;

    [SerializeField] GameObject crossfadeObject;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (chosen)
        {
            Crossfade crossfade = crossfadeObject.GetComponent<Crossfade>();
            crossfade.LoadNextLevel();
        }
    }

    public void ChoosePOV(string name)
    {
        DialogueManager.GetInstance().SetVariableState("pov", name);
        chosen = true;
    }
}
