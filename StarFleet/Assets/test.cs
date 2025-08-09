using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class test : MonoBehaviour
{
    public GameObject target;
    public NavMeshAgent agent;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.stoppingDistance = 3;
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        agent.SetDestination(target.transform.position);
    }
}
