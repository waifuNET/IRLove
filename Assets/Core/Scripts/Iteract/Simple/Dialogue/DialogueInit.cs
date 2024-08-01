using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using TMPro.EditorUtilities;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject ESCMenu;

    int dialogueLineNum = 0;

    public bool dialogueIsActive = false;

    List<DialogueElement> elements = new List<DialogueElement>();

    private void Start()
    {
        //NextLine();
        PlayerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FirstPersonLook>();
        PlayerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonMovement>();
    }

    private void Update()
    {
        isActive();
        if (dialoguePanel.isActiveAndEnabled)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                NextLine();
            }
        }
        if(ESCMenu.activeSelf)
        {
            dialoguePanel.gameObject.SetActive(false);
        }
        else if(dialogueIsActive)
        {
            dialoguePanel.gameObject.SetActive(true);
        }
    }

    public List<string> InitializeDialogueData(string file_name)
    {
        string path = Path.Combine(Application.streamingAssetsPath, file_name);
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

        for(int i = 0; i<local.Count;i++)
        {
            if(local[i].Split('#')[1].Contains(":"))
            {
                DialogueElement dialogueElement = new DialogueElement();
                dialogueElement.SetName(local[i].Split("#")[1].Split(":")[0].Trim());
                dialogueElement.SetText(local[i].Split("#")[1].Split(":")[1].Trim());
                dialogueElements.Add(dialogueElement);
            }
        }
        elements = dialogueElements;
    }
    public void isActive()
    {
        if(dialogueIsActive)
        {
            PlayerCamera.LockCamera();
            PlayerMovement.LockMovement();
        }
        else 
        {
            PlayerCamera.UnLockCamera();
            PlayerMovement.UnLockMovement();
        }
    }

    public void NextLine()
    {
        dialogueIsActive = true;
        if (elements.Count == dialogueLineNum)
        {
            dialogueIsActive = false;
            dialoguePanel.gameObject.SetActive(false);
            
        }
        else
        {
            Name.text = elements[dialogueLineNum].GetName();
            Text.text = elements[dialogueLineNum].GetText();
            dialogueLineNum++;
        }
    }
}
