using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Singleton stuff
	public static GameManager Instance { get; private set; }
	#endregion

	private void Awake()
	{
		#region Singleton stuff
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		Instance = this;

		// if we get a lot of managers it's easy to keep them all in a prefab, this lets DontDestroyOnLoad work if so
		// transform.parent = null;
		DontDestroyOnLoad(gameObject);
		#endregion
	}

	private void Start()
	{

	}

	private void Update()
	{

	}
}
