using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;           
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Main : MonoBehaviour
{
    public static float Medicine;
    public static int Score;
    public TMP_Text Scoretext, pricetext, pricetext2;
    public ParticleSystem gecis;
    public int medicineprice = 10;
    public float time;
    public AudioSource poweronm, poweroffm;
    public static float Stageperform;
    public float hiz = 2;

    [Header("Efekt Ayarlarý")]
    public Volume globalVolume; // Inspector'dan Global Volume objesini sürüklemeyi unutma!
    private Vignette _vignette;

    public static bool Stage = false;

    [Header("Stage öncesi")]
    public GameObject buybutton, background, bloom1, stagefirst, sapwner1;

    [Header("Stage sonrasý")]
    public GameObject newbackground, animblackscene, PauseMenu, bloom2, stagesecond, sapwner2;

    void Start()
    {
        sapwner1.SetActive(true);
        sapwner2.SetActive(false);
        Stageperform = 70;
        poweronm.volume = 0;
        poweroffm.volume = 1f;
        pricetext.text = "";
        PauseMenu.SetActive(false);
        animblackscene.SetActive(false);
        buybutton.SetActive(false);
        Medicine = 100f;
        Score = 0;
        if (globalVolume != null)
        {
            globalVolume.profile.TryGet<Vignette>(out _vignette);
        }
        Stage = false;
        medicineprice = 10;
        StartCoroutine(Zamlar());
        bloom1.SetActive(true);
        bloom2.SetActive(false);
    }

    private void Update()
    {
        Scoretext.text = Score.ToString();
        if(!Stage) {
            SetVignette((100 - Medicine) / 130);
        }
        if (Medicine <= 0 && !Stage)
        {
            StartCoroutine(LastStage());
            StartCoroutine(PowerMusicOnEvent());
        }

        if (!Stage && Score >= medicineprice)
        {
            buybutton.SetActive(true);
        }
        else
        {
            buybutton.SetActive(false);
        }

        if (Medicine < 40 && !Stage)
        {
            // Önce scripti bul
            var shaker = Camera.main.GetComponent<CameraShake>();

            if (shaker != null)
            {
                // ÖRNEK 1: Canýn azaldýkça titremeyi artýr (Can 100 ise titreme 0, Can 20 ise titreme 0.8)
                float stres = 1f - (Medicine / 500);
                shaker.SetShake(stres);

                // Titremeyi durdur
            }
        }
        else if (Stage)
        {
            var shaker = Camera.main.GetComponent<CameraShake>();
            shaker.SetShake(1);
        }
        else
        {
            var shaker = Camera.main.GetComponent<CameraShake>();
            shaker.SetShake(0);
        }
        if (Medicine >= 100)
        {
            Medicine = 100;
        }
        if (Keyboard.current.escapeKey.wasPressedThisFrame && Time.timeScale == 1) 
        {
            Pausebutton();
        }
        else if (Keyboard.current.escapeKey.wasPressedThisFrame && Time.timeScale == 0)
        {
            Resumebutton();
        }
        pricetext2.text = medicineprice.ToString();
        time += Time.deltaTime;
        if(time > 60)
        {
            //2. böcek türü
        }
        if(Stage && Stageperform > 100)
        {
            StartCoroutine(Returns());
        }
        if (Stage)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Stageperform += 5;
            }
        }
        if(Stageperform <= 0)
        {
            StartCoroutine(Death());
        }
    }
    void FixedUpdate()
    {
        Medicine -= Time.deltaTime * hiz;
        
        if (Stage)
        {
            Stageperform -= Time.deltaTime * 3.5f;
        }
        if(hiz < 3.5f)
        {
            hiz += Time.deltaTime * 0.01f;
        }
    }
    public void Resumebutton()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Pausebutton()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ExitButton()
    {
        SceneManager.LoadScene(1);
    }
    public void BuyButton()
    {
        if(Score >= medicineprice)
        {
            Score -= medicineprice;
            Medicine += 40;
        }
    }
    public void SetVignette(float yogunluk)
    {
        if (_vignette != null)
        {
            // Deðeri 0 ile 1 arasýna sabitler ki hata olmasýn
            _vignette.intensity.value = Mathf.Clamp01(yogunluk);
        }
    }

    IEnumerator LastStage()
    {
        Stage = true;
        sapwner1.SetActive(false);
        stagefirst.SetActive(false);
        animblackscene.SetActive(true);
        yield return new WaitForSeconds(2);
        background.SetActive(false);
        animblackscene.SetActive(false);
        gecis.Play();
        PowerMusicOnEvent();
        SetVignette(0.1f);
        yield return new WaitForSeconds(5);
        BugSpawner1.Counter = 0;
        sapwner2.SetActive(true);
        bloom1.SetActive(false);
        bloom2.SetActive(true);
        newbackground.SetActive(true);
        yield return new WaitForSeconds(1);
        stagesecond.SetActive(true);
    }

    IEnumerator Zamlar()
    {
        if (!Stage)
        {
            yield return new WaitForSeconds(Random.Range(25, 35));
            medicineprice += 5;
            pricetext.text = "ELONMUSK TWÝT ATTI ILACA ZAM GELDI +5";
            yield return new WaitForSeconds(5);
            pricetext.text = "";
            yield return new WaitForSeconds(Random.Range(25, 35));
            medicineprice += 5;
            pricetext.text = "GEMINI 15.5 CIKTI ILACA ZAM GELDI +5";
            yield return new WaitForSeconds(5);
            pricetext.text = "";
            yield return new WaitForSeconds(Random.Range(25, 35));
            medicineprice += 10;
            pricetext.text = "UZAYLILAR DUNYAYA INDI ILACA ZAM GELDI +10";
            yield return new WaitForSeconds(5);
            pricetext.text = "";
            yield return new WaitForSeconds(Random.Range(20, 25));
            medicineprice += 20;
            pricetext.text = "4. DÜNYA SAVASI BASLADI ILACA ZAM GELDI +20";
            yield return new WaitForSeconds(5);
            pricetext.text = "";
            yield return new WaitForSeconds(Random.Range(15, 25));
            medicineprice += 20;
            pricetext.text = "AY HAVAYA UCTU ILACA ZAM GELDI +20";
            yield return new WaitForSeconds(5);
            pricetext.text = "";
            yield return new WaitForSeconds(Random.Range(13, 20));
            medicineprice += 30;
            pricetext.text = "KEDÝ AGACTAN DUSTU ILACA ZAM GELDI +30";
            yield return new WaitForSeconds(5);
            pricetext.text = "";
        }
        if (Stage)
        {
            pricetext.text = "";
        }
    }
    IEnumerator PowerMusicOnEvent()
    {
        poweronm.volume = 0;
        poweronm.Play();
        yield return new WaitForSeconds(0.3f);
        poweronm.volume = 0.04f;
        poweroffm.volume = 1f;
        yield return new WaitForSeconds(0.3f);
        poweronm.volume = 0.08f;
        poweroffm.volume = 0.9f;
        yield return new WaitForSeconds(0.3f);
        poweronm.volume = 0.12f;
        poweroffm.volume = 0.8f;
        yield return new WaitForSeconds(0.3f);
        poweronm.volume = 0.16f;
        poweroffm.volume = 0.7f;
        yield return new WaitForSeconds(0.3f);
        poweronm.volume = 0.20f;
        poweroffm.volume = 0.6f;
        yield return new WaitForSeconds(0.3f);
        poweronm.volume = 0.24f;
        poweroffm.volume = 0.5f;
        yield return new WaitForSeconds(0.3f);
        poweronm.volume = 0.28f;
        poweroffm.volume = 0.4f;
        yield return new WaitForSeconds(0.3f);
        poweronm.volume = 0.32f;
        poweroffm.volume = 0.3f;
        yield return new WaitForSeconds(0.3f);
        poweronm.volume = 0.36f;
        poweroffm.volume = 0.2f;
        yield return new WaitForSeconds(0.3f);
        poweronm.volume = 0.40f;
        poweroffm.volume = 0.1f;
        yield return new WaitForSeconds(0.3f);
        poweronm.volume = 0.4f;
        poweroffm.volume = 0f;
    }
    IEnumerator PowerMusicOffEvent()
    {
        poweroffm.volume = 0;
        poweroffm.Play();
        yield return new WaitForSeconds(0.3f);
        poweroffm.volume = 0.0f;
        poweronm.volume = 0.4f;
        yield return new WaitForSeconds(0.3f);
        poweroffm.volume = 0.1f;
        poweronm.volume = 0.36f;
        yield return new WaitForSeconds(0.3f);
        poweroffm.volume = 0.2f;
        poweronm.volume = 0.32f;
        yield return new WaitForSeconds(0.3f);
        poweroffm.volume = 0.3f;
        poweronm.volume = 0.28f;
        yield return new WaitForSeconds(0.3f);
        poweroffm.volume = 0.4f;
        poweronm.volume = 0.24f;
        yield return new WaitForSeconds(0.3f);
        poweroffm.volume = 0.5f;
        poweronm.volume = 0.20f;
        yield return new WaitForSeconds(0.3f);
        poweroffm.volume = 0.6f;
        poweronm.volume = 0.16f;
        yield return new WaitForSeconds(0.3f);
        poweroffm.volume = 0.7f;
        poweronm.volume = 0.12f;
        yield return new WaitForSeconds(0.3f);
        poweroffm.volume = 0.8f;
        poweronm.volume = 0.08f;
        yield return new WaitForSeconds(0.3f);
        poweroffm.volume = 0.9f;
        poweronm.volume = 0.04f;
        yield return new WaitForSeconds(0.3f);
        poweroffm.volume = 1f;
        poweronm.volume = 0f;
    }

    IEnumerator Returns()
    {
        Medicine = 70;
        Stage = false;
        Stageperform = 70;
        stagesecond.SetActive(false);
        animblackscene.SetActive(true);
        sapwner2.SetActive(false);
        StartCoroutine(PowerMusicOffEvent());
        yield return new WaitForSeconds(2);
        stagefirst.SetActive(true);
        BugSpawner.Counter = 0;
        newbackground.SetActive(false);
        SetVignette(0.1f);
        sapwner1.SetActive(true);
        background.SetActive(true);
        bloom1.SetActive(true);
        bloom2.SetActive(false);
        animblackscene.SetActive(false);        
    }

    IEnumerator Death()
    {
        animblackscene.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
        Application.OpenURL("https://www.kariyer.net/is-ilanlari/bilgisayar+muhendisi");
        animblackscene.SetActive(false);
    }
}
