using UnityEngine;
using System.Collections;

public class TimedTrap:MonoBehaviour {
	[SerializeField] private float damageDelay;
	[SerializeField] protected float startDelay;
	[SerializeField] protected float duration;
	[SerializeField] protected float delay;
	[SerializeField] protected float damage;

	protected Entity entity;

	protected bool activated;

	/** <summary>Damage the player every cycle</summary> */
	protected IEnumerator DamageEntity() {
		while(true) {
			while(!activated)
				yield return new WaitForSeconds(0.01f);

			entity.Damage(damage);

			yield return new WaitForSeconds(damageDelay);
		}
	}
}
