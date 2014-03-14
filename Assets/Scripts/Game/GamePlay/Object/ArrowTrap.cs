using UnityEngine;
using System.Collections;

public class ArrowTrap:MonoBehaviour {
	[SerializeField] private float delay;

	private Arrow arrow;

	void Start () {
		arrow = GetComponentInChildren<Arrow>();

		if(arrow == null)
			throw new System.NullReferenceException("No child found with the 'Arrow' script");

		StartCoroutine(FireArrow(delay));
	}

	/** Fire an arrow */
	private IEnumerator FireArrow(float delay) {
		while(true) {
			arrow.Fire(this);

			yield return new WaitForSeconds(delay);
		}
	}
}
