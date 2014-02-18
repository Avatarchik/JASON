using UnityEngine;
using System;

public class ItemEquipable:MonoBehaviour {
	public EquipableData data;

	void Start() {

	}
}

[Serializable]
public class EquipableData:Item {
	[Serializable]
	public class Stats {
		public int speed;
		public int defence;
		public int damage;
		public int storePrice;
		public byte durability;
	}
	
	public enum EquipableType {
		Helmet,
		Chest,
		Legs,
		Weapon,
		Shield
	}
	
	public enum EquipableElement {
		Normal,
		Water,
		Earth,
		Fire
	}
	
	public enum EquipableRarity {
		Common,
		Rare,
		Epic,
		Legendary
	}
	
	public EquipableType equipableType;
	public EquipableElement element;
	public EquipableRarity rarity;
	public GameObject model;
	public Stats stats;
	
	public EquipableData() {
		stats = new Stats();
	}
}