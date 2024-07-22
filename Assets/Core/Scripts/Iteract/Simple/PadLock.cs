using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadLock : MonoBehaviour
{
    private FirstPersonLook FPL;
    private FirstPersonMovement FPM;
    public GameObject PadLockTexture;

    public List<GameObject> wheels = new List<GameObject>();

    private int currentWheelPos = 0;
    private float anglerRotate;
    // Start is called before the first frame update
    void Start()
    {
        FPL = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FirstPersonLook>();
        FPM = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonMovement>();
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
        }
        

    }

    private void ChangeCodeWheel()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentWheelPos++;

            if (currentWheelPos > 3) currentWheelPos = 0;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            currentWheelPos++;

            if (currentWheelPos > 0) currentWheelPos = 3;
        }
    }

    private void RotateCodeWheel()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            wheels[currentWheelPos].transform.Rotate(-anglerRotate, 0, 0, Space.Self);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            wheels[currentWheelPos].transform.Rotate(anglerRotate, 0, 0, Space.Self);
        }
    }
}
