using UnityEngine;
using System.Collections;

public class ArrowTrap : MonoBehaviour {
	public Arrow arrow;
	public Transform shotposition;
	public float delay;
	// Use this for initialization
	void Start () {
		StartCoroutine(Shoot(delay));
	}

	IEnumerator Shoot(float d){
		while(true){
			arrow.ShootArrow(shotposition);
			yield return new WaitForSeconds(d);
		}
	}
}
