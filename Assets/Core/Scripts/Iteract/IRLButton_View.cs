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

	private void Start()
	{
		outline = GetComponent<Outline>();
	}

	public void Select(bool select)
	{
		if (select && active)
		{
			outline.OutlineWidth = 10;
			//SmoothOutlineWidth(0, 10);
		}
		else
		{
			outline.OutlineWidth = 0;
			//StartCoroutine(SmoothOutlineWidth(10, 0));
		}
	}
	private IEnumerator SmoothOutlineWidth(float start, float finish)
	{
		float smooth_base = 0.1f;
		float smooth = start > finish ? smooth_base * -1 : smooth_base;
		float _now_value = start;
		Debug.Log("smooth: " + smooth);
		Debug.Log("smooth: " + (start != finish));
		Debug.Log("start: " + start + " finish: " + finish);
		while (start != finish)
		{
			if(_now_value + smooth * 2 > finish || _now_value <= 0)
			{
				break;
			}
			_now_value += smooth;
			outline.OutlineWidth = _now_value;
			Debug.Log(_now_value);
			yield return new WaitForSeconds(0.1f);
		}
		Debug.Log("finish: " + _now_value);
	}
}
