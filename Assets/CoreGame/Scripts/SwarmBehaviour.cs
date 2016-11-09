using UnityEngine;
using System.Collections.Generic;

public abstract class SwarmBehaviour : MonoBehaviour {
    
    public static float maxImportance = 10f;

    public SwarmBehaviourPriority priority;
    public float importance;
    public Vector3 direction;

    public abstract void CalculateVelicity (List<SwarmBrainController> swarm);

}

public enum SwarmBehaviourPriority {

    Low,
    High,

}
