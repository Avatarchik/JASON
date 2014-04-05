using UnityEngine;
using System;
using SGUI;

public class BasicTutorial:Singleton<BasicTutorial> {
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

		if(!GameData.Instance.tutorialFinished)
			StartTutorial();
	}

	/** Start the tutorial */
	public void StartTutorial() {
		if(!canStart)
			return;

		stage = TutorialStage.Movement;

		started = true;

		StartStage();
	}

	/** Start the stage */
	public void StartStage() {
		tooltip.Activated = true;
		labels[(int)stage - 1].Activated = true;

		StartCoroutine(labels[(int)stage - 1].Write());
	}

	/** Go to the next stage */
	public void NextStage() {
		tooltip.Activated = false;
		labels[(int)stage - 1].Activated = false;

		stage++;
	}

	/** Stop the tutorial */
	public void StopTutorial() {
		Debug.Log("Fap");
		tooltip.Activated = false;
		labels[(int)stage - 1].Activated = false;

		stage = TutorialStage.None;

		started = false;

		GameData.Instance.tutorialFinished = true;
	}

	/** Get the labels */
	public SGUISlowWriteLabel[] Labels {
		get { return labels; }
	}

	/** Get the stage */
	public TutorialStage Stage {
		get { return stage; }
	}

	/** Whether the tutorial has been started */
	public bool Started {
		get { return started; }
	}
}
