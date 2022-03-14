using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float paddingLeft = 0.6f;
    [SerializeField] float paddingRight = 0.6f;
    [SerializeField] float paddingTop = 2f;
    [SerializeField] float paddingBottom = 3f;
    Vector2 rawInput;
    Vector2 minBounds;
    Vector2 maxBounds;
    Shooter shooter;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    void Start()
    {
        initBounds();
    }

    void Update()
    {
        Move();
    }

    void initBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPosition = new Vector2();
        newPosition.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPosition.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);

        transform.position = newPosition;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if (shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }
}
