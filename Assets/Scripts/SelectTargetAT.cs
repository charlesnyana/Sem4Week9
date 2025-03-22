using System.Collections.Generic;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class SelectTargetAT : ActionTask {
        public float detectionRadius;
		public LayerMask thiefLayer;
		public Transform closestThief;
		float closestThiefDistance; 

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            SetTarget();
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            if (Vector3.Distance(agent.transform.position, closestThief.transform.position) == closestThiefDistance) return; //if the distance is the same as closest thief
            SetTarget();

        }

		void SetTarget()
		{
            Collider[] allThieves = Physics.OverlapSphere(agent.transform.position, detectionRadius, thiefLayer);
            List<Transform> thiefPositions = new List<Transform>();
            List<float> distanceFromGuard = new List<float>();

            foreach (Collider thief in allThieves)
            {
                Transform thiefTransform = thief.GetComponent<Transform>();
                thiefPositions.Add(thiefTransform);

                float currentThiefDistance = Vector3.Distance(agent.transform.position, thiefTransform.position);

                if (closestThiefDistance > currentThiefDistance)
                {
                    closestThiefDistance = currentThiefDistance;
                    closestThief = thiefTransform;
                    Debug.Log("new closest thief is " + closestThief);
                }
                if (closestThief == null)
                {

                    closestThiefDistance = currentThiefDistance;
                    closestThief = thiefTransform;
                }
            }
        }

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}