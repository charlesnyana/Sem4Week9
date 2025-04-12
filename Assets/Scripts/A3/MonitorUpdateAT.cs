using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class MonitorUpdateAT : ActionTask {

		public BBParameter<float> timer;
		public BBParameter<bool> isScanning;
		public BBParameter<float> interval;
		public BBParameter<float> minTimerInterval;
        public BBParameter<float> maxTimerInterval;

		void ResetTimer()
		{
            timer.value = 0; 
			// sets new random interval
            interval.value = Random.Range(minTimerInterval.value, maxTimerInterval.value);
        }

        protected override string OnInit() {
			ResetTimer();
			return null;
		}

		protected override void OnExecute() {

		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {

			// timer logic
			if (!isScanning.value)
			{
                timer.value += Time.deltaTime;
            }
			
			if (timer.value > interval.value)
			{
				isScanning.value = true;
				ResetTimer();
			}
			// ----

		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}