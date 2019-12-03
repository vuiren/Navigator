using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NameKeeperController : MonoBehaviour
{
	[SerializeField]
	bool doIt;
	static List<List<string>> floorsCells = new List<List<string>>();
	static List<String> namedCells=new List<String>();

	private void Awake()
	{
		DoIt();
	}

	private void DoIt()
	{
		var floors = Map.GetFloors();
		for (int i = 0; i < floors.Count; i++)
		{
			Floor e = floors[i];
			List<string> list = e.GetCellsNames().OrderBy(x => x).Select(x => x).ToList();
			floorsCells.Add(list);
			var z = e.GetCellsNames();
			namedCells.AddRange(e.GetCellsNames().OrderBy(x=>x).Select(x=>x));
		}
	}

	public static List<String> GetCells() => namedCells;

	public static List<List<string>> GetAllCells() => floorsCells;
}
