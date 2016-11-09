using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AvoidCollisionBehaviour : SwarmBehaviour {

    public float distanceMaxThreshold = 10f;
    public float distanceMinThreshold = 1.3f;

    public override void CalculateVelicity (List<SwarmBrainController> swarm) {
        var pos = this.transform.position;
        SwarmBrainController nearestBrain = null;
        float nearestBrainDistance = 0f;

        foreach (var brain in swarm) {
            if (this.gameObject == brain.gameObject) {
                continue;
            }

            var brainDistance = Vector3.Distance (pos, brain.transform.position);
            
            if (null == nearestBrain) {
                nearestBrain = brain;
                nearestBrainDistance = brainDistance;
            }
            else {
                var preaviousNearestBrainDistance = Vector3.Distance (pos, nearestBrain.transform.position);

                if (brainDistance < this.distanceMaxThreshold && brainDistance < preaviousNearestBrainDistance) {
                    nearestBrain = brain;
                    nearestBrainDistance = brainDistance;
                }
            }
        }

        var x = Mathf.Clamp (nearestBrainDistance, this.distanceMinThreshold, this.distanceMaxThreshold);
        var r = 1f - MathUtil.Normalize (x, this.distanceMinThreshold, this.distanceMaxThreshold);

        this.direction = -1f * (nearestBrain.transform.position - pos).normalized;
        this.importance = SwarmBehaviour.maxImportance * r;
    }

}
