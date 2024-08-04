using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickUP : MonoBehaviour, Iteraction, IPickUpInfo
{
	private PlayerInventory inventory;
	public GameObject TakeGameobject;
	public string name;
	public bool CanDrop = true;
	private void Start()
	{
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
		_startedRotation = gameObject.transform.rotation;
	}
	public void Iterction()
	{
		if (!inventory.heldButton && inventory.inventory.Count < 5)
		{
			inventory.inventory.Add(new Items() { Name = name, gameObject = TakeGameobject, OriginalObject = gameObject });
            inventory.inventoryItemScrollPosition = inventory.inventory.Count - 1;
            inventory.ItemSwitch();
			inventory.SetDictNewRotation();
            gameObject.SetActive(false);
		}
	}

	private Quaternion _startedRotation;
	public Quaternion GetRotation()
	{
		return _startedRotation == null ? Quaternion.identity : _startedRotation;
	}
}
