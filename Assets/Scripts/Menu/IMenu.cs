using UnityEngine;
using System.Collections;

public interface IMenu {
	void Open();

	void Close();

	Transform GetCameraTransform();
}
