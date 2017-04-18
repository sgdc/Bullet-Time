using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour {

	private bool isHeld;

	// Use this for initialization
	void Start ()
	{
		isHeld = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	/// <summary>
	/// Attaches the flag to the player.
	/// </summary>
	/// <param name="holder">The gameobject in which the flag will become the child of.</param>
	public void GrabFlag(GameObject holder)
	{
		transform.SetParent(holder.transform);
		isHeld = true;
	}

	public void DropFlag()
	{
		transform.SetParent(null);
		isHeld = false;
	}
}
