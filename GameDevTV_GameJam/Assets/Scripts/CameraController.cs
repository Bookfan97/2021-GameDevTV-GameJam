using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public GenerateEnvironment Environment;
    public float speed = 2.0f;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;
    Vector3 movement;
    
    // Use this for initialization
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        Environment = FindObjectOfType<GenerateEnvironment>();
        movement = target.position;
    }
    
    // LateUpdate is called once per frame after Update
    void LateUpdate()
    {
        if (target != null)
        {
            transform.Translate(movement * speed * Time.deltaTime, Space.Self);
            transform.position = new Vector3(
                Mathf.Clamp(target.position.x, 0, Environment.floorX),
                Mathf.Clamp(target.position.y, 0, Environment.floorY),
                -10f);
        }
    }
}
