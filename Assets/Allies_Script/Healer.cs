using UnityEngine;

public class Healer : BasicAllies
{
    private float basicAttackCooldown = 3f, singleHealCooldown = 5f, ultimateCooldown = 30f;
    private float nextBasicAttackTime, nextSingleHealAttackTime, nextUltimateTime;
    public override void AllyHeroAttack()
    {
        nextUltimateTime = Time.time + ultimateCooldown;
        nextSingleHealAttackTime = Time.time + singleHealCooldown;
        nextBasicAttackTime = Time.time + basicAttackCooldown;
        if (Time.time >= nextUltimateTime)
        {
            ultimateHealStings();
            nextUltimateTime = Time.time + ultimateCooldown;
        }
        else if (Time.time >= nextSingleHealAttackTime)
        {
            singleHeal();
            nextSingleHealAttackTime = Time.time + singleHealCooldown;
        }
        else if (Time.time >= nextBasicAttackTime)
        {
            basicAttack();
            nextBasicAttackTime = Time.time + basicAttackCooldown;
        }

    }

    private void basicAttack()
    {

        Debug.Log("basic attack");

    }
    private void singleHeal()
    {
        Debug.Log("Single heal attack");

    }
    private void ultimateHealStings()
    {

        Debug.Log("Ultimate heal attack");

    }
}
