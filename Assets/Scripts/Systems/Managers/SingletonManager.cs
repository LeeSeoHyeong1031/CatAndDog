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
				// 이미 존재하는 인스턴스를 찾아봄.
				instance = (T)FindObjectOfType(typeof(T));

				//null이라면 호출
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
		//찾아봄
		instance = (T)FindObjectOfType(typeof(T));

		//null이라면
		if (instance == null)
		{
			GameObject gameObj = new GameObject(); //게임 오브젝트 하나 만듬
			gameObj.name = typeof(T).Name; //게임 오브젝트의 이름을 'T'의 이름을 줌
			instance = gameObj.AddComponent<T>(); //게임 오브젝트 한테 T컴포넌트를 붙이고 붙인것을 instance에 할당.
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
