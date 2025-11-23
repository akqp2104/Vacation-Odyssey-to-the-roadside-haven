using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollidersManager : MonoBehaviour
{
    Collider[] colliders;
    void Start()
    {
        colliders = FindObjectsOfType<Collider>();
    }

    public void DisableColliders()
    {
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
    }

    public void EnableColliders()
    {
        foreach (Collider collider in colliders)
        {
            collider.enabled = true;
        }
    }
}
