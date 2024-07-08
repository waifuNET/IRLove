using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using System.Runtime.CompilerServices;

[RequireComponent(typeof(Outline))]
public class IRLButton_View : MonoBehaviour
{
	public Outline outline;
	public bool active = true;
	private IEnumerator smothIE;
	private void Start()
	{
		outline = GetComponent<Outline>();
	}

	public void Select(bool select)
	{
		if (select && active)
		{
			//outline.OutlineWidth = 10;
			if(smothIE != null) StopCoroutine(smothIE);
			smothIE = SmoothOutlineWidth(10);
			StartCoroutine(smothIE);
		}
		else
		{
			//outline.OutlineWidth = 0;
			if (smothIE != null) StopCoroutine(smothIE);
			smothIE = SmoothOutlineWidth(0);
			StartCoroutine(smothIE);
		}
	}
	private IEnumerator SmoothOutlineWidth(float need)
	{
		float smooth_base = 0.1f;
		float smooth = outline.OutlineWidth > need ? smooth_base * -1 : smooth_base;
		float _now_value = outline.OutlineWidth;
		while (outline.OutlineWidth != need)
		{
			if((_now_value + (smooth * 2)) > need || _now_value < 0)
			{
				break;
			}
			_now_value += smooth;
			outline.OutlineWidth = _now_value;
			Debug.Log("_now_value: " + _now_value);
			yield return new WaitForSeconds(0.01f);
		}
	}
}
