using System;
using UnityEngine;

public class DefaultInteractableItem : MonoBehaviour, IInteractable
{
    [SerializeField] private string _mainInformation;
	[SerializeField] private string _secondaryInformation;
	[SerializeField] private HoldableItem _holdableItem;

	[field:SerializeField] public bool Active { get; private set; }

	public event EventHandler StateChanged;

	public string GetMainInformation() => _mainInformation;
	public string GetSecondaryInformation() => _secondaryInformation;
	public void Interact()
	{
		Player.Instance.PlayerItemHolder.AddItem(_holdableItem);
	}

#if UNITY_EDITOR

	private void Reset()
	{
		_holdableItem = GetComponent<HoldableItem>();
	}

#endif
}
