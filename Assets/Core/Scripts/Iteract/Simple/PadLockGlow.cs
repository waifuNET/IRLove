using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PadLockGlow : MonoBehaviour
{
    public float blinkTime = 0.5f;

    private GameObject currentWheel;

    public bool isSelect;

    private void Awake()
    {
        currentWheel = gameObject;
    }
    public void PadLockGlowing()
    {
        currentWheel.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

        if (isSelect)
        {
            currentWheel.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.clear, Color.grey, Mathf.PingPong(Time.time, blinkTime)));
        }
        if(!isSelect)
        {
            currentWheel.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.clear);
        }
    }
}
