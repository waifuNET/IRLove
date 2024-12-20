using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public bool isMainMenu = false;
	public GameObject mainMenu;
	public GameObject gameMenu;
	public List<GameObject> hideCanvas;
	public FirstPersonLook playerCamera;
	public bool canOpenMenu;
	public List<Image> MENU_COLORS = new List<Image>();
	[HideInInspector] public List<GameObject> menuObjects = new List<GameObject>();

	private void Start()
	{
		if (playerCamera == null) playerCamera = GameObject.FindGameObjectWithTag("MainCamera")?.GetComponent<FirstPersonLook>();
	}
	public void Resume()
	{
		Time.timeScale = 1.0f;
		GlobalBack();
	}

	public void MainContinue()
	{

	}

	public void MainNewGame()
	{
		Debug.Log("Load: " + 1);
		SceneManager.LoadScene(1);
	}

	public void MainQuit()
	{
		Environment.Exit(0);
	}

	public void SetMenuColor()
	{
		float normalizedValue = Mathf.InverseLerp(100, -100, GameValues.Sainty);
		Color newColor = Color.Lerp(Color.white, Color.red, normalizedValue);
		for (int i = 0; i < MENU_COLORS.Count; i++)
		{
			if (MENU_COLORS[i] != null)
			{
				MENU_COLORS[i].color = newColor;
			}
		}
	}

	public void Update()
	{
		if (isMainMenu) return;

		if (Input.GetKeyDown(KeyCode.Escape)&&canOpenMenu)
		{
			if(menuObjects.Count == 0)
			{
				SetMenuColor();
				/* First method trying to realize this, but i'll try to realize in Dialogue init script
				for(int i = 0; hideCanvas.Count>i;i++)
				{
					hideCanvas[i].SetActive(false);
				}
				*/
				gameMenu.SetActive(true);
				menuObjects.Add(gameMenu);
				Time.timeScale = 0f;
				playerCamera.LockCamera();
				Cursor.lockState = CursorLockMode.Confined;
			}
			else
			{
				GlobalBack();
				/* continue^
                for (int i = 0; hideCanvas.Count > i; i++)
                {
                    hideCanvas[i].SetActive(true);
                }
				*/
            }
		}
	}

	private void GlobalBack()
	{
		if (menuObjects[menuObjects.Count - 1] != null)
		{
			menuObjects[menuObjects.Count - 1].SetActive(false);
			menuObjects.Remove(menuObjects[menuObjects.Count - 1]);
		}
		if (menuObjects.Count == 0)
		{
			Time.timeScale = 1f;
			playerCamera.UnLockCamera();
			Cursor.lockState = CursorLockMode.Locked;
		}
	}
}
