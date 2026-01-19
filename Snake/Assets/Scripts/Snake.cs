using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();

    public Transform _segmentPrefab;

    public int InitalSize = 4;


    private void Start()
    {
        ResetState();
    }


    private void Update()
    {
        if (Keyboard.current.wKey.isPressed)
        {
            _direction = Vector2.up;
        }

        if (Keyboard.current.sKey.isPressed)
        {
            _direction = Vector2.down;
        }

        if (Keyboard.current.aKey.isPressed)
        {
            _direction = Vector2.left;
        }

        if (Keyboard.current.dKey.isPressed)
        {
            _direction = Vector2.right;
        }
    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );
    }

    private void Grow()
    {
        Transform segment = Instantiate(this._segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }

    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(this.transform);

        for (int i = 1; i < this.InitalSize; i++)
        {
            _segments.Add(Instantiate(this._segmentPrefab));
        }

        this.transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            Grow();
        }

        if (other.CompareTag("Obstacle"))
        {
            ResetState();
        }
    }
}
