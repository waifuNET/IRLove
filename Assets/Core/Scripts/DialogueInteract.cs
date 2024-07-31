using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteract : MonoBehaviour
{
    public Transform PlayerCamera;
    public LayerMask layerMask;


    private DialogueInit _dialogueInit;

    private void Update()
    {
    }

    private RaycastHit ItemRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            return hit;
            Debug.DrawRay(PlayerCamera.transform.position, PlayerCamera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
        }
        else
        {
            return hit;
            Debug.DrawRay(PlayerCamera.transform.position, PlayerCamera.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
    }
}
