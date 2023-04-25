using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    static public GameManager instance = null;

    [Header("-UI-")]
    [SerializeField] private TextMeshProUGUI timerTxt;
    [SerializeField] private TextMeshProUGUI fstSecTxt;
    [SerializeField] private TextMeshProUGUI sndSecTxt;
    [SerializeField] private TextMeshProUGUI fstMinTxt;
    [SerializeField] private TextMeshProUGUI sndMinTxt;
    [SerializeField] private TextMeshProUGUI sepratorTxt;
    [SerializeField] private TextMeshProUGUI hostCountTxt;
    [SerializeField] private Image powerImg;


    [Header("-REFRENCES-")]
    public PlayerCotroller player;
    public UnityEngine.Rendering.Universal.Light2D globalLight;
    public UnityEngine.Rendering.Universal.Light2D playerLight;
    public UnityEngine.Rendering.Universal.Light2D playerSpotLight;


    [Header("GAME-STATS")]
    public int population;
    private float timeScale = 1f;
    private float oT;


    [Header("-TIMER-")]
    [SerializeField] private float timerToReset = 300; // timer to reset the game from begining.
    [SerializeField] private float timer = 0; // timer for coding
    public bool extraTime = false;
    public bool isTimeStart = false;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        globalLight.intensity = 0f;
        playerLight.intensity = 0;
        playerSpotLight.intensity = 0;
        Time.timeScale = timeScale;
        player = player.GetComponent<PlayerCotroller>();
        oT = Time.timeScale;
    }


    // Update is called once per frame
    void Update()
    {

        //lights
        globalLight.intensity += 0.2f * Time.deltaTime;
        playerLight.intensity += 0.2f * Time.deltaTime;
        playerSpotLight.intensity += 0.3f * Time.deltaTime;

        if (globalLight.intensity >= 0.3f)
        {
            globalLight.intensity = 0.3f;
            playerLight.intensity = 0.39f;
            playerSpotLight.intensity = 0.7f;

        }

        //Timer
        TimerOnScreen(timerToReset - timer);

        if (isTimeStart)
            timer += 1f * Time.deltaTime;
        else
            timer = 0;
        if (timer >= timerToReset) 
        {
            extraTime = true;
            StartCoroutine(ExtraTimeToReset());
            timer = 0;
        }

        //controlls
        if (Input.GetKeyDown(KeyCode.Space) && player.power == PowerUps.SPEED)
        {
            Time.timeScale = timeScale * 3.5f;
            player.speedSFX.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            player.speedSFX.enabled = false;
            Time.timeScale = oT;
        }

        //checks for hostages
        if (population == player.hostsCount)
            player.hasAllHosts = true;

        //UI stuff for the HUD
        if(player.power== PowerUps.SPEED)
            powerImg.gameObject.SetActive(true);

        hostCountTxt.text = player.hostsCount.ToString();

        //End of Update
    }

    public void ResetDay()
    {
        SceneManager.LoadScene(3);
    }

    void TimerOnScreen(float time) 
    {
        float min = Mathf.FloorToInt(time / 60);
        float sec = Mathf.FloorToInt(time % 60);

        string currTime = string.Format("{00:00}{1:00}", min, sec);
        fstMinTxt.text = currTime[0].ToString();
        fstSecTxt.text = currTime[2].ToString();
        sndMinTxt.text = currTime[1].ToString();
        sndSecTxt.text = currTime[3].ToString();
        if (extraTime)
        {
            fstMinTxt.text = 0.ToString();
            fstSecTxt.text = 0.ToString();
            sndMinTxt.text = 0.ToString();
            sndSecTxt.text = 0.ToString();
        }
    }


    IEnumerator ExtraTimeToReset()
    {
        timer = 497;
        //todo flash the screen.
        for (int i =0; i< 4; i++)
        {
            globalLight.intensity = 1;
            yield return new WaitForSeconds(0.1f);
            globalLight.intensity = 0.2f;
            yield return new WaitForSeconds(0.1f);

        }
        globalLight.intensity = 1;

        yield return new WaitForSeconds(0.01f);
        globalLight.intensity = 0.07f;
        globalLight.intensity += 0.27f * Time.deltaTime;

        yield return new WaitForSeconds(0.4f);
        extraTime = false;
        ResetDay();
    }


    public void GameOver()
    {
        if (player.hasAllHosts)
        {
            //Game over screen.
            player.power = PowerUps.NONE;
            MenuUI();
            // time manipulation stops
            //todo play animation(text that says that the player won)
            Debug.Log("yay, you won!");
        }
        else
            ResetDay();
    }




    public void MenuUI()
    {
        SceneManager.LoadScene(4);
        Time.timeScale = 1;
    }



}
