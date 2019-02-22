using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
	public class MenuState : MonoBehaviour
	{
		void Awake()
		{
			GameContext.Initialize();
		}

		public void OnPlayButtonPressed()
		{
			LoadSceneMessage loadSceneMessage = GameContext.messageDispatcher.AddMessage<LoadSceneMessage>();
			loadSceneMessage.scene = GameHelper.GetSceneName(SceneType.Game);
			GameHelper.PlaySound(SfxId.PlayButton);
		}
	}
}
