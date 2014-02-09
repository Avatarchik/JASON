using UnityEngine;

public class ItemEquipable:Item {
	public class ItemEquipableStats {
		public int speed;
		public int defence;
		public int damage;
		public int storePrice;
		
		public ItemEquipableStats() { }
		
		public ItemEquipableStats(int speed, int defence, int damage, int storePrice) {
			this.speed = speed;
			this.defence = defence;
			this.damage = damage;
			this.storePrice = storePrice;
		}
	}

	public enum EquipableType {
		Helmet,
		Chest,
		Legs,
		Weapon,
		Shield
	};
	
	public enum EquipableElement {
		Normal,
		Water,
		Earth,
		Fire
	};

	public EquipableType equipableType;
	public EquipableElement element;
	public ItemEquipableStats stats;
	public GameObject model;
	
	public ItemEquipable() { }
	
	public ItemEquipable(string itemName, EquipableType equipableType, EquipableElement element, ItemEquipableStats stats, GameObject model):base(Item.ItemType.Equipable, itemName) {
		this.equipableType = equipableType;
		this.stats = stats;
		this.model = model;
	}
}

