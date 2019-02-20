using UnityEngine;

namespace game
{
	public static class UnityHelper
	{
		public static Vector3 ConvertScreenToWorldPoint(Vector2 screenPoint, Camera camera)
		{
			Vector3 position = screenPoint;
			position.z = Mathf.Abs(camera.transform.position.z);
			return camera.ScreenToWorldPoint(position);
		}
	}
}
