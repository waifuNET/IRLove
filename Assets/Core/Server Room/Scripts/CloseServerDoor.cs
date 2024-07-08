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
			Action(-1f, false);

        }
		else if (!isOpened)
		{
			Action(1f, true);
        }
    }
	public void Action(float f,bool b)
	{
		animator.SetFloat("AnimSpeed", f);
        animator.SetTrigger("DoorInt");
        isOpened = b;
    }
    public void SetActive(bool status)
	{
		active = status;
		GetComponent<IRLButton_View>().active = status;
	}
}
