using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour,IBounceable,IColorable {

	public Colors color;
	public Colors.ColorChoice color2;

	void Start () {
		color = new Colors(color2);
	}

	public Colors GetColor () {
		return color;
	}

	void Update () {
		
	}
}