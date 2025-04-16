
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class RivalCaughtAT : ActionTask {

		public BBParameter<bool> isCaught;
		public BBParameter<Vector3> spawnpoint;
		public BBParameter<Vector3> targetPos;

		NavMeshAgent NavMeshAgent;

        public BBParameter<float> detectProgress;
        public BBParameter<int> maxDetectProgress;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			NavMeshAgent = agent.GetComponent<NavMeshAgent>();
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            Debug.Log("rival caught!");
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            
			if (detectProgress.value >= maxDetectProgress.value) //if detected fully, returns to seat ans reset detection
			{
				detectProgress.value = 0f;
				isCaught.value = false;
                EndAction(true);
            } else
			{
				EndAction(false);
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