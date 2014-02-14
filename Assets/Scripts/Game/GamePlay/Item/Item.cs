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
	[SerializeField] private string itemDescription;
	
	public Item() { }

	public Item(ItemType itemType, string itemName, string itemDescription) {
		this.itemType = itemType;
		this.itemName = itemName;
		this.itemDescription = itemDescription;
	}

	public ItemType Type {
		set { itemType = value; }
		get { return itemType; }
	}

	public string Name {
		set { itemName = value; }
		get { return itemName; }
	}

	public string Description {
		set { itemDescription = value; }
		get { return itemDescription; }
	}
}
