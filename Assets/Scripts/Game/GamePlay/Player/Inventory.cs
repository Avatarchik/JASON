using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory:MonoBehaviour {
	[SerializeField] private List<ItemSpecial> specials;

	[SerializeField] private ItemEquipable[] helmets;
	[SerializeField] private ItemEquipable[] chests;
	[SerializeField] private ItemEquipable[] legs;
	[SerializeField] private ItemEquipable[] shields;
	[SerializeField] private ItemWeapon[] weapons;
	[SerializeField] private ItemPower power;

	/** Pickup an equipable item */
	public int PickupEquipable(ItemEquipable item) {
		int i = -1;

		switch(item.data.equipableType) {
		case EquipableData.EquipableType.Helmet:
			i = AddHelmet(item);
			break;
		case EquipableData.EquipableType.Chest:
			i = AddChest(item);
			break;
		case EquipableData.EquipableType.Legs:
			i = AddLegs(item);
			break;
		case EquipableData.EquipableType.Shield:
			i = AddShield(item);
			break;
		}

		item.collider.enabled = false;
		item.renderer.enabled = false;

		return i;
	}

	/** Pickup a weapon */
	public int PickupWeapon(ItemWeapon weapon) {
		for(int i = 0; i < weapons.Length; i++) {
			if(weapons[i] != null) {
				weapons[i] = weapon;
				return i;
			}
		}

		return -1;
	}

	/** Switch the current power with the parameter */
	public void PickupPower(ItemPower power) {
		this.power = power;
	}

	/** Add a special item to the inventory */
	public int PickupSpecial(ItemSpecial special) {
		specials.Add(special);

		return specials.IndexOf(special);
	}

	/** Try to add a helmet to the inventort */
	private int AddHelmet(ItemEquipable item) {
		for(int i = 0; i < helmets.Length; i++) {
			if(helmets[i] == null) {
				helmets[i] = item;
				return i;
			}
		}

		return -1;
	}

	/** Try to add a chestplate to the inventory */
	private int AddChest(ItemEquipable item) {
		for(int i = 0; i < chests.Length; i++) {
			if(chests[i] == null) {
				chests[i] = item;
				return i;
			}
		}

		return -1;
	}

	/** Try to add a legplate to the inventory */
	private int AddLegs(ItemEquipable item) {
		for(int i = 0; i < legs.Length; i++) {
			if(legs[i] == null) {
				legs[i] = item;
				return i;
			}
		}

		return -1;
	}

	/** Try to add a shield to the inventory */
	private int AddShield(ItemEquipable item) {
		for(int i = 0; i < shields.Length; i++) {
			if(shields[i] == null) {
				shields[i] = item;
				return i;
			}
		}

		return -1;
	}

	public ItemEquipable[] Helmet { get { return helmets; } }
	public ItemEquipable[] Chests { get { return chests; } }
	public ItemEquipable[] Legs { get { return legs; } }
	public ItemEquipable[] Shields { get { return shields; } }
	public ItemWeapon[] Weapons { get { return weapons; } }
	public ItemPower Power { get { return power; } }
}
