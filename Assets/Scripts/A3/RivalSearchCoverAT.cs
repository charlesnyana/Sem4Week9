using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class RivalSearchCoverAT : ActionTask {

        Blackboard agentBB;
        public BBParameter<float> searchRadius;
        public BBParameter<float> baseSearchRadius;
        public BBParameter<Vector3> targetPos;

        public Color drawColour;
        public int numberOfScanCirclePoints;
        public LayerMask coverLayerMask;
        public BBParameter<Transform> targetTransform;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {

        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {
            DrawCircle(agent.transform.position, searchRadius.value, drawColour, numberOfScanCirclePoints); // debugging

            Collider[] detectedColliders =
                Physics.OverlapSphere(agent.transform.position, searchRadius.value, coverLayerMask);

            if (detectedColliders.Length > 0 )
            {
                targetTransform.value = detectedColliders[0].GetComponentInParent<Transform>();
                Debug.Log("Found cover in " + detectedColliders[0]);
                targetPos.value = targetTransform.value.position;
                EndAction(true);
            }
        }

        protected override void OnStop()
        {
 
        }

        private void DrawCircle(Vector3 center, float radius, Color colour, int numberOfPoints)
        {
            Vector3 startPoint, endPoint;
            int anglePerPoint = 360 / numberOfPoints;
            for (int i = 1; i <= numberOfPoints; i++)
            {
                startPoint = new Vector3(Mathf.Cos(Mathf.Deg2Rad * anglePerPoint * (i - 1)), 0, Mathf.Sin(Mathf.Deg2Rad * anglePerPoint * (i - 1)));
                startPoint = center + startPoint * radius;
                endPoint = new Vector3(Mathf.Cos(Mathf.Deg2Rad * anglePerPoint * i), 0, Mathf.Sin(Mathf.Deg2Rad * anglePerPoint * i));
                endPoint = center + endPoint * radius;
                Debug.DrawLine(startPoint, endPoint, colour);
            }
        }
    }
}