using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SendMessage : MonoBehaviour
{
	public TextMeshProUGUI textMeshProUGUI;
	private IEnumerator IEstartVariable;
	public void SendPlayerMessage(string text, Color color, float delay = 0.1f, bool bold = false, float clearTime = 5)
    {
		if(IEstartVariable != null) StopCoroutine(IEstartVariable);
		IEstartVariable = IEstart(text, color, delay, bold, clearTime);
		StartCoroutine(IEstartVariable);
    }

	private IEnumerator IEstart(string text, Color color, float delay, bool bold, float clearTime)
	{
		textMeshProUGUI.text = "";
		for (int i = 0; i < text.Length; i++)
		{
			textMeshProUGUI.text += text[i];
			if (text[i] == ' ') continue;
			yield return new WaitForSeconds(delay);
		}

		yield return new WaitForSeconds(clearTime);
		textMeshProUGUI.SetText(" ");
	}
}
