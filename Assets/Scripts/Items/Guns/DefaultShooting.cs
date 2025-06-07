using UnityEngine;

[RequireComponent(typeof(Aiming))]
public class DefaultShooting : MonoBehaviour
{
	[SerializeField]
	protected Aiming Aiming;
	[SerializeField]
	protected float ScatterDefaultDegree = 5f;
	[SerializeField]
	protected float ScatterAimingDegree = 0.5f;

	[Space(3)]
    [SerializeField]
	protected GameObject HitHolePrefab;
    [SerializeField]
	protected GameObject MuzzleFlashPrefab;
	[SerializeField]
	protected Vector3 MuzzleFlashOffset = Vector3.zero;
	[SerializeField]
	protected AudioSource ShootingAudioSource;
	[SerializeField]
	protected float CooldownTime = 0.5f;
	[SerializeField]
	protected bool HoldToShoot = false;

	protected float CooldownTimer = 0f;

	protected virtual void Update()
	{
		if (CooldownTime > 0f)
		{
			CooldownTimer -= Time.deltaTime;
			if (CooldownTimer < 0f)
			{
				CooldownTimer = 0f;
			}
			else
			{
				return;
			}
		}

		if (Input.GetMouseButtonDown(0) || (HoldToShoot && Input.GetMouseButton(0)))
		{
			Shoot();
			CooldownTimer = CooldownTime;
		}
	}

	protected virtual void Shoot()
	{
		if (ShootingAudioSource is not null)
		{
			ShootingAudioSource.Play();
		}

		if (MuzzleFlashPrefab is not null)
		{
			Vector3 offsetWithDirrection = transform.forward * MuzzleFlashOffset.z + transform.right * MuzzleFlashOffset.x + transform.up * MuzzleFlashOffset.y;
			GameObject muzzleFlash = Instantiate(MuzzleFlashPrefab, transform.position + offsetWithDirrection, transform.rotation);
		}

		Raycast();
	}

	protected virtual void Raycast()
	{
		Transform cameraTransform = Player.Instance.Camera.transform;
		Vector3 scatterDirection = GetScatterDirection();
		Ray ray = new Ray(cameraTransform.position, scatterDirection);
		if (RaycastShootingHelper.TryGetHitPointAndNormal(ray, out Vector3 hitPoint, out Vector3 hitNormal))
		{
			if (HitHolePrefab != null)
			{
				GameObject hitHole = Instantiate(HitHolePrefab, hitPoint, Quaternion.LookRotation(hitNormal));
			}
		}
	}

	protected virtual Vector3 GetScatterDirection()
	{
		float scatterDegree = Mathf.Lerp(ScatterDefaultDegree, ScatterAimingDegree, Aiming.AimingValue);
		float angleX = Random.Range(-scatterDegree, scatterDegree);
		float angleY = Random.Range(-scatterDegree, scatterDegree);
		Vector3 direction = Quaternion.Euler(angleX, angleY, 0) * Player.Instance.Camera.transform.forward;
		return direction.normalized;
	}
}
