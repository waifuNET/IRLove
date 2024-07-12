using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUP : MonoBehaviour, Iteraction
{
	private PlayerInventory inventory;
	public GameObject TakeGameobject;
	public string name;
	private void Start()
	{
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
	}
	public void Iterction()
	{
		if (!inventory.heldButton)
		{
			inventory.inventory.Add(new Items() { Name = name, gameObject = TakeGameobject, OriginalObject = gameObject });
			inventory.inventoryItemScrollPosition = inventory.inventory.Count - 1;
            inventory.ItemSwitch();
			gameObject.SetActive(false);
		}
	}

}
