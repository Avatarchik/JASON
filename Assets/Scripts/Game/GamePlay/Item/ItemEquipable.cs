using UnityEngine;
using System;

[Serializable]
public class ItemEquipable:Item {
	public static ItemEquipable equipable = new ItemEquipable();

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

	[SerializeField] private EquipableType equipableType;
	[SerializeField] private EquipableElement element;
	[SerializeField] private EquipableRarity rarity;
	[SerializeField] private GameObject model;
	[SerializeField] private ItemEquipableStats stats;
	
	public ItemEquipable() {
		stats = ItemEquipableStats.equipableStats;
	}
	
	public ItemEquipable(string itemName, string itemDescription, EquipableType equipableType, EquipableElement element, EquipableRarity rarity, ItemEquipableStats stats, GameObject model):base(Item.ItemType.Equipable, itemName, itemDescription) {
		this.equipableType = equipableType;
		this.element = element;
		this.rarity = rarity;
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

	public EquipableRarity Rarity {
		set { rarity = value; }
		get { return rarity; }
	}

	public GameObject Model {
		set { model = value; }
		get { return model; }
	}

	public ItemEquipableStats Stats {
		set { stats = value; }
		get { return stats; }
	}

	public static Color RarityColor(EquipableRarity rarity) {
		switch(rarity) {
		case EquipableRarity.Common:
			return new Color(1.0f, 1.0f, 1.0f);
		case EquipableRarity.Rare:
			return new Color(0.0f, 0.44f, 0.87f);
		case EquipableRarity.Epic:
			return new Color(0.64f, 0.21f, 0.93f);
		case EquipableRarity.Legendary:
			return new Color(1.0f, 0.5f, 0.0f);
		default:
			return new Color(1.0f, 1.0f, 1.0f);
		}
	}

	[Serializable]
	public class ItemEquipableStats {
		public static ItemEquipableStats equipableStats = new ItemEquipableStats();
	
		[SerializeField] private int speed;
		[SerializeField] private int defence;
		[SerializeField] private int damage;
		[SerializeField] private int storePrice;
		[SerializeField] private byte durability;
		
		public ItemEquipableStats() { }
		
		public ItemEquipableStats(int speed, int defence, int damage, int storePrice) {
			this.speed = speed;
			this.defence = defence;
			this.damage = damage;
			this.storePrice = storePrice;
			this.durability = 255;
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

		public byte Durability { get { return durability; } }
	}
}

