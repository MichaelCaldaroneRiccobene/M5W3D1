using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PL_Rb : MonoBehaviour
{
    public int HP;
    [SerializeField] private float speed = 6f;
    [SerializeField] private TextMeshProUGUI hp;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
        hp.text = HP.ToString();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0; right.y = 0;

        Vector3 dir = forward * y + right * x;

        dir.Normalize();

        if (dir.sqrMagnitude > 0.01f) transform.forward = dir;

        Vector3 OnDirection = new Vector3(dir.x, rb.velocity.y, dir.z) * speed;
        rb.MovePosition(rb.position +  OnDirection * Time.fixedDeltaTime);
    }
} 
