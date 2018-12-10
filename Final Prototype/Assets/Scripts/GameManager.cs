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
	public AudioManager playerAudio;

	private int buildNum = 1;


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
		playerAudio = player.GetComponent<AudioManager>();
		playerAudio.music = Resources.Load<AudioStuff>("Level1");
		playerAudio.StartPlaying();
	}

	public void LoadNextScene()
	{
		buildNum++;
		if (buildNum == 5)
		{
			transform.SetParent(player.transform);
			SceneManager.LoadScene(5);
		}
		else
		{
			SceneManager.LoadSceneAsync(buildNum, LoadSceneMode.Additive);
			playerAudio.StopAllCoroutinesMe();
			playerAudio.music = Resources.Load<AudioStuff>("Level" + buildNum);
			playerAudio.StartPlaying();
		}
	}
}
