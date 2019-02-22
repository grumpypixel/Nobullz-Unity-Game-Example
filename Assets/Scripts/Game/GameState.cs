using UnityEngine;

namespace game
{
	public class GameState : MonoBehaviour
	{
		void Awake()
		{
			GameContext.Initialize();
		}

		void Start()
		{
			RegisterMessages();
			GameContext.ballFactory.CreateBall(Vector3.zero);
		}

		void OnDisable()
		{
			DeregisterMessages();
		}

		private void RegisterMessages()
		{
			MessageCenter center = GameContext.messageCenter;
			center.AddListener<TouchBeganMessage>(HandleTouchBeganMessage);
		}

		private void DeregisterMessages()
		{
			MessageCenter center = GameContext.messageCenter;
			center.RemoveListener<TouchBeganMessage>(HandleTouchBeganMessage);
		}

		public void OnHomeButtonPressed()
		{
			LoadSceneMessage loadSceneMessage = GameContext.messageDispatcher.AddMessage<LoadSceneMessage>();
			loadSceneMessage.scene = GameHelper.GetSceneName(SceneType.Menu);
			GameHelper.PlaySound(SfxId.HomeButton);
		}

		private void HandleTouchBeganMessage(IMessageProvider provider)
		{
			TouchBeganMessage message = provider.GetMessage<TouchBeganMessage>();
			if (!message.isPointerOverUIObject)
			{
				Vector3 position = UnityHelper.ConvertScreenToWorldPoint(message.touchPosition, Camera.main);
				GameContext.ballFactory.CreateBall(position);
			}
		}
	}
}
