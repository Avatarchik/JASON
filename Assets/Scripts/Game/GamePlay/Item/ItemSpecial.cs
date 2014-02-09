using UnityEngine;

public class ItemSpecial:Item {
	public int id;
	
	public ItemSpecial() { }

	public ItemSpecial(string itemName, int id):base(Item.ItemType.Special, itemName) {
		this.id = id;
	}
}
