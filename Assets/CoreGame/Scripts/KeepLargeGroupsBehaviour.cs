using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeepLargeGroupsBehaviour : SwarmBehaviour {

    public float significantDistance = 10f;
    public float targetDistance = 2f;

    public override void CalculateVelicity (List<SwarmBrainController> swarm) {
        var pos = this.transform.position;
        var maxDistance = 0f;

        foreach (var brain in swarm) {
            if (this.gameObject == brain.gameObject) {
                continue;
            }

            var distance = Vector3.Distance (pos, brain.transform.position);

            if (distance > maxDistance) {
                maxDistance = distance;
            }
        }

        var halfDistance = maxDistance / 2f;
        var farGroup = new List<Vector3> ();
        foreach (var brain in swarm) {
            if (this.gameObject == brain.gameObject) {
                continue;
            }

            var distance = Vector3.Distance (pos, brain.transform.position);
            if (distance > halfDistance) {
                farGroup.Add (brain.transform.position);
            }
        }

        var halfCount = swarm.Count / 2f;
        var k = farGroup.Count - halfCount;
        if (0 < k) {
            var medianPosition = MathUtil.Median (farGroup);
            this.direction = (medianPosition - pos).normalized;
            this.importance = maxImportance * MathUtil.Normalize (k, 0, halfCount);
        }
    }

}
