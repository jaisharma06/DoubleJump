using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions{

	public static T GetRandom<T>(this List<T> list)
	{
		return list[Random.Range(0, list.Count)];
	}

	public static T GetRandom<T>(this List<T> list, int lowerLimit, int upperLimit)
	{
		return list[Random.Range(lowerLimit, upperLimit)];
	}
}
