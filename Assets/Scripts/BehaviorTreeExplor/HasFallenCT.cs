using System.Collections.Generic;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

    public class HasFallenCT : ConditionTask
    {
        Blackboard agentBB;
        List<GameObject> allBalloons = new List<GameObject>();
        List<float> distanceFromBilly = new List<float>();
        public float detectionRadius;
        public LayerMask balloonLayer;
        public BBParameter<GameObject> closestBalloon;

        public GameObject ground;
        public BBParameter<float> fallDetectThreshold;
        public BBParameter<float> fallenDetectThreshold;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            return null;
        }

        //Called whenever the condition gets enabled.
        protected override void OnEnable()
        {

        }

        //Called whenever the condition gets disabled.
        protected override void OnDisable()
        {

        }

        //Called once per frame while the condition is active.
        //Return whether the condition is success or failure.
        protected override bool OnCheck()
        {
            FindBalloons();

            float distanceFromGround = closestBalloon.value.transform.position.y;
            if (distanceFromGround <= fallenDetectThreshold.value)
            {
                return true;
            }
            else
                return false;
        }

        void FindBalloons()
        {
            agentBB = agent.GetComponent<Blackboard>();

            Collider[] balloons = Physics.OverlapSphere(agent.transform.position, detectionRadius, balloonLayer);
            Debug.Log("Found " + balloons.Length + " balloons");

            float closestBalloonDist = 10000;
            foreach (Collider balloon in balloons)
            {
                allBalloons.Add(balloon.gameObject);

                float currentBalloonDist;
                currentBalloonDist = Vector3.Distance(agent.transform.position, balloon.transform.position);
                distanceFromBilly.Add(currentBalloonDist);

                if (currentBalloonDist < closestBalloonDist)
                {
                    currentBalloonDist = closestBalloonDist;
                    closestBalloon.value = balloon.gameObject;

                    Debug.Log("new closest balloon: " + closestBalloon);
                }
            }
        }
    }
}