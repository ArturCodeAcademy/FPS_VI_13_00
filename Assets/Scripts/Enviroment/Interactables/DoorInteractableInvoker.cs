using System;
using UnityEngine;

public class DoorInteractableInvoker : MonoBehaviour, IInteractable
{
	public bool Active => _active;
	public event EventHandler StateChanged;

	[SerializeField] private bool _active = true;
	[SerializeField] private TwoStatesRotator _rotator;
	[SerializeField] private string _mainInformation;

	public string GetMainInformation() => _mainInformation;
	public string GetSecondaryInformation() => _rotator.IsFirstState ? "Opened" : "Closed";
	public void Interact()
	{
		_rotator.ChangeState();
		StateChanged?.Invoke(this, EventArgs.Empty);
	}

	public void ChangeActive(bool active)
	{
		if (_active == active)
		{
			return;
		}

		_active = active;
		StateChanged?.Invoke(this, EventArgs.Empty);
	}
}
