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
/* TO DO: ✓ 1.We have to fix Decoding every time when pressing E key, added new bool param to
 *          detect dialogue beginnig. ✓
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

    public int dialogueLineNum = 0;
    public int choicesLine;

    public bool dialogueIsActive = false;
    public bool choiceIsActive = false;

    public PersonDialogueHandler PDH;

    public List<PersonDialogueHandler> PDHScan = new List<PersonDialogueHandler>();

    public List<string> choices = new List<string>();
    public List<string> choicesFileName = new List<string>();

    public List<DialogueElement> dialogueElements = new List<DialogueElement>();
    public List<DialogueElement> elements = new List<DialogueElement>();

    private void Start()
    {
        PlayerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FirstPersonLook>();
        PlayerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonMovement>();
        PDHScan = FindObjectsByType<PersonDialogueHandler>(sortMode: FindObjectsSortMode.InstanceID).ToList();
        for(int i = 0; i < PDHScan.Count; i++)
        {
            /*  Need to refract this part and decode with getting gameObject param to track choices files inside
             *  TO DO: 1. Make Decoding in iteract to give opportunity track choice and loop dialogue without choice
             *         2. Do tracking param to attract with buttons and decoding file inside theirs code
             */
            PDHScan[i].decodedDialogue = Decode(PDHScan[i].GetFiles(PDHScan[i].files));
            if(choicesFileName != null)
            {
                PDHScan[i].choicesFilesName = choicesFileName;
            }
        }
    }

    private void Update()
    {
        if(PDH!=null)
        {
            if (PDH.IsActive())
            {
                BlockMenu();
            }
            else
            {
                UnblockMenu();
            }
        }
        
        if (dialoguePanel.isActiveAndEnabled)
        {
            if (!ChoiceIsActive())
            {
                SkipDialogue();
            }
        }
    }
    public void SkipDialogue()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            NextLine();
        }
    }
    
    public void BlockMenu()
    {
        mm.canOpenMenu = false;
    }
    public void UnblockMenu()
    {
        mm.canOpenMenu = true;
    }

    public List<string> InitializeDialogueData(string file_name)
    {
        string path = Path.Combine(Application.streamingAssetsPath, file_name);
        List<string> lines = new List<string>();

        using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
        {
            string line = string.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                line = line.Trim();
                if (line != "")
                {
                    lines.Add(line);
                }
            }
        }
        return lines;
    }
    public List<DialogueElement> Decode(List<string> local)
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
                else if(local[i].Split('?')[1].Contains('>'))
                {
                    for(int j = 0;j < local[i].Split('?')[1].Split('>').ToList().Count; j++)
                    {
                        choicesFileName.Add(local[i].Split('?')[1].Split('>').ToList()[j]);
                    }
                }
            }
        }
        return elements = dialogueElements;
    }
    
    public void CreateChoice()
    {
        SetChoice(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
		for (int i = 0; i < choices.Count; i++)
        {
            GameObject go = Instantiate(choiceButtonPrefab, choicePanel.transform, false);
            bn = go.GetComponent<ButtonNumber>();
            bn.buttonNumber = i;
            go.transform.Find("text").GetComponent<TextMeshProUGUI>().text = choices[i].ToString();
        }
    }  
    public bool ChoiceIsActive()
    {
        return choiceIsActive;
    }
    public void SetChoice(bool local)
    {
        choiceIsActive = local;
    }
    public void isActive()
    {
        if(PDH.IsActive())
        {
            PlayerCamera.LockCamera();
            PlayerMovement.LockMovement();
        }
        if(!PDH.IsActive())
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
            
            dialoguePanel.transform.Find("Name Panel").gameObject.SetActive(false);
            dialoguePanel.transform.Find("Dialogue Panel").gameObject.SetActive(false);
        }
        if (PDH.decodedDialogue.Count == dialogueLineNum)
        {
            PDH.DeActive();
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
