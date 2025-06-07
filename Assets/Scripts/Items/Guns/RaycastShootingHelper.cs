using UnityEngine;

public static class RaycastShootingHelper
{
    public static bool TryGetHitPointAndNormal(Ray ray, out Vector3 hitPoint, out Vector3 hitNormal, float maxDistance = Mathf.Infinity)
	{
		if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, ~Player.Instance.PlayerLayerMask))
		{
			hitPoint = hit.point;
			hitNormal = hit.normal;
			return true;
		}

		hitPoint = Vector3.zero;
		hitNormal = Vector3.zero;
		return false;
	}
}
