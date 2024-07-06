using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class IRLButton_View : MonoBehaviour
{
	public Outline outline;
	public bool active = true;

	private void Start()
	{
		outline = GetComponent<Outline>();
	}

	public void Select(bool select)
	{
		if (select && active)
		{
			outline.OutlineWidth = 10;
		}
		else
		{
			outline.OutlineWidth = 0;
		}
	}
}
