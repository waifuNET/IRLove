using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class WomanAI : MonoBehaviour
{
    [System.Serializable]
    public class AIPoints
    {
        public string name;
        public Transform position;
    }
    public Transform currentPoint;
    public List<AIPoints> points = new List<AIPoints>();

    public NavMeshAgent agent;
    void Start()
    {
        
    }

    void Update()
    {
        if(currentPoint != null)
        {
            if (agent.remainingDistance < 0.01f)
            {
                //currentPoint = points[Random.Range(0, points.Count)].position;

				agent.SetDestination(currentPoint.position);
            }
            
        }
    }
}
