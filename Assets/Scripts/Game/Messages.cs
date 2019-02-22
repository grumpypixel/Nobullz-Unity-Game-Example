using UnityEngine;

namespace game
{
	public class ScoreMessage : Message
	{
		public int score;

		public override void Reset()
		{
			score = 0;
		}
	}
}
