using UnityEngine;
using UnityEngine.AI;

public class BasicAllies : MonoBehaviour
{
    [SerializeField] protected GameObject Player;
     protected NavMeshAgent ally_agent;
     public AllyHeroData heroData;
     protected int currentHp;
     protected string allyHeroName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        ally_agent=GetComponent<NavMeshAgent>();
        currentHp=heroData.MaxHP;
        allyHeroName= heroData.HeroName;
        Debug.Log("Hero Name: "+allyHeroName);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Movement();
        AllyHeroAttack();
    }
    protected virtual void Movement()
    {
        ally_agent.SetDestination(Player.transform.position);
    }
    public virtual void AllyHeroAttack()
    {
        Debug.Log("Basic attack");
    }
}
