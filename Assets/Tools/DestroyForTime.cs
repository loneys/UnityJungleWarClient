﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyForTime : MonoBehaviour {

    public float time = 1;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, time);
	}

}
