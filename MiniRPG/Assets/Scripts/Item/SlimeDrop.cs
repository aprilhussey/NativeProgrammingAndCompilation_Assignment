using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/SlimeDrop")]
public class SlimeDrop : Item
{
	PlayerStats playerStats;

	public int healAmount = 2;

	public override void Use()
	{
		base.Use();

		playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

		if (playerStats != null)
		{
			playerStats.Heal(healAmount);
		}
	}
}
