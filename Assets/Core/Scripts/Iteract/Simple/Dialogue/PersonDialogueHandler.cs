using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Windows;


public class PersonDialogueHandler : MonoBehaviour
{
    public List<string> files;
    public List<string> choicesFilesName = new List<string>();

    int localIndex = 0;

    public DialogueInit _dialogueInit;

    public List<string> lines = new List<string>();
    public List<DialogueElement> decodedDialogue = new List<DialogueElement>();

    public bool dialogueStarted = false;
    private void Start()
    {
        _dialogueInit = GameObject.FindGameObjectWithTag("DialogueHandler").GetComponent<DialogueInit>();
    }
    public List<string> GetFiles(List<string> local)
    {
        if(localIndex> local.Count - 1)
        {
            localIndex = 0;
        }
        List<string> lines = _dialogueInit.InitializeDialogueData("Dialogue/DialogueData/" + local[localIndex] + ".dlg");
        localIndex++;
        return lines;
    }
    public List<string> GetFiles(string local)
    {
        List<string> lines = _dialogueInit.InitializeDialogueData("Dialogue/DialogueData/" + local + ".dlg");
        return lines;
    }
    public void StartDialogue()
    {
        _dialogueInit.dialoguePanel.gameObject.SetActive(true);
        _dialogueInit.PDH = gameObject.GetComponent<PersonDialogueHandler>();
        dialogueStarted = true;
    }
    public bool IsActive()
    {
        return dialogueStarted;
    }

    public void DeActive()
    {
        dialogueStarted = false;
    }
}
