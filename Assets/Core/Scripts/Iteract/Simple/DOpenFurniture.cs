using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class DOpenFurniture : MonoBehaviour, Iteraction
{
	public bool status = false;
	public bool permOpen = false;
	public bool useLocaleCoordinatePosition = true;
	public RotateMode rotateMode = RotateMode.Fast;


	[HideInInspector] public Vector3 startPosition;
	public Vector3 needPosition;
	private Vector3 _needPosition;

	[HideInInspector] public Vector3 startRotation;
    public Vector3 needRotation;



    private void Start()
	{
        _needPosition = needPosition;
		if (useLocaleCoordinatePosition)
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
			transform.DORotate(needRotation, 1, rotateMode);
        }
			
		else
		{
            transform.DOMove(startPosition, 1);
			transform.DORotate(startRotation, 1, rotateMode);
			

        }

        permOpen = false;
	}

	public void SetActive(bool status)
	{

	}
}
