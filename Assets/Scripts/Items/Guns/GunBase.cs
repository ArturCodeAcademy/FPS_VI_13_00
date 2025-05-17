using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Aiming))]
public class GunBase : DefaultHoldableItem, IInteractable
{
	[field: SerializeField]
	public bool Active { get; set; } = true;

	public event EventHandler StateChanged;

	[SerializeField] private string _mainInformation;
	[SerializeField] private string _secondaryInformation;

	private Aiming _aiming;
	// private RaycastShootingBase _shootingBase; // Temprorary

	protected override void Awake()
	{
		base.Awake();
		_aiming = GetComponent<Aiming>();
	}

	public virtual string GetMainInformation()
	{
		return _mainInformation;
	}

	public virtual string GetSecondaryInformation()
	{
		return _secondaryInformation;
	}

	public void Interact()
	{
		Player.Instance.PlayerItemHolder.AddItem(this);
	}

	public override void OnShow()
	{
		_aiming.enabled = true;
	}

	public override void OnHide()
	{
		_aiming.enabled = false;
	}
}
