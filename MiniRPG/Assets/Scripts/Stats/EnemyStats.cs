using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    CharacterAnimator characterAnimator;

    void Start()
    {
		characterAnimator = GetComponent<CharacterAnimator>();
	}

    public override void Die()
    {
        base.Die();
        StartCoroutine(DeathAnim());
        
        // Drop loot

    }

    IEnumerator DeathAnim()
    {
		characterAnimator.OnDeath();
		yield return new WaitForSeconds(3f);
		Destroy(gameObject);
	}
}
