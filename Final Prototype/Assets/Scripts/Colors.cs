using System.Collections;
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

	public static Colors operator +(Colors c1, Colors c2)
	{
		if (c1.color == Color.white)
		{
			return c2;
		}

		if (c1.color == Color.cyan || c1.color == Color.magenta || c1.color == Color.yellow)
		{
			return new Colors(brown);
		}

		if (c1.color == Color.red)
		{
			if (c2.color == Color.green)
			{
				return new Colors(Color.yellow);
			}
			if (c2.color == Color.blue)
			{
				return new Colors(Color.magenta);
			}
			return c1;
		}
		if (c1.color == Color.green)
		{
			if (c2.color == Color.red)
			{
				return new Colors(Color.yellow);
			}
			if (c2.color == Color.blue)
			{
				return new Colors(Color.cyan);
			}
			return c1;
		}
		if (c1.color == Color.blue)
		{
			if (c2.color == Color.green)
			{
				return new Colors(Color.cyan);
			}
			if (c2.color == Color.red)
			{
				return new Colors(Color.magenta);
			}
			return c1;
		}

		return c1;
	}
}
