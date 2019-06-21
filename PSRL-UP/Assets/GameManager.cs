using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Transform selected;

    public Animator pauseScreen;

    public static bool gamePaused = false;

    UI_Selected _US;
    MapManager _MM;

    private void Awake()
    {
        _US = FindObjectOfType<UI_Selected>();
        _MM = FindObjectOfType<MapManager>();

        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1.5f);

        FindObjectOfType<UI_Interactions>().ShowTurnBanner("player turn");

        LightMyCharacters();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(FindObjectOfType<TileActions>().attacking == true)
            {
                FindObjectOfType<TileActions>().DisableAttacking();
            }
            else
            {
                selected = null;
                LightMyCharacters();
                _US.Close();
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        gamePaused = !gamePaused;

        pauseScreen.SetBool("OnOff", gamePaused);

    }

    public static void Select(Transform toSet)
    {
        selected = toSet;
        FindObjectOfType<UI_Selected>().Open();
    }

    public void EndTurn()
    {
        if(FindObjectOfType<AI>().CheckIfAnyLeft() == false)
        {
            EndGame();
        }

        if (FindObjectOfType<TileActions>().aiIsPlaying == false)
        {
            Entity[] entities = FindObjectsOfType<Entity>();

            foreach (Entity e in entities)
            {
                e.ResetTurn();
            }
            _MM.UnlitTiles();
            selected = null;
            _US.Close();

            FindObjectOfType<AI>().StartAiTurn();
            FindObjectOfType<UI_Interactions>().ShowTurnBanner("enemy turn");
            FindObjectOfType<TileActions>().aiIsPlaying = true;
        }

    }

    void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EntityDied()
    {

    }

    public void EndAiTurn()
    {
        FindObjectOfType<TileActions>().aiIsPlaying = false;
        FindObjectOfType<UI_Interactions>().ShowTurnBanner("player turn");
        LightMyCharacters();
    }

    public void LightMyCharacters()
    {
        _MM.UnlitTiles();

        Entity[] entities = FindObjectsOfType<Entity>();

        foreach(Entity e in entities)
        {
            if(e.myOwner == Owner.Player)
            {
                e.curr_Node.LightUp();
            }
        }
    }
}
