using UnityEngine;
using UnityEngine.SceneManagement;

namespace game
{
	public class SceneLoader : MonoBehaviour
	{
		public string			targetSceneName = "";

		public Color			fadeInColor = Color.black;
		public Color			fadeOutColor = Color.black;

		public float			fadeInTime = 0.5f;
		public float			fadeOutTime = 0.5f;

		public bool				autoFadeIn = true;

		private UIScreenFader	m_screenFader;
		private bool			m_loading = false;

		public bool isLoading
		{
			get { return m_loading; }
		}

		public void Load(string sceneName)
		{
			this.targetSceneName = sceneName;
			FadeOut(this.targetSceneName);
		}

		public void Load()
		{
			FadeOut(this.targetSceneName);
		}

		void Awake()
		{
			m_screenFader = GameObject.FindObjectOfType<UIScreenFader>();
		}

		void Start()
		{
			if (string.IsNullOrEmpty(this.targetSceneName) == false)
			{
				Load();
			}
		}

		void OnEnable()
		{
			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		void OnDisable()
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;
		}

		void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			Resources.UnloadUnusedAssets();
			System.GC.Collect();

			if (autoFadeIn)
			{
				FadeIn();
			}
		}

		public void FadeIn()
		{
			if (m_screenFader != null)
			{
				m_screenFader.FadeIn(this.fadeInColor, this.fadeInTime, 0f, this.OnFadeInComplete);
			}
		}

		public void FadeOut(string targetSceneName)
		{
			this.targetSceneName = targetSceneName;

			if (m_screenFader != null)
			{
				m_loading = true;
				m_screenFader.FadeOut(this.fadeOutColor, this.fadeOutTime, 0f, this.LoadGameScene);
			}
			else
			{
				LoadGameScene();
			}
		}

		private void OnFadeInComplete()
		{
			if (m_screenFader != null)
			{
				m_screenFader.enabled = false;
			}
		}

		private void LoadGameScene()
		{
			if (string.IsNullOrEmpty(this.targetSceneName) == false)
			{
				SceneManager.LoadScene(this.targetSceneName);
			}
		}
	}
}
