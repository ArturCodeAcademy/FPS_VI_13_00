using System;
using UnityEngine;
using UnityEngine.UI;

public class OxygenSliderUI : MonoBehaviour
{
	[SerializeField] private Oxygen _oxygen;
	[SerializeField] private Image _fill;

	private void Start()
	{
		SetFill(_oxygen.OxygenLevelNormalized);
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
		SetFill(_oxygen.OxygenLevelNormalized);
	}

	private void SetFill(float amount)
	{
		_fill.fillAmount = amount;
	}
}
