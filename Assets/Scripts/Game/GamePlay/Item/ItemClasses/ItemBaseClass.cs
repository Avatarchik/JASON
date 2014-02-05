using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemBaseClass {
	public enum ItemType {
		Weapon,
		Shield,
		Helmet,
		Armor,
		Leggings,
		Boots,
		Power,
		Special
	};

	public string name;
	public ItemType itemType;
}
