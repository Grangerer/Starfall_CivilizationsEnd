﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunParticleScale : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.transform.localScale = this.transform.parent.localScale;
	}
	

}
