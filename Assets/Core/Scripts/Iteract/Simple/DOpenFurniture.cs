using DG.Tweening;
using UnityEngine;

public class DOpenFurniture : MonoBehaviour, Iteraction
{
	public bool status = false;
	public bool autoStartPosition = true;
	public Vector3 startPosition;
	public Vector3 needPosition;
	private Vector3 _needPosition;
	private void Start()
	{
		_needPosition = needPosition;
		if (autoStartPosition)
			startPosition = transform.position;
	}
	public void Iterction()
	{
		needPosition = startPosition + _needPosition;
		status = !status;
		if (status)
			transform.DOMove(needPosition, 1);
		else
			transform.DOMove(startPosition, 1);
	}

	public void SetActive(bool status)
	{

	}
}
