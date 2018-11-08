using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceProvider : MonoBehaviour {

    public static ServiceProvider instance;

    [SerializeField]
    private Player m_Player;

    [SerializeField]
    private MouseLine m_MouseLine;

    [SerializeField]
    private PuzzleManager m_PuzzleManager;

	// Use this for initialization
	void Start () {
		if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
	}
	
    public Player player
    {
        get { return m_Player; }
    }

    public MouseLine mouseLine
    {
        get { return m_MouseLine; }
    }

    public PuzzleManager puzzleManager
    {
        get { return m_PuzzleManager; }
    }
}
