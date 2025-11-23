using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Door : MonoBehaviour
{
    [Header("Next room position")]
    [SerializeField] private float x;
    [SerializeField] private float y;

    [Header("Map")]
    //[SerializeField] GameObject map;
    [SerializeField] private Image map;
    [SerializeField] private Sprite nextRoomMapSprite;

    void OnMouseDown()
    {
        ChangeRoom();
    }

    protected void ChangeRoom()
    {
        if (!DialogueManager.GetInstance().DialogueIsPlaying())
        {
            Vector3 newPosition = new Vector3(x, y, -10f);
            Camera.main.transform.position = newPosition;
            ChangeMap(nextRoomMapSprite);
        }
    }

    private void ChangeMap(Sprite mapSprite)
    {
        map.sprite = nextRoomMapSprite;
    }
}
