using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightIteract : MonoBehaviour, ItemInterface
{
	public bool status = false;
	public GameObject flashLightTexture;
	public GameObject root;

	public bool canUse = true;
	public Light flashLight;

	public float maxIntensity = 1.5f;

	public void ChangeItem()
	{
		status = false;
		flashLightTexture.SetActive(false);
		TurnStatus(false);
	}
	public void PickedItem()
	{
		flashLightTexture.SetActive(true);
	}

	public void Iterction()
	{
		if (!canUse) return;
		status = !status;
		TurnStatus(status);
	}

	public void TurnStatus(bool status)
	{
		root.GetComponent<Light>().enabled = status;
	}
	public bool GetStatus()
	{
		return status;
	}
}
