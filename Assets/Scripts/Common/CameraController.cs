using UnityEngine;

namespace game
{
	[RequireComponent(typeof(Camera))]
	public class CameraController : MonoBehaviour
	{
		private Transform 	m_transform;
		private Camera		m_camera;

		public Bounds bounds
		{
			get; private set;
		}

		public Camera mainCamera
		{
			get { return m_camera; }
		}

		public Vector3 position
		{
			get { return m_transform.position; }
		}

		public float width
		{
			get { return m_camera.orthographicSize * m_camera.aspect * 2; }
		}

		public float height
		{
			get { return m_camera.orthographicSize * 2; }
		}

		public float top
		{
			get { return m_transform.position.y + this.height * 0.5f; }
		}

		public float bottom
		{
			get { return m_transform.position.y - this.height * 0.5f; }
		}

		public float left
		{
			get { return m_transform.position.x - this.width * 0.5f; }
		}

		public float right
		{
			get { return m_transform.position.x + this.width * 0.5f; }
		}

		public float orthographicSize
		{
			get { return m_camera.orthographicSize; }
			set { m_camera.orthographicSize = value; }
		}

		void Awake()
		{
			m_transform = this.transform;
			m_camera = GetComponent<Camera>();
		}
	}
}
