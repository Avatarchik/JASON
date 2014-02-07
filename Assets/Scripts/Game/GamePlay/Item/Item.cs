using UnityEngine;
using System;

[Serializable]
public class Item {
	public enum ItemType {
		Equipable,
		Weapon,
		Power,
		Special
	}

	public string itemName;
	public ItemType itemType;
}
