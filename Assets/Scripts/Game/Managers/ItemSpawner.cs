using UnityEngine;
using System.Collections;

public class ItemSpawner:MonoBehaviour {
	void Start() {
		EquipableData w = ItemList.Instance.EquipableItems[0];

		GameObject go = (GameObject)Instantiate(w.model, Vector3.zero, Quaternion.identity);
		ItemEquipable equipable = go.AddComponent<ItemEquipable>();

		equipable.data = w;
	}
}
