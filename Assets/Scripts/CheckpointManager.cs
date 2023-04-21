using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public float MaxTimeToReachNextCheckpoint = 30f;
    public float TimeLeft = 30f;
    
    public RaceCarAgent carAgent;
    public Checkpoint nextCheckPointToReach;
    
    private int CurrentCheckpointIndex;
    private List<Checkpoint> Checkpoints;

    public GameObject Map;
    private Checkpoint lastCheckpoint;

    public event Action<Checkpoint> reachedCheckpoint; 
    public GameObject raceCar;

    void Start()
    {
        //Checkpoints = FindObjectOfType<Checkpoints>().checkPoints;
        Checkpoints = Map.transform.Find("Checkpoints").GetComponent<Checkpoints>().checkPoints;
        ResetCheckpoints();
    }

    public void ResetCheckpoints()
    {
        CurrentCheckpointIndex = 0;
        TimeLeft = MaxTimeToReachNextCheckpoint;
        
        SetNextCheckpoint();
    }

    private void Update()
    {
        TimeLeft -= Time.deltaTime;

        if (TimeLeft < 0f)
        {
            carAgent.AddReward(-1f);
            carAgent.EndEpisode();
        }
    }

    public void CheckPointReached(Checkpoint checkpoint)
    {
        if (nextCheckPointToReach != checkpoint) return;
        
        lastCheckpoint = Checkpoints[CurrentCheckpointIndex];
        reachedCheckpoint?.Invoke(checkpoint);
        CurrentCheckpointIndex++;

        if (CurrentCheckpointIndex >= Checkpoints.Count){
            if(Mathf.Abs(raceCar.transform.InverseTransformDirection(lastCheckpoint.transform.position - raceCar.transform.position).x) < 3.5f){
                carAgent.AddReward(0.5f);
                carAgent.EndEpisode();
            }
        }
        else{
            if(Mathf.Abs(raceCar.transform.InverseTransformDirection(lastCheckpoint.transform.position - raceCar.transform.position).x) < 3.5f){
                carAgent.AddReward((0.5f) / Checkpoints.Count);
                SetNextCheckpoint();
            }
        }
    }

    private void SetNextCheckpoint()
    {
        if (Checkpoints.Count > 0)
        {
            TimeLeft = MaxTimeToReachNextCheckpoint;
            nextCheckPointToReach = Checkpoints[CurrentCheckpointIndex];
            
        }
    }
}
