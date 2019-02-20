using UnityEngine;

namespace game
{
	public class LoadSceneMessage : Message
	{
		public SceneType scene;

		public override void Reset()
		{
			scene = SceneType.None;
		}
	}

	public class PlaySoundMessage : Message
	{
		public SfxId sfxId;

		public override void Reset()
		{
			sfxId = SfxId.None;
		}
	}

	public class ScoreMessage : Message
	{
		public int score;

		public override void Reset()
		{
			score = 0;
		}
	}

	public class TouchBeganMessage : Message
	{
		public Vector2 touchPosition;
		public bool isPointerOverUIObject;

		public override void Reset()
		{
			touchPosition = Vector2.zero;
			isPointerOverUIObject = false;
		}
	}

	public class TouchMovedMessage : Message
	{
		public Vector2 touchPosition;

		public override void Reset()
		{
			touchPosition = Vector2.zero;
		}
	}

	public class TouchEndedMessage : Message
	{
		public Vector2 touchPosition;
		public bool canceled;

		public override void Reset()
		{
			touchPosition = Vector2.zero;
			canceled = false;
		}
	}
}
