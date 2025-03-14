using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class MoveTowardsAT : ActionTask {

		public BBParameter<Transform> target;
		public Transform targetA;
        public Transform targetB;
        public BBParameter<float> arrivalDistance;
		public float speed;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			target.value = targetA;

            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			Vector3 destinationVector = (target.value.position - agent.transform.position).normalized;
			agent.transform.position += destinationVector * Time.deltaTime * speed;

			float distance = Vector3.Distance(target.value.position, agent.transform.position);

			if (distance <= arrivalDistance.value)
			{
				if (target.value == targetA) target.value = targetB;
				else target.value = targetA;
            }
				
			EndAction(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}