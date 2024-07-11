using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToObject : MonoBehaviour
{
	public bool permament = false;
	public Transform target;
	public float followSpeed = 1;
	public Vector3 offsetRotation = Vector3.zero;
	public Vector3 offsetPosition = Vector3.zero;

	void FixedUpdate()
	{
		if (target == null) return;

		Vector3 desiredPosition = target.position + target.TransformDirection(offsetPosition);
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * followSpeed);

		if (permament)
		{
			transform.position = desiredPosition;
			transform.rotation = target.rotation * Quaternion.Euler(offsetRotation);
		}
		else
		{
			transform.position = smoothedPosition;
		}
	}
}
