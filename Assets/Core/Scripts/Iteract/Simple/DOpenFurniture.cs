using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class DOpenFurniture : MonoBehaviour, Iteraction
{
	public bool status = false;
	public bool permOpen = false;
	public bool autoStartPosition = true;


    public Vector3 startPosition;
	public Vector3 needPosition;
	private Vector3 _needPosition;

	public Vector3 startRotation;
    public Vector3 needRotation;
	public Vector3 tempRotationToStart;
	public Vector3 tempRotationToEnd;



    private void Start()
	{
        _needPosition = needPosition;
		if (autoStartPosition)
			startPosition = transform.position;
	}
	
    public void Iterction()
	{
		needPosition = startPosition + _needPosition;
		if (!permOpen)
			status = !status;
		if (status)
		{
			transform.DOMove(needPosition, 1);
			transform.DORotate(needRotation, 1);
        }
			
		else
		{
            transform.DOMove(startPosition, 1);
			transform.DORotate(startRotation, 1);
			

        }

        permOpen = false;
	}

	public void SetActive(bool status)
	{

	}
}
