using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LookAtItem : MonoBehaviour, ItemInterface
{
	public bool status;
	private Transform player;
	private Transform camera;
	public GameObject texture;
	private GameObject _lookAtItem;
	private PlayerInventory inventory;
	private FirstPersonLook FPL;
	private FirstPersonMovement FPM;
	private Quaternion _lastRotation;
	private Quaternion _firstRotation;
	private GoToObject gotoObject;

	public float rotationSpeed = 100.0f;
	private bool isRotating;

	public void ChangeItem()
	{
		status = false;
		texture.SetActive(false);
		if (FPL != null && FPM != null) { FPL.UnLockCamera(); FPM.UnLockMovement(); }
		_lastRotation = _firstRotation;
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
		if (FPL != null && FPM != null) { FPL.UnLockCamera(); FPM.UnLockMovement(); }
	}

	private void Start()
	{
		if (_lookAtItem == null)
		{
			player = GameObject.FindGameObjectWithTag("Player").transform;
			camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
			inventory = player.GetComponent<PlayerInventory>();
			FPL = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FirstPersonLook>();
			FPM = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonMovement>();
			_lookAtItem = GameObject.Find("LookAtItem");
			gotoObject = gameObject.GetComponent<GoToObject>();
		}
		_lastRotation = transform.rotation;
		_firstRotation = transform.rotation;
	}

	private void LockRotate(bool state)
	{
		if (gotoObject != null)
			gotoObject.lockRotation = state;
	}

	void Update()
	{
		if (!status) return;

		if (!isRotating)
		{
			transform.rotation = camera.rotation * _lastRotation;
		}

		if(inventory.canRotate)
		{
			MouseButtonDown();
        }


        if (isRotating)
		{
			float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
			float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

			transform.Rotate(Vector3.up, -mouseX, Space.Self);
			transform.Rotate(Vector3.right, mouseY, Space.Self);

			//transform.position = _lookAtItem.transform.position; 
			_lastRotation = Quaternion.Inverse(camera.rotation) * transform.rotation;
		}

		MouseButtonUp();
    }

	public void MouseButtonDown()
	{
        if (Input.GetMouseButtonDown(0))
        {
            isRotating = true;
            FPL.LockCamera();
			FPM.LockMovement();
            LockRotate(true);
        }
    }

	public void MouseButtonUp()
	{
        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
            FPL.UnLockCamera();
			FPM.UnLockMovement();
			LockRotate(false);
        }
    }
}
