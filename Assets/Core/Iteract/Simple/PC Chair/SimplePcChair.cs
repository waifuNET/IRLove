using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePcChair : MonoBehaviour, Iteraction
{
	public FirstPersonMovement playerMovement;
	public Jump playerJump;
	public bool playerSit = false;
	public float Delay = 3;
	private float time;

	public Vector3 CPos1, CPos2;

	private Vector3 lastPos;
	public Transform needPos;

	void Start()
	{

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
			transform.DOMove(CPos2, 1);
			playerMovement.transform.DOMove(needPos.position, 1);
		}
		else
		{
			transform.DOMove(CPos1, 1);
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
		SetActive(!status);
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

	public void SetActive(bool status)
	{
		GetComponent<IRLButton_View>().active = status;
	}
}
