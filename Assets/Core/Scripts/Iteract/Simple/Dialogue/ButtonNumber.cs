using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ButtonNumber : MonoBehaviour
{
    public int buttonNumber;
    DialogueInit _dialogueInit;
    public string fileName;
    bool isClicked = false;

    public void Start()
    {
        _dialogueInit = GameObject.FindWithTag("DialogueHandler").GetComponent<DialogueInit>();
    }

    public void OnClick()
    {
        //vot tut bag. tut full list peredaet so vsemi slovami v knopku
        List<DialogueElement> temp = _dialogueInit.Decode(_dialogueInit.PDH.choicesFileLines[buttonNumber]);
        Debug.Log(temp.Count); 
        foreach (DialogueElement element in temp)
        {
            Debug.Log(element.GetName());
            Debug.Log(element.GetText());
            //tut bag ya hz pochemy tol'ko vtoroi element chitaet...
        }
        _dialogueInit.dialoguePanel.transform.Find("Name Panel").gameObject.SetActive(true);
        _dialogueInit.dialoguePanel.transform.Find("Dialogue Panel").gameObject.SetActive(true);
        _dialogueInit.PDH.StartDialogue();
        _dialogueInit.NextLine();
        _dialogueInit.SetChoice(false);
        while (_dialogueInit.choicePanel.transform.childCount > 0)
        {
            DestroyImmediate(_dialogueInit.choicePanel.transform.GetChild(0).gameObject);
        }
    }
    

    public bool IsClicked()
    {
        return isClicked;
    }
}
