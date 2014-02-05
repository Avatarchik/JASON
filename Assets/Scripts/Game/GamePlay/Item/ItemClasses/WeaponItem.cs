using UnityEngine;
using System.Collections;

[System.Serializable]
public class WeaponItem : XPeriaPlayer   {
	public enum WeaponType{
		Sword,
		Dagger,
		Spear,
	};
	public WeaponType weaponType;
}

