using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ButtonNumber : MonoBehaviour
{
    public int buttonNumber;
    DialogueInit _dialogueInit;
    bool isClicked = false;

    public void Start()
    {
        _dialogueInit = GameObject.FindWithTag("DialogueHandler").GetComponent<DialogueInit>();
    }
    public void Update()
    {
        if(gameObject.activeSelf)
        {
            _dialogueInit.PDH.files = _dialogueInit.choicesFileName;
        }
    }

    public void OnClick()
    {
        _dialogueInit.Decode(_dialogueInit.PDH.choicesFileLines[buttonNumber]);
        Debug.Log(_dialogueInit.PDH.choicesFileLines[buttonNumber]);
        _dialogueInit.dialoguePanel.transform.Find("Name Panel").gameObject.SetActive(true);
        _dialogueInit.dialoguePanel.transform.Find("Dialogue Panel").gameObject.SetActive(true);
        _dialogueInit.PDH.StartDialogue();
        _dialogueInit.NextLine();
        //Need to kill all child
    }
    

    public bool IsClicked()
    {
        return isClicked;
    }
}
