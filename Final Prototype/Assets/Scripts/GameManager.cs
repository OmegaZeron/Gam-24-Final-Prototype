using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton stuff
    public static GameManager Instance { get; private set; }
    #endregion

    [SerializeField] private Player player;
    public Player PlayerInstance { get { return player; } }

	private int buildNum = 0;


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

	public void LoadNextScene()
	{
		buildNum++;
		SceneManager.LoadScene(buildNum, LoadSceneMode.Additive);
	}
}
