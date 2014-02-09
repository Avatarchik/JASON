using UnityEngine;

public class Item {
	public enum ItemType {
		Equipable,
		Power,
		Special
	}

	public ItemType itemType;
	public string itemName;	
	
	public Item() { }
	
	public Item(ItemType itemType, string itemName) {
		this.itemType = itemType;
		this.itemName = itemName;
	}
}
