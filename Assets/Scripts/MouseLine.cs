using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MouseLine : MonoBehaviour {

    private Vector3 m_PositionA;
    private Vector3 m_PositionB;

    private GenericPoints m_GenericPointA;
    private GenericPoints m_GenericPointB;

    private LineRenderer m_Renderer;

    private Material m_Material;

    private bool m_Selected = false;

    public void Start()
    {
        m_Renderer = GetComponent<LineRenderer>();
    }

    public void Update()
    {
        m_Renderer.SetPosition(0,m_PositionA);
        m_Renderer.SetPosition(1, m_PositionB);

        if (m_GenericPointA != null)
        {
            switch (m_GenericPointA.type)
            {
                case LineType.Water:
                    m_Material = Resources.Load("PipeMaterial") as Material;
                    m_Renderer.material = m_Material;
                    break;
                case LineType.Electricity:
                    m_Material = Resources.Load("CableMaterial") as Material;
                    m_Renderer.material = m_Material;
                    break;
            }
        }

        Debug.DrawRay(m_PositionA, m_PositionB - m_PositionA);

        if (m_GenericPointA != null && m_GenericPointB != null)
        {
            RaycastHit2D raycastHit;
            Ray ray = new Ray(m_GenericPointA.position, (m_GenericPointB.position - m_GenericPointA.position).normalized);
            raycastHit = Physics2D.Raycast(ray.origin, ray.direction, (m_PositionB - m_PositionA).magnitude);


            if (m_GenericPointA != m_GenericPointB)
            {
                if (m_GenericPointA.type == m_GenericPointB.type)
                {
                    if (raycastHit.collider != null)
                    {
                        Debug.Log(raycastHit.collider.gameObject.name);
                        Reset();
                    }
                    else if (raycastHit.collider == null)
                    {
                        GameObject LineObject = Instantiate(Resources.Load("Line"), Vector3.zero, Quaternion.identity) as GameObject;
                        GenericLine line = LineObject.GetComponent<GenericLine>();
                        line.Initialize(m_GenericPointA, m_GenericPointB);

                        Reset();
                    }
                }
            }
        }
    }

    public void Reset()
    {
        m_PositionA = Vector3.zero;
        m_PositionB = Vector3.zero;
        m_GenericPointA = null;
        m_GenericPointB = null;
        m_Selected = false;
    }

    public void AddPoint(GenericPoints point)
    {
        if(m_GenericPointA == null)
        {
            m_GenericPointA = point;
        }
        else if(m_GenericPointA != null && m_GenericPointB == null)
        {
            if (!(m_GenericPointA.isConnectedToVillage == point.isConnectedToVillage))
            {
                if (point.type == m_GenericPointA.type)
                {
                    m_GenericPointB = point;
                }
            }
        }
        else if(m_GenericPointA == m_GenericPointB)
        {
            m_GenericPointB = point;
        }
    }

    public bool selected
    {
        get { return m_Selected; }
        set { m_Selected = value; }
    }

    public Vector3 positionA
    {
        get { return m_PositionA; }
        set { m_PositionA = value; }
    }

    public Vector3 positionB
    {
        get { return m_PositionB; }
        set { m_PositionB = value; }
    }
}
