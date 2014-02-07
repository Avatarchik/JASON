using UnityEngine;
using System;

[Serializable]
public class InventoryContent {
	[SerializeField] private Item[] items;

	public int AddItem(Item item) {
		for(int i = 0; i < items.Length; i++) {
			if(items[i].Equals(null)) {
				items[i] = item;
				return i;
			}
		}

		return -1;
	}

	public Item GetItem(Type type) {
		foreach(Item item in items) {
			if(item.GetType() == type) {
				return item;
			}
		}

		return null;
	}

	public int IndexOf(Item item) {
		for(int i = 0; i < items.Length; i++) {
			if(items[i] == item) {
				return i;
			}
		}
		
		return -1;
	}

	public void RemoveItemAt(int index) {
		items[index] = null;
	}

	public Item[] Items { get { return items; } }
}
