using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class MonitorTurnAT : ActionTask {
        public BBParameter<GameObject> target;
        public BBParameter<float> rotationSpeed = 30f; // Degrees per second
        public BBParameter<float> maxAngle = 45f;


        public GameObject alertSig;

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
            alertSig.SetActive(false);
            if (target.value != null)
            {
                alertSig.SetActive(true);
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
            EndAction(false);   
        }
    }
}