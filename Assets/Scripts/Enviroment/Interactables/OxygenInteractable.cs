using System;
using UnityEngine;

public class OxygenInteractable : MonoBehaviour, ILongInteractable
{
	[field:SerializeField] public bool Active { get; set; }
	public float Progress => _currentValue / _maxValue;

	public event EventHandler ProgressChanged;
	public event EventHandler StateChanged;

	[SerializeField] private string _mainInformation;
	[Tooltip("0 - current value" +
			 "1 - max value" +
			 "2 - current value in percent" +
			 "3 - current value from 0 to 1")]
	[SerializeField] private string _secondaryInformationFormat;
	[SerializeField] private float _maxValue = 100;
	[SerializeField] private float _decreaseSpeed = 1;

	private float _currentValue;

	private void Awake()
	{
		_currentValue = _maxValue;
	}

	public string GetMainInformation()
	{
		return _mainInformation;
	}

	public string GetSecondaryInformation()
	{
		return string.Format(	_secondaryInformationFormat,
								_currentValue,
								_maxValue,
								_currentValue / _maxValue * 100,
								_currentValue / _maxValue);
	}

	public void Interact()
	{
		float decreaseValue = _decreaseSpeed * Time.deltaTime;
		decreaseValue = Mathf.Min(decreaseValue, _currentValue);

		_currentValue -= Player.Instance.Oxygen.AddOxygen(decreaseValue);

		ProgressChanged?.Invoke(this, EventArgs.Empty);
		StateChanged?.Invoke(this, EventArgs.Empty);
	}

	public void StopInteraction()
	{
		
	}
}
