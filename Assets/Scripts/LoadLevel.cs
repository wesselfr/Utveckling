using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

    private int m_SceneToLoad;

    public void Load()
    {
        m_SceneToLoad = ServiceProvider.instance.puzzleManager.levelToLoad;
        SceneManager.LoadScene(m_SceneToLoad);
    }
}
