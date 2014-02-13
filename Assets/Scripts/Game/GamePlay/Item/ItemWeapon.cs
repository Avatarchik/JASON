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
	
	public ItemWeapon(string itemName, EquipableElement element, ItemEquipableStats stats, GameObject model, WeaponType weaponType):base(itemName, EquipableType.Weapon, element, stats, model) {
		this.weaponType = weaponType;
	}

	public WeaponType TypeWeapon {
		set { weaponType = value; }
		get { return weaponType; }
	}
}
