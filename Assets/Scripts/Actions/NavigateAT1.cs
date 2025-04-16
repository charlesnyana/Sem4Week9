using JetBrains.Annotations;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class NavigateAT : ActionTask {
        public BBParameter<Vector3> targetPos;
        public float sampleRate;
		public float sampleDist;

        NavMeshAgent navAgent;
		float timeSinceLastSample = 0;
		Vector3 lastDestination;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {

			navAgent = agent.GetComponent<NavMeshAgent>();
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {

		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			timeSinceLastSample += Time.deltaTime;

			if (timeSinceLastSample > sampleRate)
			{
				if(lastDestination != targetPos.value)
				{
                    lastDestination = targetPos.value;

					NavMeshHit hit;
					bool foundPoint = NavMesh.SamplePosition(targetPos.value, out hit, sampleDist, NavMesh.AllAreas);

					if (foundPoint)
					{
                        navAgent.SetDestination(targetPos.value);
                    }
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