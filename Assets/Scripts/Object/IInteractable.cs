using UnityEngine;

public enum InteractableType {
	PushableBlock,
	Key,
	Brazier
}

public interface IInteractable {
	/** <summary>Pickup the object, it stays in the specified position</summary>
	 * <param name="position">The position the object should stay in</param> */
	void Pickup(Transform position);

	/** <summary>Drop the object</summary> */
	void Drop();

	/** <summary>Throw the object, always moves in the forward direction</summary>
	 * <param name="forward">The forward direction</param>
	 * <param name="up">The up direction</param> */
	void Throw(Vector3 forward);

	/** <summary>Lock or unlock the object, if it's locked it can't be picked up or move</summary>
	 * <param name="locked">Whether or not the object is locked</param> */
	void Lock(bool locked);

	/** <summary>Get the type of the object, should never change</summary>
	 * <returns>The type of the object</returns> */
	InteractableType GetInteractableType();

	/** <returns>Whether or not the object is currently locked</returns> */
	bool IsLocked();
}