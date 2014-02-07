using UnityEngine;
using System.Collections;

public class ItemList {
	public static Item[] items;

	public static void Add(Item item) {
		items[items.Length - 1] = item;

		Debug.Log(item);
	}
}
