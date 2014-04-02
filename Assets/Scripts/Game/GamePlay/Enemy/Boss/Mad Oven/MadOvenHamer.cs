using UnityEngine;
using System.Collections;

public class MadOvenHamer:Boss {
	private bool attacking;

	void OnCollisionEnter(Collision collision) {
		if(!attacking)
			return;

		if(collider.gameObject.CompareTag("Player"))
			collider.GetComponent<Player>().Damage(data.AttackDamage, 0, false);
	}

	public void StartAttack() {
		StartCoroutine(Attack());
	}

	public override void Damage(int amount) {
		if(data.Health <= 0)
			return;

		data.Health -= amount;
		DisplayCombatText(amount.ToString(), Color.red, 0.4f);

		Debug.Log("Hamer: " + data.Health);
	}

	private IEnumerator Attack() {
		while(true) {
			yield return new WaitForSeconds(data.AttackDelay / 2);

			attacking = true;

			yield return new WaitForSeconds(data.AttackDelay / 2);

			attacking = false;
		}
	}
}
