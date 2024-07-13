using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtItem : MonoBehaviour, ItemInterface
{
	public bool status;
	public GameObject texture;
	private GameObject _lookAtItem;
	private PlayerInventory inventory;
	public void ChangeItem()
	{
		status = false;
		texture.SetActive(false);
	}

	public bool GetStatus()
	{
		return status;
	}

	public void Iterction()
	{
		if(_lookAtItem == null)
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			inventory = player.GetComponent<PlayerInventory>();
			_lookAtItem = player.transform.Find("LookAtItem").gameObject;
		}
		
	}

	public void PickedItem()
	{
		status = true;
		texture.SetActive(true);
	}
}
