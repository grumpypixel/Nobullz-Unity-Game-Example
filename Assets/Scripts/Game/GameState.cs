using UnityEngine;

namespace game
{
	public class GameState : MonoBehaviour
	{
		private SceneLoader m_sceneLoader;

		void Awake()
		{
			GameContext.Initialize();
			m_sceneLoader = FindObjectOfType<SceneLoader>();
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
			center.AddListener<LoadSceneMessage>(HandleLoadSceneMessage);
			center.AddListener<PlaySoundMessage>(HandlePlaySoundMessage);
			center.AddListener<TouchBeganMessage>(HandleTouchBeganMessage);
		}

		private void DeregisterMessages()
		{
			MessageCenter center = GameContext.messageCenter;
			center.RemoveListener<LoadSceneMessage>(HandleLoadSceneMessage);
			center.RemoveListener<PlaySoundMessage>(HandlePlaySoundMessage);
			center.RemoveListener<TouchBeganMessage>(HandleTouchBeganMessage);
		}

		private void HandleLoadSceneMessage(IMessageProvider provider)
		{
			if (m_sceneLoader != null && m_sceneLoader.isLoading == false)
			{
				LoadSceneMessage message = provider.GetMessage<LoadSceneMessage>();
				string sceneName = GameHelper.GetSceneName(message.scene);
				m_sceneLoader.Load(sceneName);
			}
		}

		private void HandlePlaySoundMessage(IMessageProvider provider)
		{
			PlaySoundMessage message = provider.GetMessage<PlaySoundMessage>();
			GameContext.sfxPlayer.Play((int)message.sfxId);
		}

		public void OnHomeButtonPressed()
		{
			LoadSceneMessage loadSceneMessage = GameContext.messageDispatcher.AddMessage<LoadSceneMessage>();
			loadSceneMessage.scene = SceneType.Menu;
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
