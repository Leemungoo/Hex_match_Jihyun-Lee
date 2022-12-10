using UnityEngine;

public class CameraResoultion : MonoBehaviour
{
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;

        float scaleHeight = ((float)Screen.width / Screen.height) / ((float)9 / 16); // 1080*1920(16:9)�ػ� ��� �÷��̾� ȭ�� ����
        float scaleWidth = 1f / scaleHeight;

        if(scaleHeight<1) // �÷��̾� ȭ�� ���κ� �� ū ���
        {
            rect.height = scaleHeight;
            rect.y = (1f - scaleHeight) / 2f;
        }
        else // �÷��̾� ȭ�� ���κ� �� ū ��� 
        {
            rect.width = scaleWidth;
            rect.x = (1f - scaleWidth) / 2f;
        }
        camera.rect = rect;
    }
}
