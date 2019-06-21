using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Changer : MonoBehaviour
{
    string toLoad = null;

    Animator _Animator;

    private void Awake()
    {
        _Animator = GetComponent<Animator>();
    }

    public void LoadLevel(string levelName)
    {
        if(toLoad == null)
        {
            toLoad = levelName;

            _Animator.SetTrigger("Fade_In");

            Invoke("SwitchScene", 1f);
        }
    }

    void SwitchScene()
    {
        SceneManager.LoadScene(toLoad);

        toLoad = null;
    }
}
