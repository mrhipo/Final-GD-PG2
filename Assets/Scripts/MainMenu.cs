using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //Cargar escenas.
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
