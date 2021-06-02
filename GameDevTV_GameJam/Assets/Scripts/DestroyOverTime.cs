using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    [SerializeField] float lifeTime;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifeTime);
    }
}
