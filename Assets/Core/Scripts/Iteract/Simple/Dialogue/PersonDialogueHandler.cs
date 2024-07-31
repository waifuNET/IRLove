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
    public void GetFiles()
    {
        _dialogueInit.InitializeDialogueData(files[fileIndex]);
    }
}
