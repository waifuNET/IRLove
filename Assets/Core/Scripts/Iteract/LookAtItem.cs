using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtItem : MonoBehaviour, ItemInterface
{
	public bool status;
	public GameObject texture;
	private GameObject _lookAtItem;
	private PlayerInventory inventory;
	private FirstPersonLook FPL;

	public float rotationSpeed = 100.0f;
	private bool isRotating;

	public void ChangeItem()
	{
		status = false;
		texture.SetActive(false);
		if (FPL != null) FPL.UnLockCamera();
	}

	public bool GetStatus()
	{
		return status;
	}

	public void Iterction()
	{

	}

	public void PickedItem()
	{
		status = true;
		texture.SetActive(true);
		if (FPL != null) FPL.UnLockCamera();
	}

	private void Start()
	{
		if (_lookAtItem == null)
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			inventory = player.GetComponent<PlayerInventory>();
			FPL = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FirstPersonLook>();
			_lookAtItem = GameObject.Find("LookAtItem");
			gameObject.GetComponent<GoToObject>().lockRotation = true;
		}
	}

	private void Update()
	{
		if (!status) return;
		if (Input.GetMouseButtonDown(0))
		{
			isRotating = true;
			FPL.LockCamera();
		}
		if (isRotating)
		{
			float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
			float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

			transform.Rotate(Vector3.up, -mouseX, Space.World);
			transform.Rotate(Vector3.right, mouseY, Space.World);

			transform.position = _lookAtItem.transform.position;
		}
		if (Input.GetMouseButtonUp(0))
		{
			isRotating = false;
			FPL.UnLockCamera();
		}
	}
}
