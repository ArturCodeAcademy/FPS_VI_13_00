using UnityEngine;

public class FallDamager : MonoBehaviour
{
	[Header("Params")]
	[SerializeField, Min(0)] private float _minVelocityWithoutDamage = 10f;
	[SerializeField, Min(0)] private float _damageMultiplier = 1f;

	[Header("Components")]
	[SerializeField] private Health _health;
	[SerializeField] private CharacterGravityController _gravityController;

	private void OnEnable()
	{
		_gravityController.Landed += OnLanded;
	}

	private void OnDisable()
	{
		_gravityController.Landed -= OnLanded;
	}

	private void OnLanded(object _, LandEventArgs args)
	{
		if (args.Velocity >= -_minVelocityWithoutDamage)
			return;

		_health.Hit(Mathf.Abs(args.Velocity) * _damageMultiplier);
	}
}
