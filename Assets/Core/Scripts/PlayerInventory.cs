using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[System.Serializable]
public class Items
{
    public string Name;
    public GameObject gameObject;
	public GameObject OriginalObject;
}
public class PlayerInventory : MonoBehaviour
{
    public Items CurrentItem;
	public int inventoryItemScrollPosition = 0;

	public Transform PlayerCamera;
    public float timeButton;

    public LayerMask layerMask;
    public Vector3 itemNewPos;
    private int buttonCounter = 0;
    Vector3 tempPos;

    public KeyCode pressedButton;
    public List<Items> inventory = new List<Items>();

    void Start()
    {
        inventory.Add(new Items() { Name = "", gameObject = null });
		PlayerCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            inventoryItemScrollPosition += 1;
			if (inventoryItemScrollPosition >= inventory.Count) inventoryItemScrollPosition = 0;

			ItemSwitch();
		}
		else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
			inventoryItemScrollPosition -= 1;
			if (inventoryItemScrollPosition < 0) inventoryItemScrollPosition = inventory.Count - 1;

			ItemSwitch();
		}
		

		if (Input.GetKeyUp(KeyCode.F))
        {
            ItemIteract(GetIteract(CurrentItem.gameObject));
		}
		
		if(Input.GetKeyDown(KeyCode.G))
		{
            ItemPut();
            //buttonCounter++;
            //timeButton = Time.fixedDeltaTime;
            //if(buttonCounter == 2)
            //{
            //    ItemPut();
            //    //ItemDrop();
            //    buttonCounter = 0;
            //}
            //else if(Input.GetKeyDown(KeyCode.E))
            //{
            //    ItemDrop();
            //    //ItemPut();
            //    buttonCounter = 0;
            //}

        }
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(PlayerCamera.transform.position, PlayerCamera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
            tempPos = hit.point;
        }
        else
        {
            Debug.DrawRay(PlayerCamera.transform.position, PlayerCamera.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
    }
    private void SetItemNewPos(Vector3 h)
    {
        if(CurrentItem.OriginalObject.GetComponent<BoxCollider>() != null)
        {
            itemNewPos = new Vector3(
            h.x,
            h.y+ CurrentItem.OriginalObject.GetComponent<BoxCollider>().size.y/2f,
            h.z
            );
        }
        else if(CurrentItem.OriginalObject.GetComponent<CapsuleCollider>() != null ) 
        {
            itemNewPos = new Vector3(
            h.x,
            h.y + CurrentItem.OriginalObject.GetComponent<CapsuleCollider>().radius/2f,
            h.z
            );
        }
        else if (CurrentItem.OriginalObject.GetComponent<SphereCollider>() != null)
        {
            itemNewPos = new Vector3(
            h.x,
            h.y + CurrentItem.OriginalObject.GetComponent<SphereCollider>().radius /2f,
            h.z
            );
        }
    }
    public void ItemSwitch()
	{
        if(inventoryItemScrollPosition > inventory.Count-1 || inventoryItemScrollPosition < 0)
        {
            inventoryItemScrollPosition = 0;
        }
		ChangeItem(GetIteract(CurrentItem.gameObject));
		CurrentItem = inventory[inventoryItemScrollPosition];
		PickedItem(GetIteract(CurrentItem.gameObject));
	}

	private ItemInterface GetIteract(GameObject go)
    {
		if (go != null)
		{
			if (go.TryGetComponent<ItemInterface>(out ItemInterface item))
			{
                return item;
			}
		}
        return null;
	}
	
    private void ItemIteract(ItemInterface item)
    {
        if (item == null) return;
        item.Iterction();
    } 

	private void ItemDrop()
	{
        GameObject origObj = CurrentItem.OriginalObject;
		if (origObj.GetComponent<Rigidbody>() == null)
        {
            Rigidbody rr = origObj.gameObject.AddComponent<Rigidbody>();
			rr.mass = 1;
			rr.angularDamping = 1.75f;
        }
        Rigidbody rg = origObj.GetComponent<Rigidbody>();
        origObj.transform.position = PlayerCamera.transform.position;
        origObj.SetActive(true);
        rg.AddForce(PlayerCamera.transform.TransformDirection(Vector3.forward), ForceMode.Impulse);
		RemoveItem(CurrentItem);
    }
    private void ItemPut()
    {
        SetItemNewPos(tempPos);
        GameObject origObj = CurrentItem.OriginalObject;
        origObj.transform.position = itemNewPos;
        origObj.SetActive(true);
        RemoveItem(CurrentItem);
    }
    private void ChangeItem(ItemInterface item)
    {
		if (item == null) return;
		item.ChangeItem();
    }
	private void PickedItem(ItemInterface item)
	{
		if (item == null) return;
		item.PickedItem();
	}
	private void RemoveItem(Items item)
	{
		if (item == null) return;
        GetIteract(item.gameObject).ChangeItem();
        inventoryItemScrollPosition -= 1;
        ItemSwitch();
        inventory.Remove(item);
    }
}
