using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class MoveAT : ActionTask {

        public BBParameter<GameObject> targetBalloon;
		public float arrivalDistance;
		public float speed;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            Debug.Log("Move AT running");
            
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			Vector3 directionToTarget = agent.transform.position - targetBalloon.value.transform.position;



			if (Vector3.Distance(agent.transform.position, targetBalloon.value.transform.position) >= arrivalDistance)
			{
				agent.transform.position += directionToTarget.normalized * Time.deltaTime * speed;
                EndAction(true);
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