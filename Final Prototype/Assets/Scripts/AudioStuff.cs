using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Music", menuName = "Music")]
public class AudioStuff : ScriptableObject
{
	public AudioClip introMusic;
	public float introMusicLength;
	public AudioClip loopMusic;
	public float loopMusicLength;
}
