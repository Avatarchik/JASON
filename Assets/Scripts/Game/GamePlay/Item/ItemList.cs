using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class ItemList:Singleton<ItemList> {
	[SerializeField] private List<EquipableData> equipableItems = new List<EquipableData>();
	[SerializeField] private List<WeaponData> weaponItems = new List<WeaponData>();
	[SerializeField] private List<PowerData> powerItems = new List<PowerData>();
	[SerializeField] private List<SpecialData> specialItems = new List<SpecialData>();

	public List<EquipableData> EquipableItems { get { return equipableItems; } }
	public List<WeaponData> WeaponItems { get { return weaponItems; } }
	public List<PowerData> PowerItems { get { return powerItems; } }
	public List<SpecialData> SpecialItems { get { return specialItems; } }
}
