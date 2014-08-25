using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Colors
{
	private static int[] possibleValues = new int[] 
	{
		255,
		224,
		192,
		160,
		128,
		96,
		64,
		32,
		0,
	};

	public static Color32 RandomColor()
	{
		int r, g, b;
		int lastIndex = possibleValues.Length - 1;

		r = possibleValues[Random.Range(0, lastIndex)];
		g = possibleValues[Random.Range(0, lastIndex)];
		b = possibleValues[Random.Range(0, lastIndex)];

		if (r != 255 && g != 255 && b != 255)
		{
			int max = GetLargestChannel(r, g, b);

			int dif = 255 - max;

			r += dif;
			g += dif;
			b += dif;
		}

		return new Color32((byte)r, (byte)g, (byte)b, 255);
	}

	private static int GetLargestChannel(int r, int g, int b)
	{
		int max = r;

		if (max < g)
		{
			max = g;
		}

		if (max < b)
		{
			max = b;
		}

		return max;
	}
}