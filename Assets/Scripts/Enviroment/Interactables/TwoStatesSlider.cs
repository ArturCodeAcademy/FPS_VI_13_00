using UnityEngine;

public class TwoStatesSlider : MonoBehaviour
{
	[SerializeField] private Vector3 _firstState;
	[SerializeField] private Vector3 _secondState;
	[SerializeField] private float _slideDuration = 1f;
	[SerializeField] private bool _startWithFirstState = true;
	[SerializeField] private bool _useLocalPosition = true;

	public bool IsFirstState { get; private set; }
	private float _lerpValue = 0f;

	private void Start()
	{
		Vector3 targetPosition = _startWithFirstState ? _firstState : _secondState;
		_lerpValue = _startWithFirstState ? 0f : 1f;
		IsFirstState = _startWithFirstState;

		if (_useLocalPosition)
		{
			transform.localPosition = targetPosition;
		}
		else
		{
			transform.position = targetPosition;
		}
	}

	private void Update()
	{
		if (IsFirstState && _lerpValue == 0f || !IsFirstState && _lerpValue == 1f)
		{
			return;
		}

		_lerpValue += Time.deltaTime / _slideDuration * (IsFirstState ? -1 : 1);
		_lerpValue = Mathf.Clamp01(_lerpValue);

		Vector3 position = Vector3.Lerp(_firstState, _secondState, _lerpValue);
		SetPosition(position);
	}

	private void SetPosition(Vector3 position)
	{
		if (_useLocalPosition)
		{
			transform.localPosition = position;
		}
		else
		{
			transform.position = position;
		}
	}

	public void ChangeState()
	{
		IsFirstState = !IsFirstState;
	}

#if UNITY_EDITOR

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;

		Vector3 from, to;
		if (_useLocalPosition)
		{
			from = transform.parent?.localPosition + _firstState ?? _firstState;
			to = transform.parent?.localPosition + _secondState ?? _secondState;
		}
		else
		{
			from = _firstState;
			to = _secondState;
		}

		Gizmos.DrawSphere(from, 0.1f);
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(to, 0.1f);
		Gizmos.color = Color.green;
		Gizmos.DrawLine(from, to);

		Gizmos.color = Color.white;
	}

	private void Reset()
	{
		_firstState = transform.localPosition;
		_secondState = Vector3.zero;
	}

#endif
}
