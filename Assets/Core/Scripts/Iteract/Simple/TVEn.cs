using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TVEn : MonoBehaviour, Iteraction
{
	public GameObject TV;
	public GameObject TV_OFF_SCREEN;
	public VideoPlayer player;
	public bool enable = true;
	public void Iterction()
	{
		if (enable)
		{
			player.Pause();
			TV.SetActive(false);
			TV_OFF_SCREEN.SetActive(true);
		}
		else
		{
			player.Play();
			TV.SetActive(true);
			TV_OFF_SCREEN.SetActive(false);
		}
		enable = !enable;
	}
}
