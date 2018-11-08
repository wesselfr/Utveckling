using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPoints : MonoBehaviour {

    [SerializeField]
    private bool m_IsProvider, m_IsProvided;

    private CircleCollider2D m_Collider;

    [SerializeField]
    private bool m_IsConnected;
    [SerializeField]
    private bool m_Transporting;

    [SerializeField]
    private Village m_Village;

    [SerializeField]
    private LineType m_Type;

    [SerializeField]
    private Source m_SourceIfProviding;

    private Source m_Source;

    private SpriteRenderer m_Renderer;

	// Use this for initialization
	void Start () {
        m_Collider = GetComponent<CircleCollider2D>();
        m_Renderer = GetComponent<SpriteRenderer>();

        if(m_SourceIfProviding != null)
        {
            m_Source = m_SourceIfProviding;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0) && !m_IsConnected)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MouseLine line = ServiceProvider.instance.mouseLine;
            mousePos.z = 0;
            if (m_Collider.OverlapPoint(mousePos)){
                CameraMovement.m_CanMove = false;
                if (line.selected == false)
                {
                    line.positionA = this.position;
                    line.selected = true;
                }
                line.AddPoint(this);
            }
            if (line.positionA != Vector3.zero)
            {
                line.positionB = mousePos;
            }



        }	
        else if (Input.GetMouseButtonUp(0))
        {
            CameraMovement.m_CanMove = true;
            MouseLine line = ServiceProvider.instance.mouseLine;
            line.positionB = Vector3.zero;
            line.Reset();
        }

        if (m_IsConnected)
        {
            m_Renderer.enabled = false;
        }
        else
        {
            m_Renderer.enabled = true;
        }
	}

    public void Disconnect()
    {
        m_Transporting = false;
        m_IsConnected = false;
        m_IsProvided = false;
    }

    /// <summary>
    /// Returns the position of the point.
    /// </summary>
    public Vector3 position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    public bool isConnected
    {
        get { return m_IsConnected; }
        set { m_IsConnected = value; }
    }

    public LineType type
    {
        get { return m_Type; }
    }

    public bool isProviding
    {
        get
        {
            if(m_IsProvider || isTransporting || m_IsProvided)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool isTransporting
    {
        get { return m_Transporting; }
        set { m_Transporting = value; }
    }

    public bool isProvided
    {
        get { return m_IsProvided; }
        set { m_IsProvided = value; }
    }

    public Source source
    {
        get { return m_Source; }
        set { m_Source = value; }
    }

    public Village isConnectedToVillage
    {
        get { if (m_Village != null) { return m_Village; } else { return null; } }
    }

}
