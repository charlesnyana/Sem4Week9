using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class ScanForOffenderAT : ActionTask {

        public BBParameter<float> scanRange = 5f;
        public BBParameter<int> rayCount = 7;
        public BBParameter<float> fanAngle = 45f;
        public LayerMask scanLayers;
        public BBParameter<GameObject> target;

        private LineRenderer lineRenderer;

        protected override string OnInit() {
            lineRenderer = agent.GetComponent<LineRenderer>();
            lineRenderer.useWorldSpace = true;
            lineRenderer.positionCount = rayCount.value + 2; 
            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            Vector3 origin = agent.transform.position + Vector3.up * 1f; // Raise ray origin to eye level
            float angleStep = fanAngle.value / (rayCount.value - 1);
            float halfAngle = fanAngle.value / 2f;

            for (int i = 0; i < rayCount.value; i++)
            {
                float angle = -halfAngle + i * angleStep;
                Vector3 direction = Quaternion.Euler(0f, angle, 0f) * -agent.transform.forward;

                if (Physics.Raycast(origin, direction, out RaycastHit hit, scanRange.value, scanLayers))
                {
                    Debug.DrawRay(origin, direction * hit.distance, Color.red, 0.2f);

                    target.value = hit.collider.gameObject;
                    EndAction(true);
                    return;
                }
                else
                {
                    Debug.DrawRay(origin, direction * scanRange.value, Color.gray, 0.2f);
                }
                DrawVisionCone();
            }
        }

        void DrawVisionCone()
        {
            Vector3[] points = new Vector3[rayCount.value + 2];
            Vector3 eye = agent.transform.position + Vector3.up * 1f;
            points[0] = eye; // Origin of cone

            float angleStep = fanAngle.value / rayCount.value;
            float startAngle = fanAngle.value / 2f;

            for (int i = 0; i <= rayCount.value; i++)
            {
                float currentAngle = -startAngle + angleStep * i;

                Vector3 direction = Quaternion.Euler(0, currentAngle, 0) * -agent.transform.forward;

                points[i + 1] = eye + direction * scanRange.value;
            }

            lineRenderer.positionCount = points.Length;
            lineRenderer.SetPositions(points);
        }
    }
}