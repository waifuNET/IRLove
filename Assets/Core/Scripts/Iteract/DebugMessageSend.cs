using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMessageSend : MonoBehaviour, Iteraction
{
	private SendMessage sendMessage;
	private void Start()
	{
		sendMessage = gameObject.GetComponent<SendMessage>();
	}
	public void Iterction()
	{
		sendMessage.SendPlayerMessage("VZLOM ASS", Color.white);
	}

	public void SetActive(bool status)
	{
		throw new System.NotImplementedException();
	}
}
