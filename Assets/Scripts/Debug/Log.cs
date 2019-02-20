using UnityEngine;

namespace game
{
	public static class Log
	{
		public enum Level
		{
			Info,
			Warning,
			Error,
			None
		}

		public delegate void LogDelegate(Level level, string format, params object[] args);

		public static void SetLogDelegate(LogDelegate logDelegate)
		{
#if UNITY_DEBUG
			s_m_logDelegate = logDelegate;
#endif
		}

		public static void SetLogLevel(Level level)
		{
#if UNITY_DEBUG
			s_m_logLevel = level;
#endif
		}

		public static Level GetLogLevel()
		{
			return s_m_logLevel;
		}

#if UNITY_DEBUG
		private static LogDelegate  s_m_logDelegate = null;
#endif
		private static Level        s_m_logLevel = Level.Info;

		public static void Info(string format, params object[] args)
		{
#if UNITY_DEBUG
			if (s_m_logLevel > Level.Info)
			{
				return;
			}

			if (s_m_logDelegate != null)
			{
				try
				{
					s_m_logDelegate(Level.Info, format, args);
				}
				catch (System.Exception e)
				{
					Debug.Log(string.Format(format, args));
					Debug.LogException(e);
				}
			}
			else
			{
				Debug.Log(string.Format(format, args));
			}
#endif
		}

		public static void Warning(string format, params object[] args)
		{
#if UNITY_DEBUG
			if (s_m_logLevel > Level.Warning)
			{
				return;
			}

			if (s_m_logDelegate != null)
			{
				try
				{
					s_m_logDelegate(Level.Warning, format, args);
				}
				catch (System.Exception e)
				{
					Debug.LogWarning(string.Format(format, args));
					Debug.LogException(e);
				}
			}
			else
			{
				Debug.LogWarning(string.Format(format, args));
			}
#endif
		}

		public static void Error(string format, params object[] args)
		{
#if UNITY_DEBUG
			if (s_m_logLevel > Level.Error)
			{
				return;
			}

			if (s_m_logDelegate != null)
			{
				try
				{
					s_m_logDelegate(Level.Error, format, args);
				}
				catch (System.Exception e)
				{
					Debug.LogError(string.Format(format, args));
					Debug.LogException(e);
				}
			}
			else
			{
				Debug.LogError(string.Format(format, args));
			}
#endif
		}
	}
}
