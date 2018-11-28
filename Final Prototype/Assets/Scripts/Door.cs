using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private int locks;
    [SerializeField] private List<IDamageable> keys = new List<IDamageable>();

	public GameObject doorRight;
	public GameObject doorLeft;

    private void Start()
    {
        locks = keys.Count;
    }

    public void OpenLock(IDamageable key)
    {
        if (keys.Contains(key))
        {
            locks--;
            keys.Remove(key);
            Debug.LogWarning("You are the light of my life. Or you would be, if you lit up my lights (add lights when a switch/enemy is hit for the door)");
			if (locks == 0) {
				doorLeft.GetComponent<DoorOpen> ().OpenDoor ();
				doorRight.GetComponent<DoorOpen> ().OpenDoor ();
			}
        }
    }
}
