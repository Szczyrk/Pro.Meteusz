using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

	public GameObject objectToFollow;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - objectToFollow.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = objectToFollow.transform.position + offset;
	}
}
