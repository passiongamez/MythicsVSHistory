using UnityEngine;
using UnityEngine.AI;
public class DummyEnemyScript : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent=GetComponent<NavMeshAgent>();
        player=FindAnyObjectByType<DummyPlayerControllerScript>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);
    }
}
