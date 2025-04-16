using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;



namespace NodeCanvas.Tasks.Actions {

	public class SearchAT : ActionTask {

		Blackboard agentBB;
		float searchRadius;
		float baseSearchRadius;
        public BBParameter<float> minNectarDetect;
        public BBParameter<Vector3> targetPos;

        public Color drawColour;
        public int numberOfScanCirclePoints;
		public LayerMask flowerLayerMask;
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
			agentBB = agent.GetComponent<Blackboard>();
			searchRadius = agentBB.GetVariableValue<float>("searchRadius");
            baseSearchRadius = agentBB.GetVariableValue<float>("initialSearchRadius");

        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            DrawCircle(agent.transform.position, searchRadius, drawColour, numberOfScanCirclePoints);

            Collider[] detectedColliders =
                Physics.OverlapSphere(agent.transform.position, searchRadius, flowerLayerMask);

            foreach (Collider detectedCollider in detectedColliders)
            {
                Blackboard flowerBB = detectedCollider.GetComponent<Blackboard>();

                if (flowerBB == null)
                {
                    Debug.LogError("SearchAT: flower blackboard not found");
                    return;
                }

                float nectarVal = flowerBB.GetVariableValue<float>("nectar");

                if (nectarVal > minNectarDetect.value)
                {
					Debug.Log("found FLOWER!");
                    agentBB.SetVariableValue("searchRadius", baseSearchRadius);
                    targetTransform.value = flowerBB.GetComponent<Transform>();

                    Vector3 directionToTarget = targetTransform.value.position;

                    targetPos.value = directionToTarget;
                    EndAction(true);
                }
            }
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

        //Called when the task is disabled.
        protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}