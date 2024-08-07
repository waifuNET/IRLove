using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIteract : MonoBehaviour
{
	public Transform PlayerCamera;
	public Image PlayerCrosshair;
	public Sprite defaultCrosshair;
	public Sprite iteractCrosshair;

	public LayerMask layerMask;

	public PersonDialogueHandler PDI;
	public Canvas dialoguePanel;

    public GameObject IterctObj;

	private float closestDistance = GameValues.PlayerInteractLenght;

    public FirstPersonLook PlayerCameraComp;
    public FirstPersonMovement PlayerMovement;

	public GameObject _dialogInit;
    private void Start()
    {
        //NextLine();
        PlayerCameraComp = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FirstPersonLook>();
        PlayerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonMovement>();
    }

    public void Update()
	{
		RaycastHit hit;
		if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.TransformDirection(Vector3.forward), out hit, closestDistance, layerMask))
		{
			Debug.DrawRay(PlayerCamera.transform.position, PlayerCamera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
			if (hit.transform.gameObject.tag == "Iteract")
			{
				float distance = Vector3.Distance(transform.position, hit.transform.transform.position);

				if (distance < closestDistance)
				{
					IterctObj = hit.transform.gameObject;
					PlayerCrosshair.sprite = iteractCrosshair;
				}
			}
			else if(hit.transform.gameObject.tag == "Dialogue")
			{
				IterctObj = hit.transform.gameObject;
            }
        }
		else
		{
			Debug.DrawRay(PlayerCamera.transform.position, PlayerCamera.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
			PlayerCrosshair.sprite = defaultCrosshair;

			IterctObj = null;
		}


		if (IterctObj && Input.GetKeyDown(KeyCode.E) && IterctObj.GetComponent<Iteraction>() != null)
		{
			IterctObj.GetComponent<Iteraction>().Iterction();
		}

		if(IterctObj && Input.GetKeyDown(KeyCode.E) && IterctObj.GetComponent<PersonDialogueHandler>() != null)
		{
            dialoguePanel.gameObject.SetActive(true);
            IterctObj.GetComponent<PersonDialogueHandler>().GetFiles();
			//_dialogInit.GetComponent<DialogueInit>().NextLine();
			
        }
	}
}
