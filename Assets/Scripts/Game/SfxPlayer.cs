using UnityEngine;

namespace game
{
	public class SfxPlayer : SfxPlayerBase
	{
		public SoundBank playButton;
		public SoundBank homeButton;
		public SoundBank ball;
		public SoundBank touch;

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
				case SfxId.Touch:
					return GetClipAndVolume(this.touch, out volume);
				default:
					break;
			}
			volume = 1.0f;
			return null;
		}

		private AudioClip GetClipAndVolume(SoundBank bank, out float volume)
		{
			volume = bank.volume;
			return bank.GetNext();
		}
	}
}
