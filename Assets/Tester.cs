using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
	public Stat stat;

	public ResourceStat resource;

	// Start is called before the first frame update
	void Start()
	{
		//stat.onBaseValueChanged.AddListener(OnChanged);
		stat.BaseValue = 1;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OnChanged(ValueChangedEvent e)
	{
		Debug.Log(e);
	}
}
