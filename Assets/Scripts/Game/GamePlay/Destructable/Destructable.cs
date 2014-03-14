using UnityEngine;
using System.Collections;

public class Destructable:MonoBehaviour {
	[SerializeField] protected int health;

	protected ParticleSystem effect;

	private bool isDestroyed;

	protected virtual void Start() {
		effect = GetComponentInChildren<ParticleSystem>();
	}

	/** Damage the destructable */
	public virtual void Damage(int amount) {
		health -= amount;

		if(health <= 0)
			Destroy();
	}

	/** Called when the destructable has been destroyed */
	public virtual void Destroy() {
		isDestroyed = true;
	}

	/** Destroy the object once the effect has ended */
	protected IEnumerator DestroyOnEffectFinish() {
		if(effect != null) {
			effect.Play();

			while(effect.isPlaying)
				yield return new WaitForSeconds(0.01f);
		}

		Destroy(gameObject);
	}

	/** Get wheter or not the destructable object has been destroyed */
	public bool IsDestroyed {
		get { return isDestroyed; }
	}
}
