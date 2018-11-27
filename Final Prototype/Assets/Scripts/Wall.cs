using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour,IBounceable,IColorable {

	public Colors color;

	void Start () {
		
	}

	public Colors GetColor () {
		return color;
	}

	void Update () {
		
	}
}