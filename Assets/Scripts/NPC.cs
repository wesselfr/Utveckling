using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action
{
    Idle,
    Walking,
    Jump
}

public enum Emotion
{
    Happy,
    Sad
}

public class NPC : MonoBehaviour {

    Animator m_Animator;

    [SerializeField]
    private Village m_Village;

    [SerializeField]
    private Action m_Action;

    [SerializeField]
    private Emotion m_Emotion;

    [SerializeField]
    private float m_Speed;

    private float m_WaitTime;

    private Vector2 m_Direction;

    private Rigidbody2D m_Rigidbody;

    private SpriteRenderer m_Renderer;

	// Use this for initialization
	void Start () {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        m_WaitTime -= Time.deltaTime;
        if(m_WaitTime<= 0)
        {
            m_Direction = Vector2.zero;
            m_WaitTime = Random.Range(1f, 4f);
            m_Action = (Action) Mathf.RoundToInt(Random.Range(0, 2));


            if(m_Action == Action.Walking)
            {
                m_Direction = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
            }

        }

        switch (m_Village.happy)
        {
            case true:
                m_Emotion = Emotion.Happy;
                break;
            case false:
                m_Emotion = Emotion.Sad;
                break;
        }

        if(m_Action == Action.Walking)
        {
            transform.Translate((m_Direction.normalized * m_Speed) * Time.deltaTime);
            //m_Rigidbody.velocity = (m_Direction.normalized * m_Speed) * Time.deltaTime; 
        }

        if(m_Direction.x > 0)
        {
            m_Renderer.flipX = true;
        }
        else
        {
            m_Renderer.flipX = false;
        }
        
        m_Animator.SetFloat("Actions", (int)m_Action);
        m_Animator.SetFloat("Emotion", (int)m_Emotion);

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            if (m_Renderer.bounds.Contains(mousePosition))
            {
                CameraMovement.m_CanMove = false;
                DragNPC(mousePosition);
            }
        }
        else
        {
            m_Animator.SetBool("Jump", false);
        }
	}

    public void DragNPC(Vector3 position)
    {
        OnMouseDown();
        transform.position = position;
    }

    private void OnMouseDown()
    {
        m_Animator.SetBool("Jump", true);
        m_Action = Action.Idle;
        if (m_WaitTime < 1)
        {
            m_WaitTime = 1f;
        }
    }
}
