using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class RivalUpdateAT : ActionTask {

        public BBParameter<bool> isCaught;
        public BBParameter<bool> isHiding;
        public BBParameter<float> detectionProgress;
        public float detectionDecay = 1;
        public float detectionRate = 1;
        public BBParameter<int> maxDetection;

        public BBParameter<Vector3> targetPos;
        public float sampleRate;
        public float sampleDist;

        NavMeshAgent navAgent;
        float timeSinceLastSample = 0;
        Vector3 lastDestination;

        public BBParameter<Vector3> spawnpoint;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
            navAgent = agent.GetComponent<NavMeshAgent>();
            spawnpoint.value = agent.transform.position;
            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {

		}

		protected override void OnUpdate() {

            timeSinceLastSample += Time.deltaTime;

            if (detectionProgress.value > 0 && !isCaught.value)
            {
                detectionProgress.value -= Time.deltaTime * detectionDecay;
            }

            if (isCaught.value && !isHiding.value)
            {
                detectionProgress.value += Time.deltaTime * detectionRate;
            }

            if (detectionProgress.value >=  maxDetection.value)
            {
                navAgent.isStopped = true;
                Debug.Log("rival bakc to start");
                navAgent.Warp(spawnpoint.value);
                targetPos.value = spawnpoint.value;
                detectionProgress.value = 0;
                isCaught.value = false;
            } 

                if (timeSinceLastSample > sampleRate)
            {
                if (lastDestination != targetPos.value)
                {
                    lastDestination = targetPos.value;

                    NavMeshHit hit;
                    bool foundPoint = NavMesh.SamplePosition(targetPos.value, out hit, sampleDist, NavMesh.AllAreas);

                    if (foundPoint)
                    {
                        Debug.Log("new point found");
                        navAgent.SetDestination(targetPos.value);
                    }
                }
            }
            EndAction(true);
        }

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}