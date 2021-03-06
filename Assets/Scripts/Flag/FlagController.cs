﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FlagController : NetworkBehaviour {

	[SerializeField]
	private GameObject returnLocation;

	private bool isHeld;
	private float returnTimer;

	private const float RETURN_TIME = 10f;

	// Use this for initialization
	void Start ()
	{
		isHeld = false;
		returnTimer = 0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (isServer)
        {
            if (returnTimer >= RETURN_TIME)
            {
                ReturnFlag();
                returnTimer = 0f;
            }

            if (isHeld)
            {
                returnTimer = 0;
            }
            else if (transform.parent != returnLocation.transform)
            {
                returnTimer += Time.deltaTime;
            }
        }
	}

	/// <summary>
	/// Attaches the flag to the player.
	/// </summary>
	/// <param name="holder">The gameobject in which the flag will become the child of.</param>
	public void GrabFlag(GameObject holder)
	{
		transform.SetParent(holder.transform);
		transform.localPosition = new Vector3(0f, -0.5f, -0.3f);
		isHeld = true;
	}

	public void DropFlag()
	{
		transform.SetParent(null);
		isHeld = false;
	}

	private void ReturnFlag()
	{
		transform.SetParent(returnLocation.transform);
		transform.localPosition= new Vector3(0f, -0.25f, 0f);
	}

    void OnTriggerEnter(Collider other)
    {
        if (isServer)
        {
            if (other.tag.Contains("Player"))
            {
                GrabFlag(other.gameObject);
                other.GetComponent<Player>().Flag = this;
            }
        }
    }
}
