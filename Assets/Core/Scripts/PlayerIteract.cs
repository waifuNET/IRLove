using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIteract : MonoBehaviour
{
	public Transform PlayerCamera;
	public Image PlayerCrosshair;
	public Sprite defaultCrosshair;
	public Sprite iteractCrosshair;
	public Camera cam;

	// See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries
	public LayerMask layerMask;
	public bool isVisible = false;

	void FixedUpdate()
	{
		RaycastHit hit;
		// Does the ray intersect any objects excluding the player layer
		if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
		{
			Debug.DrawRay(PlayerCamera.transform.position, PlayerCamera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
			isVisible = true;
		}
		else
		{
			Debug.DrawRay(PlayerCamera.transform.position, PlayerCamera.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
			isVisible = false;
		}
	}
	public GameObject IterctObj;

	private float closestDistance = float.MaxValue;
	private List<GameObject> interactableObjects = new List<GameObject>();

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Iteract" && isVisible)
		{
			if (!interactableObjects.Contains(other.gameObject))
			{
				interactableObjects.Add(other.gameObject);
			}

			float distance = Vector3.Distance(transform.position, other.transform.position);

			if (distance < closestDistance)
			{
				closestDistance = distance;
				IterctObj = other.gameObject;
				PlayerCrosshair.sprite = iteractCrosshair;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject == IterctObj)
		{
			PlayerCrosshair.sprite = defaultCrosshair;
			IterctObj = null;
			closestDistance = float.MaxValue;
		}

		if (interactableObjects.Contains(other.gameObject))
		{
			PlayerCrosshair.sprite = defaultCrosshair;
			interactableObjects.Remove(other.gameObject);
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
