using UnityEngine;

namespace game
{
	public class Ball : MonoBehaviour
	{
		private BallType type = BallType.T_1;

		public bool destroy { set; private get; }
		private bool upgrade { set; get; }

		public Vector3 position
		{
			set { this.transform.position = value; }
		}

		public Color color
		{
			set { GetComponent<SpriteRenderer>().color = value; }
		}

		public void Reset(Vector3 position, Color color)
		{
			this.type = BallType.T_1;
			this.position = position;
			this.color = color;
			this.destroy = false;
			this.upgrade = false;
		}

		void OnCollisionEnter2D(Collision2D collision)
		{
			if (this.destroy)
			{
				return;
			}

			SfxId sfxId = SfxId.None;

			GameObject other = collision.gameObject;
			if (other.CompareTag(Constants.BallTag))
			{
				sfxId = SfxId.Ball;

				Ball otherBall = other.GetComponent<Ball>();
				if (this.type == otherBall.type)
				{
					otherBall.destroy = true;
					this.upgrade = true;

					this.color = otherBall.color = Color.white;
				}
			}
			else if (other.CompareTag(Constants.WallTag))
			{
				sfxId = SfxId.Ball;
			}

			GameHelper.PlaySound(sfxId);
		}

		void OnCollisionExit2D(Collision2D collision)
		{
			if (this.upgrade)
			{
				BallDatabase database = GameContext.ballFactory.GetDatabase();
				BallType nextType = database.GetNextBallType(this.type);
				if (nextType != BallType.Null)
				{
					ScoreMessage scoreMessage = GameContext.messageDispatcher.AddMessage<ScoreMessage>();
					scoreMessage.score = database.GetValue(this.type) * 2;

					this.type = nextType;
					this.color = database.GetColor(nextType);

					this.upgrade = false;
				}
				else
				{
					this.destroy = true;
				}
			}
			if (this.destroy)
			{
				MakeAvailableForReuse();
			}
		}

		public void MakeAvailableForReuse()
		{
			PoolObject poolObject = GetComponent<PoolObject>();
			if (poolObject)
			{
				poolObject.isAvailableForReuse = true;
			}
		}
	}
}
