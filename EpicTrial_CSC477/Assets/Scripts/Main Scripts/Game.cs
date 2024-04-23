using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public static Game Instance { get; private set; }

    // Start is called before the first frame update
    void Start() {
        input = new Input();
        input.Enable();
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
