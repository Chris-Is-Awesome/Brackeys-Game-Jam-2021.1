using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author(s): Derp Princess (Allison Mackenzie Johnson)
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    [Header("GameObjects and Components")]
    [SerializeField] private GameObject leftBound;
    [SerializeField] private GameObject rightBound;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D box;

    [Header("Movement")]
    [SerializeField] private Vector2 position;
    [SerializeField] private int facing; // -1 = left, 1 = right
    [SerializeField] private float speed;
    

    public void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        box = gameObject.GetComponent<BoxCollider2D>();
        speed = 2f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (facing == 0)
        {
            Debug.LogError("Give Snake a facing in inspector of 1 (right) or -1 (left)");
        }
        if(facing == 1)
        {
            transform.localScale = new Vector2(-1, 1);
            transform.Translate((Vector2.right * speed) * Time.deltaTime);
        } else if (facing == -1)
        {
            transform.localScale = new Vector2(1, 1);
            transform.Translate((Vector2.left * speed) * Time.deltaTime);

        }
    }

    private void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision of Snek with Player || Take 1 damage to HP");
            obj.gameObject.GetComponent<Entity>().stats.SetCurrHp(-1, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj.CompareTag("Enemy Bound"))
        {
            if (facing == 1) //right to left face
            {
                facing = -1;
            } else if (facing == -1) //left to right face
            {
                facing = 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
