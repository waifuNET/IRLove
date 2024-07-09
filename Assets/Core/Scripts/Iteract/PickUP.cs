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
		gameObject.SetActive(false);
		inventory.inventory.Add(new Items() { Name = name, gameObject = TakeGameobject, OriginalObject = gameObject});
	}

	public void SetActive(bool status)
	{
		
	}
}
