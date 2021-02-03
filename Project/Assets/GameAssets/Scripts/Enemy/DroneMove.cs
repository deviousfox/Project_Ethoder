using UnityEngine;
using UnityEngine.AI;

public class DroneMove : MonoBehaviour
{
    public Transform Target;
    public float MoveSpeed = 2;
    public float VolumeRange = 2;
    private NavMeshAgent thisAgent;
    private Drone drone;
    Vector3 currentTargetPos;
    Vector3 lastTergetPos;

    private void OnEnable()
    {
        PlayerDelegate.OnPlayerSpawnetEventHandler += SetTarget;
    }
    private void OnDisable()
    {
        PlayerDelegate.OnPlayerSpawnetEventHandler -= SetTarget;
    }

    void Start()
    {
        thisAgent = thisAgent ?? GetComponent<NavMeshAgent>();
        drone = drone ?? GetComponent<Drone>();
        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SetTarget(Transform tr)
    {
        Target = tr;
    }

    // Update is called once per frame
    void Update()
    {
        if (drone.PlayerIsVisible())
        {
            thisAgent.SetDestination(Target.position);
            currentTargetPos = Target.position;
        }
        else
        {
            thisAgent.SetDestination(lastTergetPos);
        }
      
    }
    private void LateUpdate()
    {
        if (!drone.PlayerIsVisible())
        {
            lastTergetPos = currentTargetPos;
        }
    }
}
