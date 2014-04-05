using UnityEngine;

public class Player:Entity {
	[SerializeField] private Transform keyPosition;
	[SerializeField] private Transform pushableBlockPosition;
	[SerializeField] private Transform brazierPosition;

	[SerializeField] private Renderer[] shieldRenderers;
	[SerializeField] private Renderer swordRenderer;

	private PlayerMovement playerMovement;

	private IInteractable interactable;

	void Start() {
		playerMovement = GetComponent<PlayerMovement>();
	}

	void Update() {
		if(interactable != null) {
			if(interactable.GetInteractableType() == InteractableType.PushableBlock) {
				animator.SetBool("IsMovingBlock", true);
				DetermineAnimationDirection();
			}
		}
	}

	/** <summary>Pick up an interactable object</summary>
	 * <param name="target">The transform of the object</param> */
	public void Pickup(Transform target) {
		interactable = target.GetComponent(typeof(IInteractable)) as IInteractable;

		swordRenderer.enabled = false;

		foreach(Renderer shieldRenderer in shieldRenderers)
			shieldRenderer.enabled = false;

		switch(interactable.GetInteractableType()) {
		case InteractableType.PushableBlock:
			interactable.Pickup(pushableBlockPosition);
			break;
		case InteractableType.Key:
			interactable.Pickup(keyPosition);
			animator.SetBool("IsHolding", true);
			break;
		case InteractableType.Brazier:
			interactable.Pickup(brazierPosition);
			animator.SetBool("IsHolding", true);
			break;
		}
	}

	public void Drop(bool throwObject) {
		if(interactable == null)
			return;

		switch(interactable.GetInteractableType()) {
		case InteractableType.PushableBlock:
			AudioManager.Instance.SetAudio(AudioManager.AudioFiles.BlockMove, false);
			animator.SetBool("IsMovingBlock", false);
			break;
		case InteractableType.Key:
		case InteractableType.Brazier:
			animator.SetBool("IsHolding", false);
			break;
		}

		if(throwObject) {
			interactable.Throw();
		} else {
			interactable.Drop();
		}

		interactable = null;
	}

	/** <summary>Determine which animation to play depending on the movement direction</summary> */
	private void DetermineAnimationDirection() {
		Vector3 moveDirection = playerMovement.MoveDirection;

		float highestDir = Mathf.Max(moveDirection.x, moveDirection.z);
		float lowestDir = Mathf.Min(moveDirection.x, moveDirection.z);

		if(moveDirection == Vector3.zero) {
			AudioManager.Instance.SetAudio(AudioManager.AudioFiles.BlockMove, false);
			animator.SetInteger("MoveDirection", 0);
		} else {
			if(Mathf.Abs(highestDir) > Mathf.Abs(lowestDir)) {
				if(highestDir == moveDirection.x) {
					AudioManager.Instance.SetAudio(AudioManager.AudioFiles.BlockMove, true);
					animator.SetInteger("MoveDirection", 4);
				} else if(highestDir == moveDirection.z) {
					animator.SetInteger("MoveDirection", 1);
					AudioManager.Instance.SetAudio(AudioManager.AudioFiles.BlockMove, true);
				}
			} else {
				if(lowestDir == moveDirection.x) {
					AudioManager.Instance.SetAudio(AudioManager.AudioFiles.BlockMove, true);
					animator.SetInteger("MoveDirection", 3);
				} else if(lowestDir == moveDirection.z) {
					AudioManager.Instance.SetAudio(AudioManager.AudioFiles.BlockMove, true);
					animator.SetInteger("MoveDirection", 2);
				}
			}
		}
	}
	
	public IInteractable Interactable {
		set { interactable = value; }
		get { return interactable; }
	}
}
