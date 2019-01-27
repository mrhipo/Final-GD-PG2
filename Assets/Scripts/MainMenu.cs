using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject main;
    public GameObject loading;
    public GameObject credtis;

    public AudioSource click;
	
    public void NewGame()
    {
        click.Play();
        main.SetActive(false);
        loading.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void Multiplayer()
    {
        click.Play();
        //TO DO
    }

    public void Credits()
    {
        click.Play();
        credtis.SetActive(true);
    }

    public void Back()
    {
        click.Play();
        credtis.SetActive(false);
    }
}
