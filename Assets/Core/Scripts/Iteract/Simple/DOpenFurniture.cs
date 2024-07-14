using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class DOpenFurniture : MonoBehaviour, Iteraction
{
	public bool status = false;
	public bool permOpen = false;
	public bool autoStartPosition = true;
	public bool autoStartRotation = true;


    public Vector3 startPosition;
	public Vector3 needPosition;
	private Vector3 _needPosition;

	public Quaternion startRotation;
    public Quaternion needRotation;
    private Quaternion _needRotation;



    private void Start()
	{
		_needPosition = needPosition;
		if (autoStartPosition)
			startPosition = transform.position;
		if(autoStartRotation)
			startRotation = transform.rotation;
	}
	public void Iterction()
	{
		needPosition = startPosition + _needPosition;
		if (!permOpen)
			status = !status;
		if (status)
		{
			transform.DOMove(needPosition, 1);
			transform.DORotate(new Vector3(needRotation.eulerAngles.x,needRotation.eulerAngles.y,needRotation.eulerAngles.z),1,RotateMode.WorldAxisAdd);
        }
			
		else
		{
            transform.DOMove(startPosition, 1);
			transform.DORotate(new Vector3(-needRotation.eulerAngles.x, -needRotation.eulerAngles.y, -needRotation.eulerAngles.z), 1, RotateMode.WorldAxisAdd);
        }

        permOpen = false;
	}

	public void SetActive(bool status)
	{

	}
}
