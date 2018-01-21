using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

	private static T _instance;

	public static T instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<T>();
			
				if (_instance == null) {
					GameObject singleton = new GameObject(typeof(T).Name);
					_instance = singleton.AddComponent<T>();
				}
			}
			return _instance; 
		}
	}

	public virtual void Awake() {
		if (_instance == null) {
			_instance = this as T;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(_instance);
			GameObject singleton = new GameObject(typeof(T).Name);
			_instance = singleton.AddComponent<T>();
			_instance = this as T;
			DontDestroyOnLoad(gameObject);
			Debug.LogWarning("Singleton reinitialized, is there more than one placed in the scene?");
		}
	}

	public virtual void Initialize(MonoBehaviour parent) {}
}
