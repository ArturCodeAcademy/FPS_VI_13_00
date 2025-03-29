using UnityEngine;

public class Flashlight : DefaultHoldableItem
{
    public bool IsOn => _light.enabled;

	[SerializeField] private Light _light;

	protected override void Awake()
	{
		base.Awake();
		enabled = false;
		_light.enabled = false;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_light.enabled = !_light.enabled;
			InvokeItemChanged();
		}
	}

	public override void OnShow()
	{
		base.OnShow();
		enabled = true;
	}

	public override void OnHide()
	{
		base.OnHide();
		enabled = false;
	}

	public override void OnDrop()
	{
		base.OnDrop();
		_light.enabled = false;
		enabled = false;
	}
}
