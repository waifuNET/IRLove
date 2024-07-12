using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsInfo : MonoBehaviour
{
    public Dictionary<string, Quaternion> RotationDict = new Dictionary<string, Quaternion>();

    public void SetNewItemRotation(GameObject g)
    {
        RotationDict.Add(g.name, g.gameObject.GetComponent<Transform>().rotation);
    }
}
