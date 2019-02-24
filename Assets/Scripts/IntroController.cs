using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public List<Sprite> images;
    public List<string> texts;

    public Image slide;
    public Image fade;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Slide());
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopCoroutine(Slide());
            SceneManager.LoadScene(2);
        }

    }

    public IEnumerator Slide()
    {
        for (int i = 0; i < images.Count; i++)
        {
            slide.sprite = images[i];
            text.text = texts[i];
            fade.CrossFadeAlpha(0f, 10f, true);
            yield return new WaitForSeconds(10f);
            fade.CrossFadeAlpha(1f, 3f, true);
            yield return new WaitForSeconds(3f);
        }

        SceneManager.LoadScene(2);  
    }

}
