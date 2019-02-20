﻿using UnityEngine;

namespace game
{
	public static class GameHelper
	{
		public static string GetSceneName(SceneType scene)
		{
			switch (scene)
			{
				case SceneType.Game: return Constants.GameSceneName;
				case SceneType.Menu: return Constants.MenuSceneName;
			}
			return null;
		}

		public static void PlaySound(SfxId sfxId)
		{
			PlaySoundMessage soundMessage = GameContext.messageDispatcher.AddMessage<PlaySoundMessage>();
			soundMessage.sfxId = sfxId;
		}
	}
}
