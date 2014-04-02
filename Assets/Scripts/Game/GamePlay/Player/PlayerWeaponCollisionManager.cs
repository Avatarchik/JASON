using UnityEngine;
using System.Collections.Generic;

public class PlayerWeaponCollisionManager:MonoBehaviour {
	private List<Collider> colliders = new List<Collider>();

	void OnTriggerEnter(Collider collider) {
		if(collider.CompareTag("Boss") || collider.CompareTag("Enemy"))
			if(!colliders.Contains(collider))
				colliders.Add(collider);
	}

	void OnTriggerExit(Collider collider) {
		if(colliders.Contains(collider))
			colliders.Remove(collider);
	}

	public Collider[] Colliders {
		get { return colliders.ToArray(); }
	}
}
