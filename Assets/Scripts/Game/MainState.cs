using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace game
{
	public class MainState : MonoBehaviour
	{
		public string targetScene;

		IEnumerator Start()
		{
			yield return new WaitForEndOfFrame();

			if (string.IsNullOrEmpty(this.targetScene) == false)
			{
				SceneManager.LoadScene(this.targetScene);
			}
		}
	}
}
