using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ButtonNumber : MonoBehaviour
{
    public int buttonNumber;
    DialogueInit di;
    PersonDialogueHandler PDH;
    bool isClicked = false;

    public void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
        di = GameObject.FindWithTag("DialogueHandler").GetComponent<DialogueInit>();
        PDH = GetComponent<PersonDialogueHandler>();
    }
    public void Update()
    {
        if(gameObject.activeSelf)
        {
            PDH.files = di.choicesFileName.ToArray();
        }
    }

    void OnClick()
    {
        PDH.GetFiles();
    }

    public bool IsClicked()
    {
        return isClicked;
    }
}
