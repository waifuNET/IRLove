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
			transform.DORotate(needRotation,1,RotateMode.LocalAxisAdd);
        }
			
		else
		{
            transform.DOMove(startPosition, 1);
			transform.DORotate(-needRotation, 1, RotateMode.LocalAxisAdd);
        }

        permOpen = false;
	}

	public void SetActive(bool status)
	{

	}
}
