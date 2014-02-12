using UnityEngine;
using System;

[Serializable]
public class ItemEquipable:Item {
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

	[SerializeField] private EquipableType equipableType;
	[SerializeField] private EquipableElement element;
	[SerializeField] private GameObject model;
	[SerializeField] private ItemEquipableStats stats;
	
	public ItemEquipable() { }
	
	public ItemEquipable(string itemName, EquipableType equipableType, EquipableElement element, ItemEquipableStats stats, GameObject model):base(Item.ItemType.Equipable, itemName) {
		this.equipableType = equipableType;
		this.element = element;
		this.stats = stats;
		this.model = model;
	}

	public EquipableType TypeEquipable {
		set { equipableType = value; }
		get { return equipableType; }
	}

	public EquipableElement Element {
		set { element = value; }
		get { return element; }
	}

	public GameObject Model {
		set { model = value; }
		get { return model; }
	}

	public ItemEquipableStats Stats {
		set { stats = value; }
		get { return stats; }
	}

	[Serializable]
	public class ItemEquipableStats {
		[SerializeField] private int speed;
		[SerializeField] private int defence;
		[SerializeField] private int damage;
		[SerializeField] private int storePrice;
		
		public ItemEquipableStats() { }
		
		public ItemEquipableStats(int speed, int defence, int damage, int storePrice) {
			this.speed = speed;
			this.defence = defence;
			this.damage = damage;
			this.storePrice = storePrice;
		}

		public int Speed {
			set { speed = value; }
			get { return speed; }
		}

		public int Defence {
			set { defence = value; }
			get { return defence; }
		}

		public int Damage {
			set { damage = value; }
			get { return damage; }
		}

		public int StorePrice {
			set { storePrice = value; }
			get { return storePrice; }
		}
	}
}

