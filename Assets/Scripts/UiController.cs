using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
	[SerializeField] private InputField inputField;
	[SerializeField] private TMP_Dropdown auditoriesList;
	[SerializeField] private List<TMP_Dropdown> floorsDropDownList = new List<TMP_Dropdown>();
	[SerializeField] MovementController movementController;

	private void Start()
	{
		//auditoriesList.options = GetOptionDatas(NameKeeperController.GetCells());
		SetDropDowns();
	}

	private void SetDropDowns()
	{
		var allCells = NameKeeperController.GetAllCells();
		for (int i = 0; i < floorsDropDownList.Count; i++)
		{
			TMP_Dropdown e = (TMP_Dropdown)floorsDropDownList[i];
			if (e == null) continue;
			if (i >= allCells.Count) continue;
			List<string> cells = allCells[i];
			e.options = GetOptionDatas(cells);
		}
	}

	private List<TMP_Dropdown.OptionData> GetOptionDatas(List<string> cells)
	{
		var result = new List<TMP_Dropdown.OptionData>();
		foreach (var e in cells)
		{
			result.Add(new TMP_Dropdown.OptionData(e));
		}
		return result;
	}

	public void ChangeFinalFromDropDown(int i)
	{
		var list = floorsDropDownList[i];
		movementController.SetFinal(list.options[list.value].text);
	}

	public void ChangeFinal()
	{
		movementController.SetFinal(inputField.text);
	}

	public void ChangeSceneToQRRead()
	{
		SceneController.ChangeScene();
	}
}
