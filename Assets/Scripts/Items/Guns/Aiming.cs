using UnityEngine;

[RequireComponent(typeof(HoldableItem))]
public class Aiming : MonoBehaviour
{
    public float AimingValue { get; private set; } = 0f;

    [SerializeField] private Vector3 _position;
	[SerializeField] private Quaternion _rotation;

    [SerializeField, Min(0)] private float _aimingDuration;
	[SerializeField] private AnimationCurve _aimingCurve;

	private HoldableItem _holdableItem;

	private void Awake()
	{
		_holdableItem = GetComponent<HoldableItem>();
		enabled = false;
	}

	private void OnEnable()
	{
		transform.localPosition = _holdableItem.HoldLocalPosition;
		transform.localRotation = _holdableItem.HoldLocalRotation;
	}

	private void Update()
	{
		if (Input.GetMouseButton(1) && AimingValue == 1)
			return;

		if (!Input.GetMouseButton(1) && AimingValue == 0)
			return;

		if (Input.GetMouseButton(1))
			AimingValue += Time.deltaTime / _aimingDuration;
		else
			AimingValue -= Time.deltaTime / _aimingDuration;

		AimingValue = Mathf.Clamp01(AimingValue);

		float t = _aimingCurve.Evaluate(AimingValue);

		transform.localPosition = Vector3.Lerp(_holdableItem.HoldLocalPosition, _position, t);
		transform.localRotation = Quaternion.Slerp(_holdableItem.HoldLocalRotation, _rotation, t);
	}
}
