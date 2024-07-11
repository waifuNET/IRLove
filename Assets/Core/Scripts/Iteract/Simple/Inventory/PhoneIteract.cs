using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneIteract : MonoBehaviour, ItemInterface
{
	public bool status = false;
	public bool canUse = true;
	public GameObject texture;
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

	}

	public void PickedItem()
	{
		texture.SetActive(true);
	}

	void Start()
    {
        
    }


}
