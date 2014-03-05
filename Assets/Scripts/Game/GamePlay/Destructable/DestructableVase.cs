using UnityEngine;
using System.Collections;

public class DestructableVase:Destructable {
	[SerializeField] private GameObject destroyedModel;

	public override void Destroy() {
		base.Destroy();

		EquipableData item = ItemList.Instance.EquipableItems[0];
		
		GameObject go = (GameObject)Instantiate(item.model, transform.position, Quaternion.identity);
		ItemEquipable equipable = go.AddComponent<ItemEquipable>();
		go.AddComponent<BoxCollider>().isTrigger = true;
		
		equipable.data = item;
		equipable.tag = "Item Equipable";

		Quaternion rotation = transform.rotation;
		rotation.x = 0;

		GameObject destroyed = Instantiate(destroyedModel, transform.position, rotation) as GameObject;
		destroyed.transform.parent = transform.parent;

		GetComponentInChildren<Renderer>().enabled = false;
		GetComponent<BoxCollider>().enabled = false;

		StartCoroutine("DestroyOnEffectFinish");
	}
}
