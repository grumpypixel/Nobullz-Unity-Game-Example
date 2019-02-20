using UnityEngine;

namespace game
{
	[System.Serializable]
	public class SoundBank
	{
		[Range(0.0f, 1.0f)]
		public float		m_volume = 1.0f;
		public bool			m_randomize = false;
		public AudioClip[]	m_soundEffects;

		private int			m_current = 0;

		public bool hasSounds
		{
			get { return (m_soundEffects.Length > 0); }
		}

		public AudioClip GetFirst()
		{
			if (m_soundEffects.Length == 0)
			{
				return null;
			}
			if (m_randomize)
			{
				m_randomize = false;
			}
			m_current = 0;
			return m_soundEffects[m_current];
		}

		public AudioClip GetNext()
		{
			if (m_soundEffects.Length == 0)
			{
				return null;
			}

			if (m_randomize)
			{
				return GetRandomNext();
			}

			m_current++;
			if (m_current >= m_soundEffects.Length)
			{
				m_current = 0;
			}
			return m_soundEffects[ m_current ];
		}

		public AudioClip GetRandomNext()
		{
			Debugger.Assert(m_soundEffects.Length > 0);
			if (m_soundEffects.Length == 0)
			{
				return null;
			}

			if (m_soundEffects.Length == 1)
			{
				return m_soundEffects[0];
			}

			int last = m_current;
			while (last == m_current)
			{
				m_current = UnityEngine.Random.Range(0, m_soundEffects.Length);
			}
			return m_soundEffects[m_current];
		}
	}
}
