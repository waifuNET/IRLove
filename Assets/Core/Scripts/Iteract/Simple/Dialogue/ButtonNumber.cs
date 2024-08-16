using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ButtonNumber : MonoBehaviour
{
    public int buttonNumber;

    DialogueInit _dialogueInit;
    PersonDialogueHandler PDH;

    bool isClicked = false;

    public List<string> file = new List<string>();
    public List<DialogueElement> lines = new List<DialogueElement>();

    public void Start()
    {
        _dialogueInit = GameObject.FindWithTag("DialogueHandler").GetComponent<DialogueInit>();
        PDH = _dialogueInit.selfGameObject.GetComponent<PersonDialogueHandler>();
        file = PDH.GetFiles(PDH.choicesFilesName[buttonNumber]);
    }

    public void OnClick()
    {
        _dialogueInit.Decode(file);
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
