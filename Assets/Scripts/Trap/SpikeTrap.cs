using UnityEngine;
using System.Collections;

public class SpikeTrap:TimedTrap {
	[SerializeField] private GameObject spikes;
	
	[SerializeField] private bool precise;
	
	private bool shuttingDown;

	void Start() {
		StartCoroutine("Cycle");
	}

	void Update() {
		if(shuttingDown) {
			activated = false;
			shuttingDown = false;
		}
	}

	void OnTriggerEnter(Collider col) {
		if(!col.CompareTag("Player") && !col.CompareTag("Enemy"))
			return;
				
		entity = col.GetComponent<Entity>();

		StartCoroutine("DamageEntity");
	}

	void OnTriggerExit(Collider col) {
		if(!col.CompareTag("Player") && !col.CompareTag("Enemy"))
			return;
		
		StopCoroutine("DamageEntity");

		entity = null;
	}
	
	/** <summary>Cycle between the on and off state</summary> */
	private IEnumerator Cycle() {
		yield return new WaitForSeconds(startDelay);

		while(true) {
			float random = 1;

			if(!precise)
				random = Random.Range(2.5f, 7.5f);

			yield return new WaitForSeconds(delay * random);

			activated = true;

			yield return new WaitForSeconds(duration);

			shuttingDown = true;

			while(shuttingDown)
				yield return new WaitForSeconds(0.01f);
		}
	}
}
