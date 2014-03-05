using UnityEngine;
using System.Collections;

public class Destructable:MonoBehaviour {
	[SerializeField] protected int health;

	protected ParticleSystem effect;
	
	protected bool destroyed;

	protected virtual void Start() {
		effect = GetComponentInChildren<ParticleSystem>();
	}

	public virtual void Damage(int amount) {
		health -= amount;

		if(health <= 0)
			Destroy();
	}

	public virtual void Destroy() {
		if(effect != null)
			effect.Play();

		destroyed = true;
	}

	protected IEnumerator DestroyOnEffectFinish() {
		while(effect.isPlaying) {
			yield return new WaitForSeconds(0.01f);
		}

		Destroy(gameObject);
	}

	public bool Destroyed { get { return destroyed; } }
}
