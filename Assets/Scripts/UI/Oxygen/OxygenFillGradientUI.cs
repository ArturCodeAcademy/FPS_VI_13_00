using System;
using UnityEngine;
using UnityEngine.UI;

public class OxygenFillGradientUI : MonoBehaviour
{
	[SerializeField] private Gradient _gradient;

	[Header("Components")]
	[SerializeField] private Oxygen _oxygen;
	[SerializeField] private Image _fill;

	private void Start()
	{
		SetGradient(_oxygen.OxygenLevelNormalized);
	}

	private void OnEnable()
	{
		_oxygen.OxygenChanged += OnOxygenChanged;
	}

	private void OnDisable()
	{
		_oxygen.OxygenChanged -= OnOxygenChanged;
	}

	private void OnOxygenChanged(object o, EventArgs a)
	{
		SetGradient(_oxygen.OxygenLevelNormalized);
	}

	private void SetGradient(float amount)
	{
		_fill.color = _gradient.Evaluate(amount);
	}
}
