using UnityEngine;

public class SmoothViewTo : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 1;
    public Vector3 offsetRotation = Vector3.zero;

    void Update()
    {
        if (target == null) return;

        Vector3 direction = target.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        Quaternion offsetQuat = Quaternion.Euler(offsetRotation);
        targetRotation *= offsetQuat;

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
    }
}
