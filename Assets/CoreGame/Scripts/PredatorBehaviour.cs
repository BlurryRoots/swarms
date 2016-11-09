using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PredatorBehaviour : SwarmBehaviour {

    public override void CalculateVelicity (List<SwarmBrainController> swarm) {
        if (null == this.prey) {
            this.prey = this.ChoosePrey (swarm);
        }

        this.direction = (this.prey.transform.position - this.transform.position).normalized;
        this.importance = SwarmBehaviour.maxImportance;
    }

    private SwarmBrainController ChoosePrey (List<SwarmBrainController> swarm) {
        SwarmBrainController pick = swarm[Random.Range (0, swarm.Count)];
        while (pick.gameObject == this.gameObject) {
            pick = swarm[Random.Range (0, swarm.Count)];
        }
        
        return pick;
    }

    private SwarmBrainController prey;

}
