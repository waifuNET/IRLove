using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseServerDoor : MonoBehaviour, Iteraction
{
    public Animator animator;
    private bool isOpened = true;
	private bool active = true;
	public void Iterction()
	{
        if (isOpened)
        {
			animator.SetTrigger("CloseDoor");
			isOpened = false;
		}
		else if (!isOpened)
		{
			animator.SetTrigger("OpenDoor");
            isOpened = true;

        }
    }
	public void SetActive(bool status)
	{
		active = status;
		GetComponent<IRLButton_View>().active = status;
	}
}
