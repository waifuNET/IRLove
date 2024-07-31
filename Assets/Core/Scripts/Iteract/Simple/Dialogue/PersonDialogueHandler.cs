using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Windows;


public class PersonDialogueHandler : MonoBehaviour
{
    public string[] files;
    int fileIndex = 0;
    public DialogueInit _dialogueInit;

    private void Start()
    {
        _dialogueInit = GameObject.FindGameObjectWithTag("DialogueHandler").GetComponent<DialogueInit>();
    }
    public void GetFiles()
    {
        List<string> line = _dialogueInit.InitializeDialogueData("Dialogue/DialogueData/test.dlg");
        _dialogueInit.Decode(line);
    }
}
