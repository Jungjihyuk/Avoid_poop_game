using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance
    {
      get
      {
        if(_instance == null)
        {
          _instance = FindObjectOfType<GameManager>();
        }
        return _instance;
      }
    }

    [SerializeField]
    private GameObject poop;

    [SerializeField]
    private GameObject poop2;

    [SerializeField]
    private GameObject poop3;

    public int score;

    [SerializeField]
    private Text scoreTxt;

    [SerializeField]
    private Text bestScore;
    [SerializeField]
    private Text best;

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private GameObject heart1, heart2, heart3;
    private static int health = 0;

    public AudioSource audioSource;

    void Awake()
    {
      PlayerPrefs.SetInt("BestScore", 0);
      PlayerPrefs.SetInt("Best", 0);
      audioSource.playOnAwake = true;
    }

    void Start()
    {
      Screen.SetResolution(768, 1024, false);
      audioSource = GetComponent<AudioSource>();
      audioSource.Play();
    }

    void Update()
    {

    }

    public bool stopTrigger = true;
    public void GameOver()
    {
      stopTrigger = false;
      StopCoroutine(CreatepoopRoutine());
      panel.SetActive(true);

      if(score>=PlayerPrefs.GetInt("BestScore",0) && score>=PlayerPrefs.GetInt("Best", 0))
      {
        PlayerPrefs.SetInt("BestScore",score);
        bestScore.text = PlayerPrefs.GetInt("BestScore",0).ToString();

        PlayerPrefs.SetInt("Best", score);
        best.text = PlayerPrefs.GetInt("Best", 0).ToString();
      }
      audioSource.Stop();
    }

    public void GameStart()
    {
      score = 0;
      scoreTxt.text = "Score:  " + score;
      stopTrigger = true;
      StartCoroutine(CreatepoopRoutine());
      panel.SetActive(false);

      if(health<=0) health = 3;
      heart1.gameObject.SetActive(true);
      heart2.gameObject.SetActive(true);
      heart3.gameObject.SetActive(true);
    }

    public void Score()
    {
      if(stopTrigger)
      score++;
      scoreTxt.text= "Score: " + score;
    }

    public void Life()
    {
      if (health==0){
        GameOver();
      } else{
        health -=1;
        switch(health){
          case 3:
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart3.gameObject.SetActive(true);
            break;
          case 2:
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart3.gameObject.SetActive(false);
            break;
          case 1:
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(false);
            heart3.gameObject.SetActive(false);
            break;
          case 0:
            heart1.gameObject.SetActive(false);
            heart2.gameObject.SetActive(false);
            heart3.gameObject.SetActive(false);
            break;
        }
      }
    }

    IEnumerator CreatepoopRoutine()
    {
      while(stopTrigger)
      {
        CreatePoop();
        yield return new WaitForSeconds(0.1f);
        if(score>30){
          CreatePoop2();
        }
        yield return new WaitForSeconds(0.1f);
        if(score>60){
          CreatePoop3();
        }
        yield return null;
        if(score>100){
          SceneManager.LoadScene("EndScene");
        }
      }
    }

    public void CreatePoop()
    {
      Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0.0f,1.0f),1.1f,0));
      pos.z = 0.0f;
      Instantiate(poop, pos, Quaternion.identity);
    }
    private void CreatePoop2()
    {
      Vector3 pos2 = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0.0f,1.0f),1.3f,0));
      pos2.z = 0.0f;
      Instantiate(poop2, pos2, Quaternion.identity);
    }

    private void CreatePoop3()
    {
      Vector3 pos3 = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0.0f,1.0f),2f,0));
      pos3.z = 0.0f;
      Instantiate(poop3, pos3, Quaternion.identity);
    }
}
