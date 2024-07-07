using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SmoothViewTo : MonoBehaviour
{
	public Transform target;
	public float smoothSpeed = 1;

    void Start()
	{
	}
	void Update()
    {
		Vector3 direction = target.position - transform.position;
		Quaternion targetRotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
	}
}
