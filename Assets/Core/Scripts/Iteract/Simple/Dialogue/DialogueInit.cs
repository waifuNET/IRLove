using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueElement
{
    private string name;
    private string text;

    public DialogueElement(string name, string text)
    {
        this.name = name;
        this.text = text;
    }
    public DialogueElement() { }

    public void SetName(string name) { this.name = name; }
    public void SetText(string text) { this.text = text; }
    public string GetName() { return name;}
    public string GetText() { return text;}
}

public class DialogueInit : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Text;

    public FirstPersonLook PlayerCamera;
    public FirstPersonMovement PlayerMovement;

    public Canvas dialoguePanel;

    int dialogueLineNum = 0;

    List<DialogueElement> elements = new List<DialogueElement>();

    private void Start()
    {
        //NextLine();
        PlayerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FirstPersonLook>();
        PlayerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonMovement>();
    }

    private void Update()
    {
        if(dialoguePanel.isActiveAndEnabled)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                NextLine();
                Debug.Log(elements.Count);
            }
        }
    }

    public List<string> InitializeDialogueData(string file_name)
    {
        string path = Path.Combine(Application.streamingAssetsPath, file_name);
        Debug.Log(path);
        List<string> lines = new List<string>();
        using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
        {
            string line = string.Empty;
            while((line = sr.ReadLine()) != null)
            {
                line = line.Trim();
                if(line!="")
                {
                    lines.Add(line);
                }
            }
        }
        return lines;
    }

    public void Decode(List<string> local)
    {
        List<DialogueElement> dialogueElements = new List<DialogueElement>();
        DialogueElement dialogueElement = new DialogueElement();

        for(int i = 0; i<local.Count;i++)
        {
            if(local[i].Split('#')[1].Contains(":"))
            {
                dialogueElement.SetName(local[i].Split("#")[1].Split(":")[0].Trim());
                dialogueElement.SetText(local[i].Split("#")[1].Split(":")[1].Trim());
                Debug.Log(dialogueElement.GetName());
                Debug.Log(dialogueElement.GetText());
                dialogueElements.Add(dialogueElement);
            }
        }
        elements = dialogueElements;
    }

    public void NextLine()
    {
        Debug.Log(dialogueLineNum);
        Name.text = elements[dialogueLineNum].GetName();
        Text.text = elements[dialogueLineNum].GetText();
        if(elements.Count>= dialogueLineNum)
        {
            dialoguePanel.gameObject.SetActive(false);
            PlayerCamera.UnLockCamera();
            PlayerMovement.UnLockMovement();
        }
        else
        {
            dialogueLineNum++;
        }
    }
}
