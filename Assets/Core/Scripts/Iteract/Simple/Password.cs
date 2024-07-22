using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Password : MonoBehaviour
{
    private PadLock _padLock;

    public int[] password = { 0, 0, 0, 0 };

    // Start is called before the first frame update
    void Awake()
    {
        _padLock = FindFirstObjectByType<PadLock>();
    }

    public void CorrectPassword()
    {
        if(_padLock.activeNumbers.SequenceEqual(password))
        {
            _padLock.PadLockPickUP.SetActive(false);
            _padLock.PadLockTexture.SetActive(false);
        }
    }
}
