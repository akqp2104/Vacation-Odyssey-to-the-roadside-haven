using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionDoor : MonoBehaviour
{
    [Header("Next room position")]
    [SerializeField] private float x;
    [SerializeField] private float y;

    [Header("InkJSON")]
    [SerializeField] TextAsset HazelInkJSON;
    [SerializeField] TextAsset CalahanInkJSON;

    [Header("Door sprite")]
    [SerializeField] Sprite doorUnlocked;

    void OnMouseDown()
    {
        if (!DialogueManager.GetInstance().DialogueIsPlaying())
        {
            StartCoroutine(DoorUnlocked());
        }
    }

    private IEnumerator DoorUnlocked()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = doorUnlocked;
        yield return new WaitForSeconds(1);
        DialogueManager.GetInstance().EnterDialogueMode(HazelInkJSON);
        yield return new WaitForSeconds(1);
        ChangeRoom();
        yield return new WaitForSeconds(1);
        DialogueManager.GetInstance().EnterDialogueMode(CalahanInkJSON);
    }

    private void ChangeRoom()
    {
        Vector3 newPosition = new Vector3(x, y, -10f);
        Camera.main.transform.position = newPosition;
    }
}
