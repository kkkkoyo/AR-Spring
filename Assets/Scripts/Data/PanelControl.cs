using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelControl : MonoBehaviour {

	public PanelSlider[] panel;


	public void PanelIn(int num)
	{
		panel[num].SlideIn();
	}
	public void PanelOut(int num)
	{
		panel[num].SlideOut();
	}
}
