using UnityEngine;
using System;

[Serializable]
public class ItemSpecial:Item {
	public static ItemSpecial special = new ItemSpecial();

	[SerializeField] private int id;
	
	public ItemSpecial() { }

	public ItemSpecial(string itemName, string itemDescription, int id):base(Item.ItemType.Special, itemName, itemDescription) {
		this.id = id;
	}

	public int Id {
		set { id = value; }
		get { return id; }
	}
}
