﻿using UnityEngine;
using System.Collections;

public class Grass : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.tag.Equals("Player"))
		{
			GetComponent<Animator>().SetTrigger("Shake");
		}
	}
}
