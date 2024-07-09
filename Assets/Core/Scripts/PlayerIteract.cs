using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIteract : MonoBehaviour
{
	public Transform PlayerCamera;
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
				if (IterctObj != null)
				{
					IterctObj.GetComponent<IRLButton_View>().Select(false); // Отключаем подсветку предыдущего объекта
				}

				closestDistance = distance;
				IterctObj = other.gameObject;
				IterctObj.GetComponent<IRLButton_View>().Select(true); // Включаем подсветку нового объекта
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject == IterctObj)
		{
			IterctObj.GetComponent<IRLButton_View>().Select(false);
			IterctObj = null;
			closestDistance = float.MaxValue;
		}

		if (interactableObjects.Contains(other.gameObject))
		{
			other.GetComponent<IRLButton_View>().Select(false);
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
