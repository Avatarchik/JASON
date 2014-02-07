using UnityEngine;

public class ItemWeapon:ItemEquipable {
	public enum WeaponType {
		Sword,
		Spear,
		Axe
	}

	public WeaponType weaponType;

	public int damage;
	public int speed;

	public ItemWeapon(GameObject itemModel, ItemWeapon.WeaponType weaponType, int storePrice, int damage, int speed):base(EquipableType.Weapon, itemModel, storePrice) {
		this.weaponType = weaponType;
		this.damage = damage;
		this.speed = speed;
	}
}
