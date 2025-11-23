using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BetweenScenesLogic : MonoBehaviour
{
    private void Awake()
    {
        var dontDestruct = FindObjectsOfType<BetweenScenesLogic>();
        if(dontDestruct.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
