using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[System.Serializable]
public class Items
{
    public string Name;
    public GameObject gameObject;
}
public class PlayerInventory : MonoBehaviour
{
    public Items CurrentItem;
	public int inventoryItemScrollPosition = 0;

    public List<Items> inventory = new List<Items>();

    void Start()
    {
        inventory.Add(new Items() { Name = "", gameObject = null });

    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            inventoryItemScrollPosition += 1;
			if (inventoryItemScrollPosition >= inventory.Count) inventoryItemScrollPosition = 0;
			
            ChangeItem(GetIteract(CurrentItem.gameObject));
			CurrentItem = inventory[inventoryItemScrollPosition];
			PickedItem(GetIteract(CurrentItem.gameObject));

		}
		else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
			inventoryItemScrollPosition -= 1;
			if (inventoryItemScrollPosition < 0) inventoryItemScrollPosition = inventory.Count - 1;
			
            ChangeItem(GetIteract(CurrentItem.gameObject));
			CurrentItem = inventory[inventoryItemScrollPosition];
			PickedItem(GetIteract(CurrentItem.gameObject));
		}

		if (Input.GetKeyUp(KeyCode.F))
        {
            ItemIteract(GetIteract(CurrentItem.gameObject));
		}
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
}
