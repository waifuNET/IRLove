using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadLock : MonoBehaviour
{
    private FirstPersonLook FPL;
    private FirstPersonMovement FPM;
    public GameObject PadLockTexture;
    public GameObject PadLockPickUP;

    private Vector3 pickUpPos;

    public List<GameObject> wheels = new List<GameObject>();
    public List<GameObject> pickupwheels = new List<GameObject>();

    private int currentWheelNum = 0;
    private float anglerRotate = 36;

    private Password _password;

    public int[] activeNumbers = {0,0,0,0};
    // Start is called before the first frame update
    void Start()
    {
        _password = FindFirstObjectByType<Password>();
        FPL = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FirstPersonLook>();
        FPM = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonMovement>();
        DetectPickUPPos();

        foreach (GameObject r in pickupwheels)
        {
            r.transform.Rotate(-36, 0, 0, Space.Self);
        }
        foreach (GameObject r in wheels)
        {
            r.transform.Rotate(36, 0, 0, Space.Self);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PadLockTexture.activeSelf)
        {
            FPL.LockCamera();
            FPM.LockMovement();
            ChangeCodeWheel();
            RotateCodeWheel();
            DetectPickUPPos();
        }
        if(!PadLockTexture.activeSelf)
        {
            PadLockPickUP.transform.position = pickUpPos;
        }
    }
    private void DetectPickUPPos()
    {
        pickUpPos = PadLockPickUP.transform.position;
    }    
    private void ChangeCodeWheel()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentWheelNum++;

            if (currentWheelNum > 3) currentWheelNum = 0;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            currentWheelNum--;

            if (currentWheelNum < 0) currentWheelNum = 3;
        }
    }

    private void RotateCodeWheel()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            MoveWheel(-1); // i = -1 for W
            if (activeNumbers[currentWheelNum] > 9) activeNumbers[currentWheelNum] = 0;
            _password.CorrectPassword();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            MoveWheel(1); // i=1 for S
            if (activeNumbers[currentWheelNum] < 0) activeNumbers[currentWheelNum] = 9;
            _password.CorrectPassword();
        }
    }

    private void MoveWheel(int i) // i = 1 for S || i = -1 for W
    {
        wheels[currentWheelNum].transform.Rotate(i*anglerRotate, 0, 0, Space.Self);
        pickupwheels[currentWheelNum].transform.Rotate(i*anglerRotate, 0, 0, Space.Self);
        if(i > 0) activeNumbers[currentWheelNum]--;
        if(i < 0) activeNumbers[currentWheelNum]++;
    }
}
