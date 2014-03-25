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

	[SerializeField] private SGUIButton[] buttons;

	private Player player;

	private Bars activeBar;

	private Rect originalBarWidth;

	private int hitTimer;

	private bool playerInstanceFound;

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

		if(player == null)
			throw new System.NullReferenceException("No Game Object found in the scene with the 'Player' tag");

		originalBarWidth = innerBars[0].Bounds;

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

		UpdateHealthBar();

		if(buttons[0].Toggle)
            player.PlayerCombat.Defend(!player.PlayerCombat.Defending);

		if(player.AttachedThrowable == null) {
			buttons[1].Activated = false;
		} else {
			buttons[1].Activated = true;

			if(buttons[1].Click)
				player.ThrowObject();
		}
	}

	private void UpdateHealthBar() {
		if(player.Hit) {
			SwitchBar(Bars.Damage);
		} else if(player.PlayerCombat.Defending) {
			SwitchBar(Bars.Shield);
		} else {
			SwitchBar(Bars.Health);
		}

		Rect bounds = originalBarWidth;
		bounds.width = bounds.width / PlayerData.Instance.InitialHealth * PlayerData.Instance.Health;

		innerBars[(int)activeBar].Bounds = bounds;

		if(player.PlayerCombat.Attacking) {
			hitTimer -= 10;
		} else {
			hitTimer = 300;
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
		
		foreach(SGUIButton button in buttons)
			button.Create();
	}
}