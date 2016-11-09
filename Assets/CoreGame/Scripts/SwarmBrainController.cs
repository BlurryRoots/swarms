using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwarmBrainController : MonoBehaviour {

    public float speed = 2f;

	void Start () {
        this.behaviours = new Dictionary<SwarmBehaviourPriority, List<SwarmBehaviour>> ();
        foreach (var behaviour in this.GetComponents<SwarmBehaviour> ()) {
            if (false == this.behaviours.ContainsKey (behaviour.priority)) {
                this.behaviours.Add (behaviour.priority, new List<SwarmBehaviour> ());
            }

            this.behaviours[behaviour.priority].Add (behaviour);
        }
        this.brainsInScene = new List<SwarmBrainController> (GameObject.FindObjectsOfType<SwarmBrainController> ());
	}
	
	void Update () {
        var importanceBudge = SwarmBehaviour.maxImportance;
        this.velocity = Vector3.zero;

        if (this.behaviours.ContainsKey (SwarmBehaviourPriority.High)) {
            foreach (var behaviour in this.behaviours[SwarmBehaviourPriority.High]) {
                behaviour.CalculateVelicity (this.brainsInScene);
                importanceBudge = Mathf.Clamp (importanceBudge - behaviour.importance, 0, importanceBudge);

                this.velocity += behaviour.direction * behaviour.importance;
            }
        }

        if (this.behaviours.ContainsKey (SwarmBehaviourPriority.Low)) {
            var lowPriorityImportanceSum = 0f;
            foreach (var behaviour in this.behaviours[SwarmBehaviourPriority.Low]) {
                behaviour.CalculateVelicity (this.brainsInScene);
                lowPriorityImportanceSum += behaviour.importance;
            }
            foreach (var behaviour in this.behaviours[SwarmBehaviourPriority.Low]) {
                var importanceFraction = (behaviour.importance / lowPriorityImportanceSum) * importanceBudge;
                this.velocity += behaviour.direction * importanceFraction;
            }
        }

        this.transform.position += this.velocity * this.speed * Time.deltaTime;
	}

    private Vector3 velocity;
    private Dictionary<SwarmBehaviourPriority, List<SwarmBehaviour>> behaviours;
    private List<SwarmBrainController> brainsInScene;

}
