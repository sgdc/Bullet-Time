using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPoleDetection : MonoBehaviour {

	private FlagController fc;

	void Start()
	{
		fc = transform.parent.gameObject.GetComponent<FlagController>();
	}
}
