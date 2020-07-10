﻿using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
	#region Fields
	private static T _instance;
	#endregion

	#region Properties
	public static T Instance
	{
		get
		{
			if ( _instance == null )
			{
				_instance = FindObjectOfType<T> ();
				if ( _instance == null )
				{
					GameObject obj = new GameObject ();
					obj.name = typeof ( T ).Name;
					_instance = obj.AddComponent<T> ();
				}
			}
			return _instance;
		}
	}
	#endregion

	#region Methods
	protected virtual void Awake ()
	{
		if ( _instance == null )
		{
			_instance = this as T;
			DontDestroyOnLoad ( gameObject );
		}
		else
		{
			Destroy ( gameObject );
		}
	}
	#endregion
	
}