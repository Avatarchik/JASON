using UnityEngine;
using System.Collections;

public class DestroyableObject : MonoBehaviour {

	public int health;
	public ParticleSystem effect;

	void OnTriggerEnter(Collider coll){
		if(coll.name == "WeaponCollision"){
			health--;
			if(health <= 0){
				Destroy(gameObject);
				if(effect != null){
					//DoEffect
				}
			}
		}
	}
}
