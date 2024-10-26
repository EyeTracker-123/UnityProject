using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    Vector3 _velocity;
    public float move_speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // Rigidbody rb = GetComponent<Rigidbody>();
       // rb.velocity = _velocity * move_speed;

        gameObject.transform.position += _velocity * move_speed;
    }
    void OnMove(InputValue value)
    {
        var axis = value.Get<Vector2>();
        _velocity = new Vector3(axis.x,0, axis.y);
    }
}
