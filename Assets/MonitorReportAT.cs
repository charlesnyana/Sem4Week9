using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class MonitorReportAT : ActionTask {

        public BBParameter<bool> isScanning;
        public BBParameter<GameObject> target;
		public BBParameter<int> maxDetectionProgress;
        public BBParameter<int> detectionRate;
        Blackboard targetBB;
		float detectionProgress;

		public GameObject alertSig;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            targetBB = target.value.GetComponent<Blackboard>();
            detectionProgress = targetBB.GetVariableValue<float> ("detectionProgress");
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			if (detectionProgress >= maxDetectionProgress.value) //if detection progress is maxed out, AT has succeeded and ends.
                EndAction(true);

			if (!targetBB.GetVariableValue<bool>("isCaught")) // if target is no longer caught (hiding), action ends and fails.
			{
				target.value = null;
                EndAction(false);
            }

			//Increments detection rate of target if neither AT end conditions are met.
			detectionProgress += detectionRate.value * Time.deltaTime;
            targetBB.SetVariableValue("detectionProgress", detectionProgress);

			alertSig.SetActive(true);
        }
        protected override void OnStop()
        {
            alertSig.SetActive(false);
        }
        protected override void OnPause()
        {
            alertSig.SetActive(false);
        }
    }
}