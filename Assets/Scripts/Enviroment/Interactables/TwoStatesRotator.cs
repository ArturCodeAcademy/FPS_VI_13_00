using UnityEngine;

public class TwoStatesRotator : MonoBehaviour
{
	[SerializeField] private Quaternion _firstState;
	[SerializeField] private Quaternion _secondState;
	[SerializeField] private float _rotationDuration = 1f;
	[SerializeField] private bool _startWithFirstState = true;
	[SerializeField] private bool _useLocalRotation = true;

	public bool IsFirstState { get; private set; }
	private float _lerpValue = 0f;

	private void Start()
	{
		Quaternion targetRotation = _startWithFirstState ? _firstState : _secondState;
		_lerpValue = _startWithFirstState ? 0f : 1f;
		IsFirstState = _startWithFirstState;

		if (_useLocalRotation)
		{
			transform.localRotation = targetRotation;
		}
		else
		{
			transform.rotation = targetRotation;
		}
	}

	private void Update()
	{
		if (IsFirstState && _lerpValue == 0f || !IsFirstState && _lerpValue == 1f)
		{
			return;
		}

		_lerpValue += Time.deltaTime / _rotationDuration * (IsFirstState ? -1 : 1);
		_lerpValue = Mathf.Clamp01(_lerpValue);

		Quaternion rotation = Quaternion.Lerp(_firstState, _secondState, _lerpValue);
		SetRotation(rotation);
	}

	private void SetRotation(Quaternion rotation)
	{
		if (_useLocalRotation)
		{
			transform.localRotation = rotation;
		}
		else
		{
			transform.rotation = rotation;
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

		Vector3 forward = transform.parent?.forward ?? Vector3.forward;
		Vector3 up = transform.parent?.up ?? Vector3.up;

		Gizmos.DrawRay(transform.position, _firstState * forward);
		Gizmos.DrawSphere(_firstState * forward + transform.position, 0.1f);
		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, _secondState * forward);
		Gizmos.DrawSphere(_secondState * forward + transform.position, 0.1f);

		Gizmos.color = Color.white;
		Gizmos.DrawLine(transform.position - up , transform.position + up);
	}

	private void Reset()
	{
		_firstState = transform.localRotation;
		_secondState = Quaternion.identity;
	}

#endif
}
