using UnityEngine;
using System;
using System.Collections;
using SGUI;

public class GameHUD:GUIBehaviour {
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

	private bool playerInstanceFound;

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

		if(player == null)
			throw new System.NullReferenceException("No Game Object found in the scene with the 'Player' tag");

		StartCoroutine(WaitForGlobalManager());
	}
	
	void Update() {
		if(!playerInstanceFound) {
			if(GameObject.Find("Global Managers") == null) {
				return;
			} else {
				playerInstanceFound = true;
			}
		}

		if(player.PlayerCombat.Defending) {
			SwitchBar(Bars.Shield);
		} else if(player.Hit) {
			SwitchBar(Bars.Damage);
		} else {
			SwitchBar(Bars.Health);
		}
		
		Rect bounds = innerBars[(int)activeBar].Bounds;
		
		bounds.width = innerBars[0].Bounds.width / PlayerData.Instance.InitialHealth * PlayerData.Instance.Health;
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
				player.ThrowObject();
			}
		}
	}

	/** Switch the active inner bar texture */
	private void SwitchBar(Bars newBar) {
		if(activeBar == newBar)
			return;

		innerBars[(int)activeBar].Activated = false;
		innerBars[(int)newBar].Activated = true;

		activeBar = newBar;
	}

	/** Wait until the Global Manager has been loaded */
	private IEnumerator WaitForGlobalManager() {
		while(GameObject.FindGameObjectWithTag("Global Manager") == null)
			yield return new WaitForSeconds(0.3f);

		outerBar.Create();
		
		foreach(SGUITexture texture in innerBars)
			texture.Create();
		
		foreach(SGUITextureButton button in buttons)
			button.Create();
	}
}