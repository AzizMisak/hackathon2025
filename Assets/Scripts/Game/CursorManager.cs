using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D crosshairTexture; // Inspector'dan resmini buraya sürükle

    void Start()
    {
        SetCustomCursor();
    }

    public void SetCustomCursor()
    {
        // Ýmlecin tam ortasý týklama noktasý olsun diye hesaplama yapýyoruz
        // Eðer bunu yapmazsan resmin sol üst köþesiyle týklarsýn!
        Vector2 hotspot = new Vector2(crosshairTexture.width / 2, crosshairTexture.height / 2);

        // Ýmleci deðiþtir
        Cursor.SetCursor(crosshairTexture, hotspot, CursorMode.Auto);
    }
}