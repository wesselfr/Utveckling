using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LineType
{
Water,
Electricity
}

[RequireComponent(typeof(LineRenderer))]
public class GenericLine : MonoBehaviour {

    [SerializeField]
    private GenericPoints m_A;
    [SerializeField]
    private GenericPoints m_B;

    private GameObject m_LineEndA, m_LineEndB;

    private bool m_Connected;

    public LineType m_Type;

    private LineRenderer m_Renderer;
    private EdgeCollider2D m_Collider;

    public GenericLine(GenericPoints a, GenericPoints b)
    {
        m_A = a;
        m_B = b;

        SpawnPipeEnd();

        m_A.isConnected = true;
        m_B.isConnected = true;
    }

    public void Initialize(GenericPoints a, GenericPoints b)
    {
        m_A = a;
        m_B = b;
        CheckDrawingSide();
        SpawnPipeEnd();
        CheckTransporting();
        SpawnParticle();
        m_A.isConnected = true;
        m_B.isConnected = true;

        if(m_A.source != null)
        {
            m_B.source = m_A.source;
        }
        else if(m_B.source != null)
        {
            m_A.source = m_B.source;
        }

    }

    //Check if the pipe is being drawn from the right side.
    void CheckDrawingSide()
    {
        if(m_A.position.x < m_B.position.x)
        {
            //Flip points
            GenericPoints a = m_A;
            GenericPoints b = m_B;

            m_A = b;
            m_B = a;

        }
    }

    // Use this for initialization
    void SpawnPipeEnd () {
        m_Type = m_A.type;

        m_Renderer = GetComponent<LineRenderer>();
        m_Renderer.SetPosition(0, m_A.position);
        m_Renderer.SetPosition(1, m_B.position);

        GameObject lineEnd = null;
        Material lineMaterial = null;
        switch (m_Type)
        {
            case LineType.Water:
                lineEnd = Resources.Load("Pipe End") as GameObject;
                lineMaterial = Resources.Load("PipeMaterial") as Material;
                break;
            case LineType.Electricity:
                lineEnd = Resources.Load("Wire End") as GameObject;
                lineMaterial = Resources.Load("CableMaterial") as Material;
                break;
        }

        m_Renderer.material = lineMaterial;

        GameObject pipeA = Instantiate(lineEnd, m_Renderer.GetPosition(0), Quaternion.identity);
        GameObject pipeB = Instantiate(lineEnd, m_Renderer.GetPosition(1), Quaternion.identity);

        Vector3 vectorToTarget = m_B.position - m_A.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion a = Quaternion.AngleAxis(angle, Vector3.forward);
        pipeA.transform.rotation = Quaternion.RotateTowards(pipeA.transform.rotation, a, 360f);

        vectorToTarget = m_A.position - m_B.position;
        angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion b = Quaternion.AngleAxis(angle, Vector3.forward);
        pipeB.transform.rotation = Quaternion.RotateTowards(pipeB.transform.rotation, b, 360f);

        if (m_Type == LineType.Water)
        {
            pipeB.GetComponent<SpriteRenderer>().flipY = false;
        }
        else
        {
            pipeA.GetComponent<SpriteRenderer>().flipY = true;
        }

        m_LineEndA = pipeA;
        m_LineEndB = pipeB;

        m_Collider = GetComponent<EdgeCollider2D>();

        UpdateCollider();

    }

    public void SpawnParticle()
    {
        GameObject particle;
        switch (m_Type)
        {
            case LineType.Water:
                particle = Resources.Load("BuildPipeEffect") as GameObject;
                Instantiate(particle, m_A.position + ((m_B.position - m_A.position) / 2), Quaternion.identity);
                break;
            case LineType.Electricity:
                particle = Resources.Load("BuildCableeffect") as GameObject;
                Instantiate(particle, m_A.position + ((m_B.position - m_A.position) / 2), Quaternion.identity);
                break;
        }
    }

    void CheckTransporting()
    {
        if(m_A.isConnected && m_B.isConnected)
        {
            if(m_A.isProviding || m_B.isProviding)
            {
                m_A.isTransporting = true;
                m_A.isProvided = true;
                m_B.isTransporting = true;
                m_B.isProvided = true;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {

        //Debug Only!
        m_Renderer.SetPosition(0, m_A.position);
        m_Renderer.SetPosition(1, m_B.position);

        UpdateCollider();

        if(m_A.isProviding || m_B.isProviding)
        {
            m_A.isTransporting = true;
            m_B.isTransporting = true;  
        }

        if (Input.GetMouseButtonDown(1))
        {

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            if (gameObject.GetComponent<Renderer>().bounds.Contains(mousePos))
            {
                m_A.Disconnect();
                m_B.Disconnect();

                m_A.isTransporting = false;
                m_B.isTransporting = false;

                ServiceProvider.instance.puzzleManager.CheckConnections();

                Destroy(m_LineEndA);
                Destroy(m_LineEndB);


                Destroy(this.gameObject);
            }
        }
    }

    public void RemovePipe()
    {

    }

    public void UpdateCollider()
    {

        Vector2[] colliderBound =
        {
            m_A.position,
            m_B.position
        };

        m_Collider.points = colliderBound;

    }

    public LineType type
    {
        get { return m_Type; }
    }

    public void OnDrawGizmosSelected()
    {

    }
}
