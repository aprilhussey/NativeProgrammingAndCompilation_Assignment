using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    const float combatCooldown = 5f;
    float lastAttackTime;

    public float attackDelay = .6f;

    public bool InCombat { get; private set; }
    public event System.Action OnAttack;

    private CharacterStats myStats;
    CharacterStats opponentStats;

    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (Time.time - lastAttackTime > combatCooldown)
        {
            InCombat = false;
        }
    }

    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            opponentStats = targetStats;

            //StartCoroutine(DoDamage(targetStats, attackDelay));

            if (OnAttack != null)
            {
                OnAttack();
            }

            attackCooldown = 1f / attackSpeed;  // The greater the attack speed the smaller the cooldown
            InCombat = true;
            lastAttackTime = Time.time;
        }
    }

    /*IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);
		stats.TakeDamage(myStats.damage.GetValue());

        if (stats.currentHealth <= 0)
        {
            InCombat = false;
        }
	}*/

    public void AttackHitEvent()
    {
        opponentStats.TakeDamage(myStats.damage.GetValue());

		if (opponentStats.currentHealth <= 0)
		{
			InCombat = false;
		}
	}
}