using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class RivalSearchAT : ActionTask {

        public BBParameter<GameObject> target;
        public BBParameter<Vector3> targetPos;
        public BBParameter<GameObject> cover;

        Collider collider;
		public GameObject hand;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
            collider = agent.GetComponent<Collider>();
            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			targetPos.value = target.value.transform.position;
            collider.enabled = true;
			cover.value = null;
			hand.SetActive(true);
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            targetPos.value = target.value.transform.position;
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