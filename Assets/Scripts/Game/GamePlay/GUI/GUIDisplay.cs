using UnityEngine;
using System;
using System.Collections;
using SGUI;

public class GUIDisplay:GUIBehaviour {
	private enum Bars {
		Health = 0,
		Damage = 1,
		Shield = 2
	}

	[SerializeField] private SGUITexture[] innerBars;
	[SerializeField] private SGUITexture outerBar;

	[SerializeField] private SGUITextureButton[] buttons;

	private Player player;

	private Bars activeBar;

	private int timershit;
	private int clickCooldown;

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

		if(player == null)
			throw new System.NullReferenceException("No Game Object found in the scene with the 'Player' tag");
		
		outerBar.Create();
		
		foreach(SGUITexture texture in innerBars)
			texture.Create();
		
		foreach(SGUITextureButton button in buttons)
			button.Create();
	}
	
	void Update() {
		if(player.PlayerCombat.Defending) {
			SwitchBar(Bars.Shield);
		} else if(player.hit) {
			SwitchBar(Bars.Damage);
		} else {
			SwitchBar(Bars.Health);
		}
		
		Rect bounds = innerBars[(int)activeBar].Bounds;
		
		bounds.width = innerBars[0].Bounds.width / player.PlayerData.maxHealth * player.PlayerData.Health;
		innerBars[(int)activeBar].Bounds = bounds;
		
		if(player.PlayerCombat.Attacking) {
			timershit -= 10;
		} else {
			timershit = 300;
		}
		
		clickCooldown--;
	}
	
	protected override void OnGUI() {
		base.OnGUI();
	
		if(clickCooldown <= 0) {
			if(buttons[0].Click) {
				clickCooldown = 10;
				player.PlayerCombat.Defend(!player.PlayerCombat.Defending);
			}
			
			if(buttons[1].Click) {
				clickCooldown = 10;
				player.Pickup();
			}
		}
	}

	private void SwitchBar(Bars newBar) {
		if(activeBar == newBar)
			return;

		innerBars[(int)activeBar].Activated = false;
		innerBars[(int)newBar].Activated = true;

		activeBar = newBar;
	}
}