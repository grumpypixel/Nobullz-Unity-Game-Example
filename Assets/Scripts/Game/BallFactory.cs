using UnityEngine;

namespace game
{
	public class BallFactory : MonoBehaviour
	{
		private GameObjectPool 	m_pool;
		private BallDatabase	m_database;

		void Awake()
		{
			m_pool = FindObjectOfType<GameObjectPool>();
			m_database = FindObjectOfType<BallDatabase>();
		}

		public Ball CreateBall(Vector3 position, BallType type = BallType.T_1)
		{
			GameObject go = m_pool.GetGameObject(Constants.BallPoolName);
			if (go)
			{
				Ball ball = go.GetComponent<Ball>();
				if (ball)
				{
					Color color = m_database.GetData(type).color;
					ball.Reset(position, color);
					return ball;
				}
			}
			return null;
		}

		public BallDatabase GetDatabase()
		{
			return m_database;
		}
	}
}
