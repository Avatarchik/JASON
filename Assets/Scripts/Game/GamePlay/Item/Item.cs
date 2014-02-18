using UnityEngine;
using System;

[Serializable]
public class Item {
	public enum ItemType {
		Equipable,
		Power,
		Special
	}

	public ItemType itemType;
	public string itemName;
	public string itemDescription;
}
