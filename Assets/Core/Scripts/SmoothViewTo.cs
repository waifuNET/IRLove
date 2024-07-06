using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SmoothViewTo : MonoBehaviour
{
	public Transform viewTo;
	public float speed = 1;

    void Start()
    {
	}
	void Update()
    {
		Vector3 cameraPoint = Camera.main.transform.position + Camera.main.transform.forward * 3f;

		// Determine which direction to rotate towards
		Vector3 targetDirection = cameraPoint - transform.position;

		// The step size is equal to speed times frame time.
		float singleStep = speed * Time.deltaTime;

		// Rotate the forward vector towards the target direction by one step
		Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

		// Draw a ray pointing at our target in
		Debug.DrawRay(transform.position, newDirection, Color.red);

		// Calculate a rotation a step closer to the target and applies rotation to this object
		transform.rotation = Quaternion.LookRotation(newDirection, Vector3.forward);
		//Quaternion lookRotation = Quaternion.LookRotation((rootObject.transform.position - viewTo.transform.position).normalized);
		//rootObject.transform.rotation = Quaternion.Slerp(rootObject.transform.rotation, lookRotation, 5f * Time.deltaTime);
	}
}
