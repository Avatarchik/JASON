using UnityEngine;
using System.Collections;

public class ArrowShooter:MonoBehaviour {
	[SerializeField] private float delay;

	private Arrow arrow;

	void Start() {
		arrow = GetComponentInChildren<Arrow>();

		StartCoroutine("Shoot");
	}

	/** <summary>Shoot an arrow each cycle</summary> */
	private IEnumerator Shoot() {
		while(true) {
			arrow.Fire(transform);

			yield return new WaitForSeconds(delay);
		}
	}
}
