using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class RivalCoverAT : ActionTask {
		Collider collider;
		Vector3 baseHeight;
		NavMeshAgent navAgent;
		public GameObject hand;

		public Blackboard monitorBB;
		public BBParameter<bool> isCaught;
		public BBParameter<Transform> cover;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			collider = agent.GetComponent<Collider>();
			navAgent = agent.GetComponent<NavMeshAgent>();
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			//collider.center = Vector3.one * -2;
			navAgent.baseOffset = 0.5f;
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			Debug.Log("hiding");
			collider.enabled = false;
			hand.SetActive(false);
            if (monitorBB.GetVariableValue<bool>("isScanning") == false)
			{
                isCaught.value = false; // resets being caught
				cover.value = null;
                EndAction(true); //waits for monitor to stop scanning.
			} else
				EndAction(false);
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			navAgent.baseOffset = 1.3f;
        }

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}