using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// destroy self after 5 seconds of appearing
public class Explosion : MonoBehaviour { void Start() { Destroy(this.gameObject, 5.0f); } }
