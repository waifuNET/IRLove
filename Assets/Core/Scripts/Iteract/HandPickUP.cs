using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPickUP : MonoBehaviour, Iteraction
{
    private PlayerInventory inventory;
    public GameObject TakeGameobject;
    private Vector3 originalPosition = Vector3.zero;
    public void Iterction()
    {
        
    }
    private Quaternion _startedRotation;
    public Quaternion GetRotation()
    {
        return _startedRotation == null ? Quaternion.identity : _startedRotation;
    }

    public Vector3 GetOriginalPosition()
    {
        return originalPosition;
    }

    public void SetOriginalPosition()
    {
        gameObject.transform.position = originalPosition;
    }
}
