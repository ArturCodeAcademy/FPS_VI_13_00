using System;
using UnityEngine;

public abstract class HoldableItem : MonoBehaviour
{
	public event Action<HoldableItem> ItemChanged;

	[field: SerializeField] public bool Droppable { get; private set; } = true;
	[field: SerializeField] public Vector3 HoldLocalPosition { get; private set; }
	[field: SerializeField] public Quaternion HoldLocalRotation { get; private set; }

	[field: Space]
	[field: SerializeField] public virtual Sprite Icon { get; private set; }
	[field: SerializeField] public virtual Color IconColor { get; private set; } = Color.white;

	public abstract void OnPickup();
	public abstract void OnDrop();
	public abstract void OnShow();
	public abstract void OnHide();

	protected virtual void InvokeItemChanged() => ItemChanged?.Invoke(this);

#if UNITY_EDITOR

	[ContextMenu(nameof(SetCurrentPositionAsHoldPosition))]
	private void SetCurrentPositionAsHoldPosition()
	{
		HoldLocalPosition = transform.localPosition;
		HoldLocalRotation = transform.localRotation;
	}

#endif
}
