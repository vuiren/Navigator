using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TurnOffAllTexts : MonoBehaviour
{
	[SerializeField] Transform text;
	[SerializeField] bool doIT;
	private void Update()
	{
		if (doIT)
			DOIT();
	}

	private void DOIT()
	{
		for(int i=0;i<text.childCount;i++)
		{
			var child = text.GetChild(i).GetComponent<Floor>();
		}
	}
}
