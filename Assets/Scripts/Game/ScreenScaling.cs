using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScaling : MonoBehaviour
{
    void Start()
    {
        
        // Ekranýn en/boy oranýný kontrol et
        float targetAspect = 16f / 9f; // 16:9 oraný
        float windowAspect = (float)Screen.width / (float)Screen.height;

        // Eðer ekran oraný 16:9'dan farklýysa siyah barlar ekleyin
        if (Mathf.Abs(targetAspect - windowAspect) > 0.01f)
        {
            // Siyah barlar ekleyin
            AddBlackBars(targetAspect, windowAspect);
        }
        
        
    }

    void AddBlackBars(float targetAspect, float windowAspect)
    {
        // Yükseklik ve geniþlik oranlarý
        float scaleHeight = windowAspect / targetAspect;
        Camera camera = Camera.main;

        // Siyah barlar ekleyin
        if (scaleHeight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            camera.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = camera.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
}
