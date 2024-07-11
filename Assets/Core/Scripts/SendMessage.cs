using System.Collections;
using TMPro;
using UnityEngine;

public class SendMessage : MonoBehaviour
{
	public TextMeshProUGUI textMeshProUGUI;
	private IEnumerator IEstartVariable;

	public void SendPlayerMessage(string text, Color color, float delay = 0.1f, bool bold = false, float clearTime = 3)
	{
		if (textMeshProUGUI == null)
			textMeshProUGUI = GameObject.FindGameObjectWithTag("PlayerMessageUI").GetComponent<TextMeshProUGUI>();

		if (IEstartVariable != null)
			StopCoroutine(IEstartVariable);

		IEstartVariable = IEstart(text, color, delay, bold, clearTime);
		StartCoroutine(IEstartVariable);
	}

	private IEnumerator IEstart(string text, Color color, float delay, bool bold, float clearTime)
	{
		textMeshProUGUI.text = "";
		for (int i = 0; i < text.Length; i++)
		{
			textMeshProUGUI.text += text[i];
			if (text[i] == ' ')
				continue;

			yield return new WaitForSeconds(delay);
		}

		yield return new WaitForSeconds(clearTime);
		textMeshProUGUI.text = " ";

		// Принудительное обновление состояния
		textMeshProUGUI.ForceMeshUpdate();
		Canvas.ForceUpdateCanvases();

		// Отключение и включение компонента
		textMeshProUGUI.enabled = false;
		yield return null;
		textMeshProUGUI.enabled = true;
	}
}
