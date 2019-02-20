using UnityEngine;

namespace game
{
	public class SfxPlayer : SfxPlayerBase
	{
		public SoundBank playButton;
		public SoundBank homeButton;
		public SoundBank ball;

		public override AudioClip GetClip(int effectId, out float volume)
		{
			switch ((SfxId)effectId)
			{
				case SfxId.PlayButton:
					return GetClipAndVolume(this.playButton, out volume);
				case SfxId.HomeButton:
					return GetClipAndVolume(this.homeButton, out volume);
				case SfxId.Ball:
					return GetClipAndVolume(this.ball, out volume);
				default:
					break;
			}
			volume = 1.0f;
			return null;
		}

		private AudioClip GetClipAndVolume(SoundBank bank, out float volume)
		{
			volume = bank.m_volume;
			return bank.GetNext();
		}
	}
}
