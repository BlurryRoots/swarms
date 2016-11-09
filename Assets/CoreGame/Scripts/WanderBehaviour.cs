using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WanderBehaviour : SwarmBehaviour {

    public float timeUntilDestinationChange = 0.618f;

    public void Start () {
        this.destination = this.transform.position;
    }

    public override void CalculateVelicity (List<SwarmBrainController> swarm) {
        if (this.currentDestinationTime > this.timeUntilDestinationChange) {
            this.currentDestinationTime = 0;
            this.CalculateNewDestination ();
        }
        else {
            this.currentDestinationTime += Time.deltaTime;
        }

        this.direction = (this.destination - this.transform.position).normalized;
        this.importance = SwarmBehaviour.maxImportance * 0.3f;
    }

    public void CalculateNewDestination () {
        this.destination += Random.insideUnitSphere * 10f;
    }

    private Vector3 destination;
    private float currentDestinationTime;

}
