using UnityEngine;
using System.Collections;

public class DestroyableObject:MonoBehaviour {

	public int health;
	public ParticleSystem effect;

	void OnTriggerEnter(Collider col){
		if(col.name == "Weapon Collision"){
			health--;

			if(health <= 0) {
				Destroy(gameObject);

				if(effect != null)
					effect.Play();
			}
		}
	}
}
