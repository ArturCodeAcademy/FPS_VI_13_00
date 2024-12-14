using System;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : MonoBehaviour
{
	public float OxygenLevel { get; private set; } = 100f;
	public const float MAX_OXYGEN_LEVEL = 100f;
	public float OxygenLevelNormalized => OxygenLevel / MAX_OXYGEN_LEVEL;

	[Header("Params")]
	[SerializeField] private float _oxygenDecreaseRate = 1f;
	[SerializeField] private float _oxygenIncreaseRate = 1f;
	[SerializeField] private float _damageRate = 1f;

	[Header("Components")]
	[SerializeField] private Health _health;
	[SerializeField] private LayerMask _noOxygenArea;

	public event EventHandler OxygenChanged;
	public event EventHandler OxygenRestored;
	public event EventHandler OxygenDepleted;
	public event EventHandler OxygenEnded;
	public event EventHandler OxygenFullyRestored;

	private HashSet<Collider> _noOxygenAreas;

	private void Awake()
	{
		_noOxygenAreas = new HashSet<Collider>();
	}

	private void Update()
	{
		if (_noOxygenAreas.Count > 0 && OxygenLevel > 0)
		{
			OxygenLevel -= _oxygenDecreaseRate * Time.deltaTime;
			OxygenDepleted?.Invoke(this, EventArgs.Empty);
			OxygenChanged?.Invoke(this, EventArgs.Empty);

			if (OxygenLevel <= 0)
			{
				OxygenEnded?.Invoke(this, EventArgs.Empty);
			}
		}

		if (_noOxygenAreas.Count == 0 && OxygenLevel < MAX_OXYGEN_LEVEL)
		{
			OxygenLevel += _oxygenIncreaseRate * Time.deltaTime;
			OxygenRestored?.Invoke(this, EventArgs.Empty);
			OxygenChanged?.Invoke(this, EventArgs.Empty);

			if (OxygenLevel >= MAX_OXYGEN_LEVEL)
			{
				OxygenFullyRestored?.Invoke(this, EventArgs.Empty);
			}
		}

		if (OxygenLevel <= 0)
		{
			_health.Hit(_damageRate * Time.deltaTime);
		}

		OxygenLevel = Mathf.Clamp(OxygenLevel, 0, MAX_OXYGEN_LEVEL);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (_noOxygenArea == (_noOxygenArea | (1 << other.gameObject.layer)))
		{
			_noOxygenAreas.Add(other);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (_noOxygenArea == (_noOxygenArea | (1 << other.gameObject.layer)))
		{
			_noOxygenAreas.Remove(other);
		}
	}
}
