using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class MonitorTurnAT : ActionTask {
        public BBParameter<GameObject> target;
        public BBParameter<float> rotationSpeed = 30f; // Degrees per second
        public BBParameter<float> maxAngle = 45f;

        private float currentYRotation;
        private int direction = -1; //
        private float baseY;

        protected override void OnExecute()
        {
            baseY = agent.transform.eulerAngles.y;
            currentYRotation = 0f;
        }

        protected override void OnUpdate()
        {
            if (target.value != null)
            {
                EndAction(true); // Target found, don't rotate
                return;
            }

            float step = rotationSpeed.value * Time.deltaTime * direction;
            currentYRotation += step;
            agent.transform.Rotate(0f, step, 0f);

            if (Mathf.Abs(currentYRotation) >= maxAngle.value)
            {
                direction *= -1; // Reverse direction
            }

            // Keep running
        }
    }
}