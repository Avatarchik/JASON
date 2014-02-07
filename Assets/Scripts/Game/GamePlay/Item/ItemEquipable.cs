using UnityEngine;

public class ItemEquipable:Item {
	public enum EquipableType {
		Helmet,
		Chest,
		Legs,
		Weapon,
		Shield
	}

	public EquipableType equipableType;

	public GameObject itemModel;

	public int storePrice;

	public ItemEquipable(ItemEquipable.EquipableType equipableType, GameObject itemModel, int storePrice) {
		this.equipableType = equipableType;
		this.itemModel = itemModel;
		this.storePrice = storePrice;
	}
}

