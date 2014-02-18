using UnityEngine;
using System;

public class ItemWeapon:MonoBehaviour {
	public WeaponData dataWeapon;
}

[Serializable]
public class WeaponData:EquipableData {
	public enum WeaponType {
		Sword,
		Spear,
		Axe
	}
	
	public WeaponType weaponType;
}