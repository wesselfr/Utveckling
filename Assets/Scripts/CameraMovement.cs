using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    Vector3 m_StartMousePos;

    [SerializeField]
    private CameraBounds m_CamBounds;

    [SerializeField]
    private CameraBounds m_CenterCamBounds;

    public static bool m_CanMove = true;
	
	// Update is called once per frame
	void Update () {
        Vector3 newBoundPosition = transform.position;
        newBoundPosition.z = 0;
        m_CenterCamBounds.SetBoundPosition(newBoundPosition);
        if (m_CanMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_StartMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                m_StartMousePos.z = 0;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 currentMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition); ;
                currentMouse.z = 0;
                Vector3 delta = m_StartMousePos - currentMouse;
                //delta = delta.normalized;

                transform.position = Vector3.Lerp(transform.position, transform.position + delta, 0.25f);
                //transform.position = Vector3.Lerp(transform.position, transform.position + (delta * m_Speed), 0.2f);

                //transform.Translate(-delta.normalized * m_Speed * Time.deltaTime);

            }

        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 currentMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                currentMouse.z = 0;
                if (!m_CenterCamBounds.bound.Contains(currentMouse))
                {
                    Vector3 delta = m_CenterCamBounds.bound.center - currentMouse;
                    delta = delta.normalized;

                    Debug.DrawLine(m_CenterCamBounds.bound.center, currentMouse);

                    transform.position = Vector3.Lerp(transform.position, transform.position - delta , 0.15f);
                    //transform.position = Vector3.Lerp(transform.position, transform.position + (delta * m_Speed), 0.2f);

                    //transform.Translate(-delta.normalized * m_Speed * Time.deltaTime);
                }

            }
        }


        if (!m_CamBounds.bound.Contains(transform.position))
        {
            //transform.position = m_CamBounds.bound.ClosestPoint(transform.position);
            transform.position = Vector3.Lerp(transform.position, m_CamBounds.bound.ClosestPoint(transform.position), 0.25f);
        }

    }
}
