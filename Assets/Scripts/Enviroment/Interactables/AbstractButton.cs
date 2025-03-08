using System;
using UnityEngine;
using UnityEngine.Events;

public class AbstractButton : MonoBehaviour, IInteractable
{
    public UnityEvent OnInteract;
	public event EventHandler StateChanged;

	public bool Active
	{
		get
		{
			return _active;
		}
		set
		{
			_active = value;
			StateChanged?.Invoke(this, EventArgs.Empty);
		}
	}

	[SerializeField] private bool _active;

	[SerializeField] private string _mainInformation;
	[SerializeField] private string _secondaryInformation;

	private void Awake()
	{
		OnInteract ??= new UnityEvent();
	}

	public string GetMainInformation() => _mainInformation;
	public string GetSecondaryInformation() => _secondaryInformation;

	public void Interact()
	{
		OnInteract?.Invoke();
	}
}
