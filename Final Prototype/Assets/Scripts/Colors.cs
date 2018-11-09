﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors
{
	public Color color { get; private set; }
	private static readonly Color brown = new Color(150/255f, 75/255f, 0/255f);

	public Colors(Color? color = null)
	{
		this.color = color ?? Color.white;
	}

	/// <summary>
	/// Adds to the laser color based on what color you hit (laserColor = laserColor + hitColor OR laserColor += hitColor)
	/// </summary>
	public static Colors operator +(Colors laserColor, Colors hitColor)
	{
		// if white
		if (laserColor.color == Color.white)
		{
			return hitColor;
		}
		if (hitColor.color == Color.white)
		{
			return laserColor;
		}

		// if multicolor
		if (laserColor.color == Color.cyan || laserColor.color == Color.magenta || laserColor.color == Color.yellow)
		{
			return new Colors(brown);
		}

		// add colors
		if (laserColor.color == Color.red)
		{
			if (hitColor.color == Color.green)
			{
				return new Colors(Color.yellow);
			}
			if (hitColor.color == Color.blue)
			{
				return new Colors(Color.magenta);
			}
			return laserColor;
		}
		if (laserColor.color == Color.green)
		{
			if (hitColor.color == Color.red)
			{
				return new Colors(Color.yellow);
			}
			if (hitColor.color == Color.blue)
			{
				return new Colors(Color.cyan);
			}
			return laserColor;
		}
		if (laserColor.color == Color.blue)
		{
			if (hitColor.color == Color.green)
			{
				return new Colors(Color.cyan);
			}
			if (hitColor.color == Color.red)
			{
				return new Colors(Color.magenta);
			}
			return laserColor;
		}

		// if brown or error
		if (laserColor.color != brown)
		{
			Debug.LogWarning("Make sure you use the format \"laserColor + hitColor\", or tell me what happened if you did and this still happened -Greg");
		}
		return laserColor;
	}
}
