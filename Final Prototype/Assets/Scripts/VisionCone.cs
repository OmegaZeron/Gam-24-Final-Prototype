using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    [SerializeField] private float detectionTime;
    private float currentDetection;

    private void Start()
    {
        currentDetection = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && currentDetection == 0)
        {
            RaycastHit hit;
            if (Physics.Linecast(transform.position, other.transform.position, out hit))
            {
                if (hit.collider.tag == "Player")
                {
                    Debug.Log("Player is in line of sight of the enemy");
                    currentDetection = Time.time;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            currentDetection = 0;
        }
    }

    private void Update()
    {
        if (currentDetection > 0 && Time.time - currentDetection >= detectionTime)
        {
            Debug.Log("Player was detected by the enemy");
        }
    }
}
