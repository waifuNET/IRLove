using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChoice
{
    public string choiceText;
    public DialogueNode nextNode;
}
public class DialogueNode
{
    public string speaker;
    public string dialogueText;
}
[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    public DialogueNode startNode;
}
