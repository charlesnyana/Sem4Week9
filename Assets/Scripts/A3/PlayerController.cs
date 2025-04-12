using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class PlayerController : ActionTask {

        public float moveSpeed = 1;
        public float rotationSpeed = 1;

        public BBParameter<bool> isCaught;

        Vector3 spawnPoint;
        Vector3 standardHeight;
        public float crouchOffset;

        protected override string OnInit()
        {
            spawnPoint = agent.transform.position;
            
            return null;
        }

        protected override void OnExecute()
        {

        }
        protected override void OnUpdate()
        {
            Quaternion targetRotation = agent.transform.rotation * Quaternion.Euler(Vector3.up * Input.GetAxis("Pan") * 10);
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            Vector3 forwardMovement = agent.transform.forward * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            Vector3 strafeMovement = agent.transform.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
 
            agent.transform.position += strafeMovement + forwardMovement;

            if (isCaught.value)
            {
                agent.transform.position = spawnPoint;
                isCaught.value = false;
            }

            //crouch
            Vector3 agentPos = agent.transform.position;
            
            if (Input.GetKeyDown(KeyCode.Space)) {
                Debug.Log("crouching");
                Vector3 crouch = new Vector3(agentPos.x, agentPos.y - crouchOffset, agentPos.z);
                agent.transform.position = new Vector3(agentPos.x, agentPos.y - crouchOffset, agentPos.z);
            }
            if (Input.GetKeyUp(KeyCode.Space)) {
                agent.transform.position = new Vector3(agentPos.x, agentPos.y + crouchOffset, agentPos.z);
            }

     
        }
       
		protected override void OnStop() {
			
		}

		protected override void OnPause() {
			
		}
	}
}