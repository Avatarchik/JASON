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
		if((target.GetComponent(typeof(IInteractable)) as IInteractable).IsLocked())
			return;

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

	/** <summary>Drop the currently held interactable object</summary>
	 * <param name="throwObject">Whether or not to throw the object</param> */
	public void Drop(bool throwObject) {
		if(interactable == null)
			return;

		swordRenderer.enabled = true;

		foreach(Renderer shieldRenderer in shieldRenderers)
			shieldRenderer.enabled = true;

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
			interactable.Throw(transform.forward);
		} else {
			interactable.Drop();
		}

		interactable = null;
	}
	
	/** <summary>Determine which animation to play depending on the movement direction</summary> */
	private void DetermineAnimationDirection() {
		float forward = Vector3.Dot(transform.forward, playerMovement.MoveDirection);
		float back = -forward;
		float right = Vector3.Dot(transform.right, playerMovement.MoveDirection);
		float left = Mathf.Abs(right);

		float highest = Mathf.Max(new float[] { forward, back, right, left });

		if(forward == 0 && back == 0 && right == 0 && left == 0) {
			AudioManager.Instance.SetAudio(AudioManager.AudioFiles.BlockMove, false);
			animator.SetInteger("MoveDirection", 0);
		} else {
			AudioManager.Instance.SetAudio(AudioManager.AudioFiles.BlockMove, true);

			if(forward == highest) {
				animator.SetInteger("MoveDirection", 1);
			} else if(back == highest) {
				animator.SetInteger("MoveDirection", 2);
			} else if(right == highest) {
				animator.SetInteger("MoveDirection", 4);
			} else if(left == highest) {
				animator.SetInteger("MoveDirection", 3);
			}
		}
	}
	
	/** <returns>The currently held interactable, or <code>null</code></returns> */
	public IInteractable Interactable {
		set { interactable = value; }
		get { return interactable; }
	}
}
