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

	public Quaternion startRotation;
    public Vector3 needRotation;
	public bool isBusy=false;



    private void Start()
	{
        startRotation = transform.rotation;
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
            isBusy = true;
            transform.DORotate(needRotation,1,RotateMode.WorldAxisAdd);
        }
			
		else
		{
            transform.DOMove(startPosition, 1);
            isBusy = true;
            transform.DORotate(-needRotation, 1, RotateMode.WorldAxisAdd);
        }

        permOpen = false;
		isBusy = false;
	}

	public void SetActive(bool status)
	{

	}
}
