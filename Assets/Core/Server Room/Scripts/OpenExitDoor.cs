using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OpenExitDoor : MonoBehaviour, Iteraction
{
	public Material material;
	public GameObject door;
	public void Iterction()
	{
		door.SetActive(true);
		SetActive(false);
		StartCoroutine("ChangeColor");
	}

	private void Start()
	{
		material.SetColor("_EmissionColor", new Color(255, 255, 255) * Mathf.Pow(2, -20));
	}

	public void SetActive(bool status)
	{
		GetComponent<IRLButton_View>().active = status;
	}

	public IEnumerator ChangeColor()
	{
		for(float i = -20; i < -6; i += 0.1f)
		{
			material.SetColor("_EmissionColor", new Color(255, 255, 255) * Mathf.Pow(2, i));
			yield return new WaitForSeconds(0.01f);
		}
	}
}
