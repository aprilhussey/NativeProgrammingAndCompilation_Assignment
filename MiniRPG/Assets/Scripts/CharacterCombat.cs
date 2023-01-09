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
    Animator opponentAnimator;

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

    public void Attack(CharacterStats targetStats, Animator targetAnimator)
    {
        if (attackCooldown <= 0f)
        {
            opponentStats = targetStats;
            opponentAnimator = targetAnimator;

            if (OnAttack != null)
            {
                OnAttack();
            }

            attackCooldown = 1f / attackSpeed;  // The greater the attack speed the smaller the cooldown
            InCombat = true;
            lastAttackTime = Time.time;
        }
    }

    public void AttackHitEvent()
    {
        if (opponentStats.currentHealth > 0)
        {
            opponentStats.TakeDamage(myStats.damage.GetValue());
            opponentAnimator.SetTrigger("takenDamage");
        }
		else if (opponentStats.currentHealth <= 0)
		{
			InCombat = false;
		}
	}
}
