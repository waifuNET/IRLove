using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class CloseServerDoor : MonoBehaviour, Iteraction
{
    public Animator animator;
    public bool isOpened = false;
	private bool active = true;
	public void Iterction()
	{
        if (isOpened)
        {
			animator.SetFloat("AnimSpeed", -1f);
			animator.SetTrigger("DoorInt");
			isOpened = false;

		}
		else if (!isOpened)
		{
			animator.SetFloat("AnimSpeed", 1f);
			animator.SetTrigger("DoorInt");
			isOpened = true;
		}
    }
	public void SetActive(bool status)
	{
		active = status;
		GetComponent<IRLButton_View>().active = status;
	}
}
