using System.Collections.Generic;
using UnityEngine;

public class Floor
{
    private readonly Dictionary<string, Cell> _cells = new Dictionary<string, Cell>();
    private readonly Dictionary<Vector3Int, Cell> _cellsByCoord = new Dictionary<Vector3Int, Cell>();
    private readonly List<Cell> _stairs = new List<Cell>();
    private readonly Transform _floorTransform;
    private readonly int _floorIndex;
	private List<string> cellsNames = new List<string>();
    public Floor(Transform floor, int floorIndex)
    {
        _floorIndex = floorIndex;
        _floorTransform = floor;
        FillCell(floor);
    }

    private void FillCell(Transform floor)
    {
        for (var i = 0; i < floor.childCount; i++)
        {
            var cell = floor.GetChild(i).GetComponent<Cell>();
            var coord = Map.ConvertGlobalPosToMapPos(cell.transform.position, _floorIndex);
            coord.y *= -1;
            cell.SetCoord(coord);
            if (cell.IsStairs())
                _stairs.Add(cell);
            _cells[cell.GetCellName()] = cell;
            _cellsByCoord[cell.GetVector3()] = cell;
			string name = cell.GetCellName();
			if (name != "" && !cell.IsStairs())
				cellsNames.Add(name);
        }
    }

    public Cell GetCellByName(string cellName)
    {
        return _cells.ContainsKey(cellName) ? _cells[cellName]: null ;
    }

    public Cell GetCellByCoord(Vector3Int coord)
    {
        return _cellsByCoord.ContainsKey(coord) ? _cellsByCoord[coord] : null;
    }

    public Cell GetClosestStair(Vector3 player)
    {
		var closestIndex = 0;
		for (int i = 0; i < _stairs.Count; i++)
		{
			if (Vector3.Distance(player, _stairs[i].transform.position) < Vector3.Distance(player, _stairs[closestIndex].transform.position))
				closestIndex = i;
		}
        return _stairs.Count > 0 ? _stairs[closestIndex] : null;
    }

    public Transform GetFloorTransform() => _floorTransform;

    public int GetFloorIndex() => _floorIndex;

	public List<string> GetCellsNames()
	{
		return cellsNames;
	}
}