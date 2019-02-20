using UnityEngine;
using UnityEngine.UI;

namespace game
{
	public class ScoreManager : MonoBehaviour
	{
		public Text		scoreText;
		public float	smoothTime = 0.3f;

		private int		m_score = 0;
		private float	m_displayScore = 0;
		private float	m_velocity = 0f;

		void Start()
		{
			RegisterMessages();
		}

		void OnDisable()
		{
			DeregisterMessages();
		}

		void Update()
		{
			if (m_displayScore < m_score)
			{
				float updatedScore = Mathf.SmoothDamp(m_displayScore, m_score, ref m_velocity, this.smoothTime);
				m_displayScore = updatedScore;
				this.scoreText.text = ((int)Mathf.Round(m_displayScore)).ToString();
			}
		}

		private void RegisterMessages()
		{
			MessageCenter center = GameContext.messageCenter;
			center.AddListener<ScoreMessage>(HandleScoreMessage);
		}

		private void DeregisterMessages()
		{
			MessageCenter center = GameContext.messageCenter;
			center.RemoveListener<ScoreMessage>(HandleScoreMessage);
		}

		private void HandleScoreMessage(IMessageProvider provider)
		{
			ScoreMessage message = provider.GetMessage<ScoreMessage>();
			m_score += message.score;
		}
	}
}
