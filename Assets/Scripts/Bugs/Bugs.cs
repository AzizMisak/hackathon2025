using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Bugs : MonoBehaviour
{
    public Animator anim;
    [Header("Hareket Ayarlarý")]
    public float moveSpeed = 3f;      // Ýlerleme hýzý
    public float turnSpeed = 200f;    // Dönüþ yumuþaklýðý
    public float changeDirectionTime = 1f; // Ne kadar sürede bir yön deðiþtirsin?

    [Header("Sýnýrlar (Ekranýn dýþýna çýkmamasý için)")]
    public float xBound = 9f;  // Ekranýn sað/sol sýnýrý
    public float yBound = 5f;  // Ekranýn üst/alt sýnýrý

    private float timer;
    private Quaternion targetRotation; // Hedeflediðimiz açý

    public AudioClip deathSound;
    void Start()
    {
        anim = GetComponent<Animator>();    
        // Baþlangýçta Spawner'dan gelen açýyý koru, hedef olarak onu belirle
        targetRotation = transform.rotation;
        timer = changeDirectionTime;
        transform.localScale = Vector3.one * Random.Range(1f, 2.5f);
        moveSpeed = Random.Range(1.2f, 3);
    }

    void Update()
    {
        // 1. Sürekli olarak "Kendi Yukarýsýna" (Kafasýnýn olduðu yöne) git
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);

        // 2. Zamanlayýcýyý çalýþtýr
        timer -= Time.deltaTime;

        // 3. Sýnýr Kontrolü (En Önemli Kýsým)
        // Eðer karýnca sýnýrlarýn dýþýna çýktýysa, rastgelelik yapma, hemen merkeze dön!
        bool outOfBounds = Mathf.Abs(transform.position.x) > xBound || Mathf.Abs(transform.position.y) > yBound;

        if (outOfBounds)
        {
            // Merkeze (0,0) doðru olan açýyý bul
            Vector2 directionToCenter = (Vector2.zero - (Vector2)transform.position).normalized;
            float angleToCenter = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg + 90f; // -90 sprite'ýn yönüne göre deðiþebilir
            targetRotation = Quaternion.Euler(0, 0, angleToCenter);
        }
        else if (timer <= 0)
        {
            // Sýnýrlarýn içindeyse rastgele bir yöne dön
            PickNewRandomDirection();
            timer = Random.Range(0.5f, 1.5f); // Her seferinde farklý sürede karar versin (doðallýk için)
        }

        // 4. Karýncayý yumuþakça hedef açýya döndür
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        if (moveSpeed != 0)
        {
            CheckForClick();
        }


        if(Main.Stage == true)
        {
            Destroy(gameObject);
        }
    }

    void PickNewRandomDirection()
    {
        // Þu anki açýsýnýn üzerine rastgele -90 ile +90 derece ekle (keskin dönüþ yapmasýn)
        float currentZ = transform.rotation.eulerAngles.z;
        float randomTurn = Random.Range(-90f, 90f);
        targetRotation = Quaternion.Euler(0, 0, currentZ + randomTurn);
    }
    void CheckForClick()
    {
        // 1. Mouse var mý ve Sol Týk basýldý mý?
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            // 2. Mouse'un ekran konumunu al
            Vector2 mousePos = Mouse.current.position.ReadValue();

            // 3. Dünyadaki karþýlýðýný bul
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            // 4. O noktaya lazer tut (Raycast)
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

            // 5. Eðer bir þeye çarptýysa VE çarptýðý þey BU OBJE ise
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                StartCoroutine(Death());
            }
        }
    }
    IEnumerator Death()
    {
        moveSpeed = 0;
        turnSpeed = 0;
        anim.SetTrigger("death");
        Main.Score++;
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }
}
