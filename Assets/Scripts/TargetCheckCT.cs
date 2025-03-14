using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class TargetCheckCT : ConditionTask {
        public BBParameter<Transform> target;
		public Transform targetA;
        public BBParameter<bool> goingToA;
        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit(){
			target.value = targetA;
			return null;
		}

		//Called whenever the condition gets enabled.
		protected override void OnEnable() {
			
		}

		//Called whenever the condition gets disabled.
		protected override void OnDisable() {
			
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {
			if (target.value == targetA)
			{
				goingToA.value = true;
			}
			else goingToA = false;

			return goingToA.value;
		}
	}
}