using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIteract : MonoBehaviour
{
	public Camera cam;

	// See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries
	public LayerMask layerMask;
	public bool isVisible = false;
	void FixedUpdate()
	{
		RaycastHit hit;
		// Does the ray intersect any objects excluding the player layer
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
		{
			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
			isVisible = true;
		}
		else
		{
			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
			isVisible = false;
		}
	}
	public GameObject IterctObj;
	
	private void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Iteract" && isVisible)
		{
			if (other.TryGetComponent(out IRLButton_View view))
			{
				view.Select(true);
				IterctObj = other.gameObject;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Iteract")
		{
			if (other.TryGetComponent(out IRLButton_View view))
			{
				view.Select(false);
				IterctObj = null;
			}
		}
	}

	public void Update()
	{
		if(isVisible && IterctObj && Input.GetKeyDown(KeyCode.E))
		{
			IterctObj.GetComponent<Iteraction>().Iterction();
		}
	}
}
