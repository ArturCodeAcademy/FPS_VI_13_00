using System;
using TMPro;
using UnityEngine;

public class OxygenTextUI : MonoBehaviour
{
	[Header("Params")]
	[Tooltip("{0} - current oxygen\n" +
			 "{1} - max oxygen\n" +
			 "{2} - current oxygen in percent 0-100%\n" +
			 "{3} - current oxygen in percent 0-1")]
	[SerializeField] private string _format = "O<size=50%>2</size>: {2:0}%";

	[Header("Components")]
	[SerializeField] private Oxygen _oxygen;
	[SerializeField] private TMP_Text _text;

	private void Start()
	{
		SetText(_oxygen.OxygenLevel, Oxygen.MAX_OXYGEN_LEVEL);
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
		SetText(_oxygen.OxygenLevel, Oxygen.MAX_OXYGEN_LEVEL);
	}

	private void SetText(float current, float max)
	{
		_text.text = string.Format(_format,
									current,
									max,
									current / max * 100,
									current / max);
	}
}
