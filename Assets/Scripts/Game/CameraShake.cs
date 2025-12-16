using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Ayarlar")]
    public float maxShakeAmount = 0.5f; // Deðiþken 1 olduðunda en fazla ne kadar sallansýn?

    // Bu deðiþken 0 ile 1 arasýnda olacak. 0 = Durur, 1 = Deprem
    [Range(0f, 1f)]
    public float currentShakeLevel = 0f;

    private Vector3 initialPos;

    void Start()
    {
        // Kameranýn oyun baþýndaki orijinal yerini kaydet
        initialPos = transform.localPosition;
    }

    void Update()
    {
        if (currentShakeLevel > 0)
        {
            // Orijinal pozisyonun üzerine rastgele sapma ekle
            // Random.insideUnitCircle: 1 birimlik daire içinde rastgele bir nokta verir (X ve Y için)
            Vector2 randomOffset = Random.insideUnitCircle * currentShakeLevel * maxShakeAmount;

            // Z eksenini (derinliði) bozmadan uygula
            transform.localPosition = new Vector3(initialPos.x + randomOffset.x, initialPos.y + randomOffset.y, initialPos.z);
        }
        else
        {
            // Shake seviyesi 0 ise kamerayý tam yerine oturt
            transform.localPosition = initialPos;
        }
    }

    // --- ÝÞTE ÝSTEDÝÐÝN FONKSÝYON ---
    // Baþka scriptten bunu çaðýracaksýn: SetShake(0.5f);
    public void SetShake(float level)
    {
        // Gelen deðeri 0 ile 1 arasýna sýkýþtýrýr (Negatif veya çok büyük sayý girmemen için)
        currentShakeLevel = Mathf.Clamp01(level);
    }
}