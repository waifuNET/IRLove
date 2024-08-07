using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
/* TO DO: 1.We have to fix Decoding every time when pressing E key, added new bool param to
 *          detect dialogue beginnig. 
 *        2.Have to change system of detecting activating buttonchoces. 
 *        3.Making visual bug fixed activating and disabling text and name menu.
 */

public class DialogueInit : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Text;

    public FirstPersonLook PlayerCamera;
    public FirstPersonMovement PlayerMovement;
    public MainMenu mm;
    public ButtonNumber bn;

    public Canvas dialoguePanel;

    public GameObject choicePanel;
    public GameObject ESCMenu;
    public GameObject choiceButtonPrefab;

    int dialogueLineNum = 0;
    public int choicesLine;

    bool isCreate = false;
    public bool dialogueIsActive = false;

    public List<string> choices = new List<string>();
    public List<string> choicesFileName = new List<string>();

    public List<DialogueElement> dialogueElements = new List<DialogueElement>();
    public List<DialogueElement> elements = new List<DialogueElement>();

    private void Start()
    {
        PlayerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FirstPersonLook>();
        PlayerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonMovement>();
    }

    private void Update()
    {
        if (dialogueIsActive)
        {
            BlockMenu();
        }
        else
        {
            UnblockMenu();
        }
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

    public void BlockMenu()
    {
        mm.canOpenMenu = false;
    }

    public void UnblockMenu()
    {
        mm.canOpenMenu = true;
    }

    public void Decode(List<string> local)
    {

        for (int i = 0; i<local.Count;i++)
        {
            if (local[i].Contains('#'))
            {
                if (local[i].Split('#')[1].Contains(":"))
                {
                    DialogueElement dialogueElement = new DialogueElement();
                    dialogueElement.SetName(local[i].Split("#")[1].Split(":")[0].Trim());
                    dialogueElement.SetText(local[i].Split("#")[1].Split(":")[1].Trim());
                    dialogueElements.Add(dialogueElement);
                }
            }
            else if(local[i].Contains('?'))
            {
                if (local[i].Split('?')[1].Contains(':'))
                {
                    choices = local[i].Split('?')[1].Split(':').ToList();
                    choicesLine = i;
                    dialogueElements.Add(new DialogueElement());
				}
                if(local[i].Split('?')[1].Contains('>'))
                {
                    choicesFileName = local[i].Split('?')[1].Split('>').ToList();
                }
            }
        }
        elements = dialogueElements;
        for(int i = 0 ; elements.Count > i ; i++)
        {
            Debug.Log(elements[i].GetName());
            Debug.Log(elements[i].GetText());
        }
    }
    public void CreateChoice()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
		for (int i = 0; i < choices.Count; i++)
        {
            GameObject go = Instantiate(choiceButtonPrefab, choicePanel.transform, false);
            bn = go.GetComponent<ButtonNumber>();
            go.transform.Find("text").GetComponent<TextMeshProUGUI>().text = choices[i].ToString();
        }
    }  

    public void isActive()
    {
        if(dialogueIsActive)
        {
            PlayerCamera.LockCamera();
            PlayerMovement.LockMovement();
        }
        if(!dialogueIsActive)
        {
            PlayerCamera.UnLockCamera();
            PlayerMovement.UnLockMovement();
        }
    }

    public void NextLine()
    {
        dialogueIsActive = true;
        isActive();
        if (dialogueLineNum == choicesLine)
        {
            CreateChoice();
        }
        if (elements.Count == dialogueLineNum)
        {
            dialogueIsActive = false;
            isActive();
            dialoguePanel.enabled = false;
			Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Name.text = elements[dialogueLineNum].GetName();
            Text.text = elements[dialogueLineNum].GetText();
            dialogueLineNum++;
        }
    }
}
