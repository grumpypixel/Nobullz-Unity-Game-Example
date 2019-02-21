using UnityEngine;

namespace game
{
	public static class Debugger
	{
		public delegate void BreakDelegate(string format, params object[] args);

		public static void SetBreakDelegate(BreakDelegate breakDelegate)
		{
		#if UNITY_DEBUG
			s_m_breakDelegate = breakDelegate;
		#endif
		}

	#if UNITY_DEBUG
		private static BreakDelegate s_m_breakDelegate = null;
	#endif
		public static void Assert(bool condition)
		{
		#if UNITY_DEBUG
			if (!condition)
			{
				if (s_m_breakDelegate != null)
				{
					s_m_breakDelegate("ASSERT");
				}
				else
				{
					Debug.LogError("ASSERT");
				}
			}
		#endif
		}

		public static void Assert(bool condition, string format, params object[] args)
		{
		#if UNITY_DEBUG
			if (!condition)
			{
				if (s_m_breakDelegate != null)
				{
					s_m_breakDelegate(format, args);
				}
				else
				{
					Debug.LogError(string.Format(format, args));
				}
			}
		#endif
		}

		public static void Enter(string format, params object[] args)
		{
		#if UNITY_DEBUG
			if (s_m_breakDelegate != null)
			{
				s_m_breakDelegate(format, args);
			}
			else
			{
				Debug.Break();
			}
		#endif
		}
	}
}
