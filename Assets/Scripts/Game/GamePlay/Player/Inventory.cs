using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory:Singleton<Inventory> {
	[SerializeField] private List<ItemSpecial> specials;

	[SerializeField] public ItemEquipable[] helmets;
	[SerializeField] public ItemEquipable[] chests;
	[SerializeField] public ItemEquipable[] legs;
	[SerializeField] public ItemEquipable[] shields;
	[SerializeField] public ItemWeapon[] weapons;
	[SerializeField] public ItemPower power;
	[SerializeField] private ItemList list;

	public GameObject inventoryObject;
	//TODO: Proper way of accesing current item
	public ItemWeapon currentWeapon;
	void Start(){
		//TESTING PURPOSE
		DEBUGADDITEMS();
	}
	void FixedUpdate(){
	}
	void DEBUGADDITEMS(){
		ItemEquipable item =   inventoryObject.AddComponent<ItemEquipable>();
		item.data = ItemList.Instance.EquipableItems[0];
		AddHelmet(item);
		ItemEquipable item1 =   inventoryObject.AddComponent<ItemEquipable>();
		item.data = ItemList.Instance.EquipableItems[1];
		AddChest(item1);
		ItemEquipable item2 =  inventoryObject.AddComponent<ItemEquipable>();
		item.data = ItemList.Instance.EquipableItems[2];
		AddLegs(item2);
		ItemEquipable item3 =   inventoryObject.AddComponent<ItemEquipable>();
		item.data = ItemList.Instance.EquipableItems[3];
		AddShield(item3);
	}
	/** Pickup an equipable item */
	public void PickupEquipable(ItemEquipable item) {
		switch(item.data.equipableType) {
		case EquipableData.EquipableType.Helmet:
			AddHelmet(item);
			break;
		case EquipableData.EquipableType.Chest:
			AddChest(item);
			break;
		case EquipableData.EquipableType.Legs:
			AddLegs(item);
			break;
		case EquipableData.EquipableType.Shield:
			AddShield(item);
			break;
		}

		item.collider.enabled = false;
		item.renderer.enabled = false;
	}

	/** Pickup a weapon */
	public void PickupWeapon(ItemWeapon weapon) {
		for(int i = 0; i < weapons.Length; i++) {
			if(weapons[i] != null) {
				weapons[i] = weapon;
				break;
			}
		}
	}

	/** Switch the current power with the parameter */
	public void PickupPower(ItemPower power) {
		this.power = power;
	}

	/** Add a special item to the inventory */
	public void PickupSpecial(ItemSpecial special) {
		specials.Add(special);
	}

	/** Try to add a helmet to the inventort */
	private void AddHelmet(ItemEquipable item) {
		for(int i = 0; i < helmets.Length; i++) {
			if(helmets[i] == null) {
				helmets[i] = item;
				break;
			}
		}
	}

	/** Try to add a chestplate to the inventory */
	private void AddChest(ItemEquipable item) {
		for(int i = 0; i < chests.Length; i++) {
			if(chests[i] == null) {
				chests[i] = item;
				break;
			}
		}
	}

	/** Try to add a legplate to the inventory */
	private void AddLegs(ItemEquipable item) {
		for(int i = 0; i < legs.Length; i++) {
			if(legs[i] == null) {
				legs[i] = item;
				break;
			}
		}
	}

	/** Try to add a shield to the inventory */
	private void AddShield(ItemEquipable item) {
		for(int i = 0; i < shields.Length; i++) {
			if(shields[i] == null) {
				shields[i] = item;
				break;
			}
		}
	}
}
