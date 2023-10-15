using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	//[SerializeField] AudioSource sound;
	// Use this for initialization
	void Start () {
		//sound.Play();
        Destroy(this.gameObject, .5f);
	}
	
}
