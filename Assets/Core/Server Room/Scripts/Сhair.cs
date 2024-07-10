using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Сhair : MonoBehaviour, Iteraction
{
	public FirstPersonMovement playerMovement;
	public Jump playerJump;
	public bool playerSit = false;
	public float Delay = 3;
	private float time;

	private Vector3 lastPos;
	public Transform needPos;

	void Start()
	{
		playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonMovement>();
		playerJump = GameObject.FindGameObjectWithTag("Player").GetComponent<Jump>();
	}

	void Update()
	{
		if ((Input.GetKeyDown(KeyCode.C)) && playerSit)
		{
			PlayerSitStatus(false);
		}

		time -= Time.deltaTime;
	}

	private void Sit(bool status)
	{
		if (status)
		{
			playerMovement.transform.DOMove(needPos.position, 1);
		}
		else
		{
			playerMovement.transform.DOMove(lastPos, 1);
		}
		time = Delay;
	}

	public void PlayerSitStatus(bool status)
	{
		playerSit = status;
		playerMovement.canWalk = !status;
		playerJump.canJump = !status;
		playerMovement.gameObject.GetComponent<Crouch>().canCrouch = !status;
		playerMovement.gameObject.GetComponent<Rigidbody>().isKinematic = status;
		Sit(status);

	}

	public void Iterction()
	{
		if (time <= 0 && !playerSit)
		{
			lastPos = playerMovement.transform.position;

			PlayerSitStatus(true);
		}
	}
}
