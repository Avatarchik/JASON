using UnityEngine;

public class ItemWeapon:ItemEquipable {
	public enum WeaponType {
		Sword,
		Spear,
		Axe
	}

	public WeaponType weaponType;
	
	public ItemWeapon() { }
	
	public ItemWeapon(string itemName, EquipableElement element, ItemEquipableStats stats, GameObject model, WeaponType weaponType):base(itemName, EquipableType.Weapon, element, stats, model) {
		this.weaponType = weaponType;
	}
}
