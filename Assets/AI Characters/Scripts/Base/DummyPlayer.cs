using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class DummyPlayer : MonoBehaviour
{
     private NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         agent=GetComponent<UnityEngine.AI.NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray= Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
    
}
