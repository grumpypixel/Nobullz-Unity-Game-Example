using UnityEngine;

namespace game
{
	public class PoolObject : MonoBehaviour
	{
		private bool m_isAvailableForReuse = true;

		public bool isAvailableForReuse
		{
			get
			{
				return m_isAvailableForReuse && this.gameObject.activeInHierarchy == false;
			}
			set
			{
				m_isAvailableForReuse = value;
				this.gameObject.SetActive(!value);
			}
		}
	}
}
