using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public float baseSize = 1f;

    void Awake()
    {
        GetComponent<Camera>().orthographicSize = baseSize * (16f / 9f) / Camera.main.aspect;
    }

}
