using UnityEngine;

namespace game
{
	public class Level : MonoBehaviour
	{
		void Start()
		{
#if UNITY_EDITOR
			CameraController camControl = GameContext.cameraController;

			float size = 1f;
			float halfSize = 0.5f;
			CreateWall(new Vector3(0, camControl.bottom - halfSize, 0), camControl.width + 2, size);
			CreateWall(new Vector3(0, camControl.top + halfSize, 0), camControl.width + 2, size);
			CreateWall(new Vector3(camControl.left - halfSize, 0, 0), size, camControl.height + 2);
			CreateWall(new Vector3(camControl.right + halfSize, 0, 0), size, camControl.height + 2);
#endif
		}

		private void CreateWall(Vector3 position, float width, float height)
		{
			GameObject wallPrefab = GameContext.prefabs.wall;
			GameObject wall = GameObject.Instantiate(wallPrefab, position, Quaternion.identity, this.transform);
			wall.transform.localScale = new Vector3(width, height, 1f);
		}
	}
}
