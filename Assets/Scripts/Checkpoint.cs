using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if (other.transform.Find("CheckpointManager").GetComponent<CheckpointManager>() != null){
            other.transform.Find("CheckpointManager").GetComponent<CheckpointManager>().CheckPointReached(this);
        }
    }
}
