using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using System.Runtime.CompilerServices;

public class IRLButton_View : MonoBehaviour
{
	public bool active = true;
	//private IEnumerator smothIE;
	private void Start()
	{
		//outline = GetComponent<Outline>();
	}

	public void Select(bool select)
	{
		//if (active)
		//{
		//	if(smothIE != null) StopCoroutine(smothIE);
		//	smothIE = SmoothOutlineWidth(select ? 10 : 0);
		//	StartCoroutine(smothIE);
		//}
	}
	//private IEnumerator SmoothOutlineWidth(float need)
	//{
	//	float smooth_base = 0.5f;
	//	float smooth = outline.OutlineWidth > need ? smooth_base * -1 : smooth_base;
	//	float _now_value = outline.OutlineWidth;
	//	while (outline.OutlineWidth != need)
	//	{
	//		if(outline.OutlineWidth > 10 || _now_value < 0)
	//		{
	//			outline.OutlineWidth = need;
	//			break;
	//		}
	//		_now_value += smooth;
	//		outline.OutlineWidth = _now_value;
	//		yield return new WaitForSeconds(0.01f);
	//	}
	//}
}
