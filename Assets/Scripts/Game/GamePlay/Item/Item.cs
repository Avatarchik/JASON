using UnityEngine;
using System;

[Serializable]
public class Item {
	public static Item item = new Item();

	public enum ItemType {
		Equipable,
		Power,
		Special
	}

	[SerializeField] private ItemType itemType;
	[SerializeField] private string itemName;
	
	public Item() { }

	public Item(ItemType itemType, string itemName) {
		this.itemType = itemType;
		this.itemName = itemName;
	}

	public ItemType Type {
		set { itemType = value; }
		get { return itemType; }
	}

	public string Name {
		set { itemName = value; }
		get { return itemName; }
	}
}
