using UnityEngine;
using UnityEngine.EventSystems;

namespace game
{
	public class GameInput : MonoBehaviour
	{
		private IMessageDispatcher	m_messageDispatcher;
		private int					m_fingerId = -1;

		void Start()
		{
			m_messageDispatcher = GameContext.messageDispatcher;
		}

		void Update()
		{
			if (m_fingerId == -1)
			{
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
				if (Input.touchCount > 0)
				{
					Touch touch = Input.GetTouch(0);
					if (touch.phase == TouchPhase.Began)
					{
						m_fingerId = 0;
						SendTouchBeganMessage(touch.position, m_fingerId);
					}
				}
#else
				if (Input.GetMouseButtonDown(0))
				{
					m_fingerId = 0;
					SendTouchBeganMessage(Input.mousePosition, m_fingerId);
				}
#endif
			}
			else
			{
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
				int touchCount = Input.touchCount;
				for (int i = 0; i < touchCount; ++i)
				{
					Touch touch = Input.GetTouch(i);
					if (touch.fingerId == m_fingerId)
					{
						if (touch.phase == TouchPhase.Moved)
						{
							SendTouchMovedMessage(touch.position);
						}
						else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
						{
							m_fingerId = -1;
							SendTouchEndedMessage(touch.position, touch.phase == TouchPhase.Canceled);
						}
					}
				}
#else
				if (m_fingerId == 0)
				{
					SendTouchMovedMessage(Input.mousePosition);
				}
				if (Input.GetMouseButtonUp(0))
				{
					m_fingerId = -1;
					SendTouchEndedMessage(Input.mousePosition, false);
				}
#endif
			}
		}

		private void SendTouchBeganMessage(Vector2 position, int fingerId)
		{
			TouchBeganMessage message = m_messageDispatcher.AddMessage<TouchBeganMessage>();
			message.touchPosition = position;
			message.isPointerOverUIObject = IsPointerOverGameObject(fingerId);
		}

		private void SendTouchMovedMessage(Vector2 position)
		{
			TouchMovedMessage message = m_messageDispatcher.AddMessage<TouchMovedMessage>();
			message.touchPosition = position;
		}

		private void SendTouchEndedMessage(Vector2 position, bool canceled)
		{
			TouchEndedMessage message = m_messageDispatcher.AddMessage<TouchEndedMessage>();
			message.touchPosition = position;
			message.canceled = canceled;
		}

		private bool IsPointerOverGameObject(int fingerId)
		{
			EventSystem eventSystem = EventSystem.current;
#if UNITY_EDITOR
			return (eventSystem.IsPointerOverGameObject() && eventSystem.currentSelectedGameObject != null);
#else
			return (eventSystem.IsPointerOverGameObject(fingerId) && eventSystem.currentSelectedGameObject != null);
#endif
		}
	}
}
