using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseServerDoor : MonoBehaviour, Iteraction
{
    public Animator animator;
    private bool active = true;
	public void Iterction()
	{
        if (active)
        {
			SetActive(false);
			animator.SetTrigger("CloseDoor");
		}
	}
	public void SetActive(bool status)
	{
		active = status;
		GetComponent<IRLButton_View>().active = status;
	}
}
