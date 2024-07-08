using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToObject : MonoBehaviour
{
    public bool permament = false;
    public Transform target;
    public float followSpeed = 1;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (target == null) return;

        if (permament)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
        else
        {
            Vector3 desiredPosition = target.position;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * followSpeed);
            transform.position = smoothedPosition;
        }
    }
}
