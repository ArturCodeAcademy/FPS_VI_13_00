using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContiniousRotator : MonoBehaviour, IInteractable
{
	bool isRotating = false;
	[SerializeField] private string _mainInformation;
	[SerializeField] private string _secondaryInformation;
	public bool Active => true;

	public event EventHandler StateChanged;

	

	public string GetMainInformation()
	{
		return _mainInformation;
	}

	public string GetSecondaryInformation()
	{
		return _secondaryInformation;
	}
	public void Interact()
	{
		isRotating = !isRotating;
		StateChanged?.Invoke(this, EventArgs.Empty);
	}

	void Update()
    {
		if (isRotating)
			transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);
	}
}
