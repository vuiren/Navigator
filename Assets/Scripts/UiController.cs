using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField] private InputField inputField;
	[SerializeField] private TMP_Dropdown auditoriesList;
	[SerializeField] MovementController movementController;

	private void Start()
	{
		auditoriesList.options = GetOptionDatas(NameKeeperController.GetCells());
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

	public void ChangeFinal()
    {
		movementController.SetFinal(inputField.text);
    }

	public void ChangeSceneToQRRead()
	{
		SceneController.ChangeScene();
	}


}
