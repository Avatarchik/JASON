using UnityEngine;
using System.Collections;

public class DestroyableObject:MonoBehaviour {
	[SerializeField] private ParticleSystem effect;

	[SerializeField] private int health;
	
	void OnTriggerEnter(Collider col) {
		if(col.name == "WeaponCollision") {
			health--;

			if(health <= 0) {
				Destroy(gameObject);

				if(effect != null) {
					// TODO: Do effect
				}
			}
		}
	}
}
