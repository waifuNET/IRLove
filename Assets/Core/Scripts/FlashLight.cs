using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public bool canUse = true;
    public Light flashLight;
    public GameObject flashTexture;
    private bool status = false;

    public float maxIntensity = 1.5f;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            status = !status;
            if (status) flashLight.intensity = maxIntensity;
            else flashLight.intensity = 0;
		}
    }
}
