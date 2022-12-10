using UnityEngine;

public class CameraResoultion : MonoBehaviour
{
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;

        float scaleHeight = ((float)Screen.width / Screen.height) / ((float)9 / 16); // 1080*1920(16:9)해상도 대비 플레이어 화면 비율
        float scaleWidth = 1f / scaleHeight;

        if(scaleHeight<1) // 플레이어 화면 세로비가 더 큰 경우
        {
            rect.height = scaleHeight;
            rect.y = (1f - scaleHeight) / 2f;
        }
        else // 플레이어 화면 가로비가 더 큰 경우 
        {
            rect.width = scaleWidth;
            rect.x = (1f - scaleWidth) / 2f;
        }
        camera.rect = rect;
    }
}
