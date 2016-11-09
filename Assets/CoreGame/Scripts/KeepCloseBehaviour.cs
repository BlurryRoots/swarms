using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeepCloseBehaviour : SwarmBehaviour {

    public float significantDistance = 10f;
    public float targetDistance = 2f;

    public override void CalculateVelicity (List<SwarmBrainController> swarm) {
        var pos = this.transform.position;
        SwarmBrainController nearestBrain = null;
        float nearestBrainDistance = 0f;

        foreach (var brain in swarm) {
            if (brain.gameObject == this.gameObject) {
                continue;
            }

            var brainDistance = Vector3.Distance (pos, brain.transform.position);

            if (null == nearestBrain) {
                nearestBrain = brain;
                nearestBrainDistance = brainDistance;
            }
            else {
                var preaviousNearestBrainDistance = Vector3.Distance (pos, nearestBrain.transform.position);

                if (brainDistance < preaviousNearestBrainDistance) {
                    nearestBrain = brain;
                    nearestBrainDistance = brainDistance;
                }
            }
        }

        var x = Mathf.Clamp (nearestBrainDistance, this.targetDistance, this.significantDistance);
        var r = MathUtil.Normalize (x, this.targetDistance, this.significantDistance);


        this.direction = (nearestBrain.transform.position - pos).normalized;
        this.importance = SwarmBehaviour.maxImportance * r;
    }

}
