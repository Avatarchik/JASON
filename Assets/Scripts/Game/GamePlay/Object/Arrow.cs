using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
	bool isTraveling;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(isTraveling){
		transform.Translate(Vector3.forward * 0.8f);
		}
	}

	public void ShootArrow(Transform arrowTrap){
		transform.position = arrowTrap.transform.position;
		transform.rotation = arrowTrap.transform.rotation;
		isTraveling = true;
		collider.enabled = true;
		transform.parent = null;
		renderer.enabled = true;
	}

	void OnCollisionEnter(Collision coll){
		isTraveling = false;
		collider.enabled = false;
		transform.parent = coll.gameObject.transform;
		if(coll.gameObject.name == "Player"){
			coll.gameObject.GetComponent<Player>().Damage(1);
			renderer.enabled = false;
		}
	}
}
