using UnityEngine;
using System;

[Serializable]
public class ItemWeapon:ItemEquipable {
	public static ItemWeapon weapon = new ItemWeapon();

	public enum WeaponType {
		Sword,
		Spear,
		Axe
	}

	[SerializeField] private WeaponType weaponType;
	
	public ItemWeapon() { }
	
	public ItemWeapon(string itemName, string itemDescription, EquipableElement element, EquipableRarity rarity, ItemEquipableStats stats, GameObject model, WeaponType weaponType):base(itemName, itemDescription, EquipableType.Weapon, element, rarity, stats, model) {
		this.weaponType = weaponType;
	}

	public WeaponType TypeWeapon {
		set { weaponType = value; }
		get { return weaponType; }
	}
}
