using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private  Vector2 _direction = Vector2.right;
    
    private List<Transform> _segments;
    
    public Transform segmentPrefab;
    
    private int previousMove=0;
    
    private void Start()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (previousMove != 1)
            {
                _direction=Vector2.up;
                previousMove = 2;
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (previousMove != 2)
            {
                _direction=Vector2.down;
                previousMove = 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (previousMove != 3)
            {
                _direction = Vector2.left;
                previousMove = 4;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (previousMove != 4)
            {
                _direction=Vector2.right;
                previousMove = 3;
            }
        }
    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x )+ _direction.x,
            Mathf.Round(this.transform.position.y )+ _direction.y,
            0.0f
        );
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        
        _segments.Add(segment);
    }

    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {    Destroy(_segments[i].gameObject);
            
        }
        _segments.Clear();
        _segments.Add(this.transform);
        
        this.transform.position=Vector3.zero;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
        }
        else if (other.tag == "Food_2")
        {
            Grow();
            Grow();
        }
        else if (other.tag == "Jaguar")
        {
            ResetState();
        }
        else if (other.tag == "Wall")
        {
            ResetState();
        }
    }
}

