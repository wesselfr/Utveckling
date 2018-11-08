using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour {

    [SerializeField]
    private Village[] m_Villages;

    [SerializeField]
    AudioSource m_Sound;

    bool m_SoundPlayed = false;

    [SerializeField]
    private int m_LoadLevelOnCompletion;

    // Use this for initialization
    void Start() {
        m_Sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (CheckMission())
        {
            Debug.Log("Great Job!");
            if (!m_Sound.isPlaying && !m_SoundPlayed)
            {
                m_Sound.Play();
                m_SoundPlayed = true;
                Instantiate(Resources.Load("NextLevelMenu"));
            }
        }
    }

    public void CheckConnections()
    {
        for(int i = 0; i < m_Villages.Length; i++)
        {
            m_Villages[i].DisconnectAll();
            m_Villages[i].CheckConnections();
        }
    }

    public bool CheckMission()
    {
        bool stillConnected = true;
        //Check all villages, if a village is not connected the puzzle is not done.
        for(int i = 0; i < m_Villages.Length; i++)
        {
            if(m_Villages[i].CheckObjectives() == false)
            {
                stillConnected = false;
            }
        }
        return stillConnected;

    }

    public int levelToLoad
    {
        get { return m_LoadLevelOnCompletion; }
    }
}
