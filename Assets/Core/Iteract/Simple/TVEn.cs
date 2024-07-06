using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TVEn : MonoBehaviour, Iteraction
{
	public GameObject TV;
	public VideoPlayer player;
	public bool enable = true;
	public void Iterction()
	{
		if (enable)
		{
			player.Pause();
			TV.SetActive(false);
		}
		else
		{
			player.Play();
			TV.SetActive(true);
		}
		enable = !enable;
	}

	public void SetActive(bool status)
	{
		GetComponent<IRLButton_View>().active = status;
	}
}
