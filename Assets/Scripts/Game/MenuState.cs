using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
	public class MenuState : MonoBehaviour
	{
		private SceneLoader m_sceneLoader;

		void Awake()
		{
			GameContext.Initialize();
			m_sceneLoader = GameObject.FindObjectOfType<SceneLoader>();
		}

		void Start()
		{
			RegisterMessages();
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
		}

		private void DeregisterMessages()
		{
			MessageCenter center = GameContext.messageCenter;
			center.RemoveListener<LoadSceneMessage>(HandleLoadSceneMessage);
			center.RemoveListener<PlaySoundMessage>(HandlePlaySoundMessage);
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

		public void OnPlayButtonPressed()
		{
			LoadSceneMessage loadSceneMessage = GameContext.messageDispatcher.AddMessage<LoadSceneMessage>();
			loadSceneMessage.scene = SceneType.Game;
			GameHelper.PlaySound(SfxId.PlayButton);
		}
	}
}
