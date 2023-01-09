using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    CharacterAnimator characterAnimator;
    public GameObject dropObjectPrefab;

    void Start()
    {
		characterAnimator = GetComponent<CharacterAnimator>();
	}

    public override void Die()
    {
        base.Die();
        StartCoroutine(DeathAnim());

        // Drop loot
        DropLoot();
    }

    IEnumerator DeathAnim()
    {
		characterAnimator.OnDeath();
		yield return new WaitForSeconds(3f);
		Destroy(gameObject);
	}

    void DropLoot()
    {
        // Create an instance of the object prefab at the enemy's location
        GameObject dropObject = Instantiate(dropObjectPrefab, transform.position, transform.rotation);

        Rigidbody rigidbody = dropObject.GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
    }
}
