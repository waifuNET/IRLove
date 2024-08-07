using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        if(fileIndex>files.Length-1)
        {
            fileIndex = 0;
        }
        List<string> line = _dialogueInit.InitializeDialogueData("Dialogue/DialogueData/" + files[fileIndex] +".dlg");
        fileIndex++;
        _dialogueInit.Decode(line);
        Debug.Log(line);
    }
}
