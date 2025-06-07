using UnityEngine;

public class ShotgunShooting : DefaultShooting
{
	[SerializeField]
	private int _pelletCount = 10;

	protected override void Shoot()
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

		for (int i = 0; i < _pelletCount; i++)
		{
			Raycast();
		}
	}
}
