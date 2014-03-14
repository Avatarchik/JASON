using UnityEngine;
using System.Collections.Generic;

public class Item:MonoBehaviour {
	private enum EquipableType {
		Helmet,
		Chest,
		Legs,
		Weapon,
		Shield
	}

	private static Item[] itemList = new Item[255];

	private static int nextItemId = 0;
	
	[SerializeField] private EquipableType type;
	[SerializeField] private GameObject model;

	private int itemId;

	void Start() {
		itemId = nextItemId++;

		itemList[itemId] = this;
	}
}