using UnityEngine;
using System.Collections;

public class Vase:Destructable {
	[SerializeField] private GameObject destroyedModel;

	/** Called when the destructable has been destroyed */
	public override void Destroy() {
		GameObject model = Instantiate(destroyedModel, transform.position, new Quaternion(0, transform.rotation.y, transform.rotation.z, transform.rotation.w)) as GameObject;
		model.transform.parent = transform.parent;

		GetComponentInChildren<MeshRenderer>().enabled = false;
		GetComponent<BoxCollider>().enabled = false;

		StartCoroutine(DestroyOnEffectFinish());
	}
}
