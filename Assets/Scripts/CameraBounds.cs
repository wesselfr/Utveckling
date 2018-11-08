using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour {

    [SerializeField]
    private Bounds m_Bound;

	public Bounds bound
    {
        get { return m_Bound; }
    }

    public void SetBoundPosition(Vector3 position)
    {
        m_Bound.center = position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(m_Bound.center, m_Bound.size);
    }
}
