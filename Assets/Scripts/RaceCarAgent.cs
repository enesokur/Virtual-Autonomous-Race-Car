using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class RaceCarAgent : Agent
{
   public CheckpointManager _checkpointManager;
   private Car _carController;
   
   //called once at the start
   public override void Initialize()
   {
      _carController = GetComponent<Car>();
   }
   
   //Called each time it has timed-out or has reached the goal
   public override void OnEpisodeBegin()
   {
      _checkpointManager.ResetCheckpoints();
      _carController.Respawn();
   }

   #region Edit this region!

      //Collecting extra Information that isn't picked up by the RaycastSensors
      public override void CollectObservations(VectorSensor sensor){
         Vector3 diff = _checkpointManager.nextCheckPointToReach.transform.position - transform.position;
         sensor.AddObservation(diff / 20f);
         AddReward(-0.001f);
      }

      //Processing the actions received
      public override void OnActionReceived(ActionBuffers actions){
         var input = actions.ContinuousActions;
         _carController.MoveCar(input[1],input[0]);
         
      }
      
      //For manual testing with human input, the actionsOut defined here will be sent to OnActionRecieved
      public override void Heuristic(in ActionBuffers actionsOut){
         var action = actionsOut.ContinuousActions;
         action[0] = Input.GetAxis("Horizontal");
         action[1] = Input.GetAxis("Vertical");
      }
      
   #endregion
}
