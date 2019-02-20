using UnityEngine;

namespace game
{
	[System.Serializable]
	public class BallData
	{
		public BallType		type;
		public Color		color;
		public int			value;
		public BallType		nextType;
	}

	public class BallDatabase : MonoBehaviour
	{
		public BallData[]	ballData;

		public BallData GetData(BallType ballType)
		{
			int count = this.ballData.Length;
			for (int i = 0; i < count; ++i)
			{
				if (this.ballData[i].type == ballType)
				{
					return this.ballData[i];
				}
			}
			return null;
		}

		public BallType GetNextBallType(BallType type)
		{
			return GetData(type).nextType;
		}

		public Color GetColor(BallType type)
		{
			return GetData(type).color;
		}

		public int GetValue(BallType type)
		{
			return GetData(type).value;
		}
	}
}
