using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SingletonManager<T> : MonoBehaviour where T : Component
{
	private static T instance;
	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				// �̹� �����ϴ� �ν��Ͻ��� ã�ƺ�.
				instance = (T)FindObjectOfType(typeof(T));

				//null�̶�� ȣ��
				if (instance == null)
				{
					SetupIntance();
				}
			}
			return instance;
		}
	}
	public virtual void Awake()
	{
		RemoveDuplicastes();
	}


	private static void SetupIntance()
	{
		//ã�ƺ�
		instance = (T)FindObjectOfType(typeof(T));

		//null�̶��
		if (instance == null)
		{
			GameObject gameObj = new GameObject(); //���� ������Ʈ �ϳ� ����
			gameObj.name = typeof(T).Name; //���� ������Ʈ�� �̸��� 'T'�� �̸��� ��
			instance = gameObj.AddComponent<T>(); //���� ������Ʈ ���� T������Ʈ�� ���̰� ���ΰ��� instance�� �Ҵ�.
			DontDestroyOnLoad(gameObj);
		}
	}

	private void RemoveDuplicastes()
	{
		if (instance == null)
		{
			instance = this as T;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

}
