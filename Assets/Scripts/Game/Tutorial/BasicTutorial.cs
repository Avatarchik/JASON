using UnityEngine;
using System;
using SGUI;

public class BasicTutorial:Singleton<BasicTutorial>, ITutorial {
	public enum TutorialStage {
		None,
		Movement,
		Traps,
		BlockPickup,
		BlockPath,
		BlockDrop,
		PlayerTrigger,
		BlockTrigger,
		Key,
		KeyDoor,
		Boss
	};

	[SerializeField] private TutorialStage stage = TutorialStage.None;

	[SerializeField] private SGUITexture tooltip;

	[SerializeField] private SGUISlowWriteLabel[] labels;

	[SerializeField] private bool canStart;

	private bool started;

	void Start() {
		tooltip.Create();

		foreach(SGUISlowWriteLabel label in labels)
			label.Create();

		StartTutorial();
	}

	public void StartTutorial() {
		if(!canStart)
			return;

		stage = TutorialStage.Movement;

		started = true;

		StartStage();
	}

	public void StartStage() {
		tooltip.Activated = true;
		labels[(int)stage - 1].Activated = true;

		StartCoroutine(labels[(int)stage - 1].Write());
	}

	public void NextStage() {
		tooltip.Activated = false;
		labels[(int)stage - 1].Activated = false;

		stage++;
	}

	public void StopTutorial() {
		tooltip.Activated = false;
		labels[(int)stage - 1].Activated = false;

		stage = TutorialStage.None;

		started = false;
	}

	public SGUISlowWriteLabel[] Labels {
		get { return labels; }
	}

	public TutorialStage Stage {
		get { return stage; }
	}

	public bool Started {
		get { return started; }
	}
}
