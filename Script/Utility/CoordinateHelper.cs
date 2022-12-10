using UnityEngine;

public class CoordinateHelper : MonoBehaviour
{
    /*
     * flat top 그리드에서 
     * 인접한 육각형 중심까지의 수평거리 = 3/4 * width = 3/2 * size(외접원의 반지름)
     * 수직거리 = hieght = sqrt(3) * size 
	*/

    public static float rootThree = Mathf.Sqrt(3); 

    //스크린 좌표 -> axial coordinate
    public static Vector2 screenToAxial(Vector2 screenPoint, float sideLength)
    {
        var axialPoint = new Vector2();
        axialPoint.y = screenPoint.x / (1.5f * sideLength);
        axialPoint.x = (screenPoint.y - (screenPoint.x / rootThree)) / (rootThree * sideLength);
        var cubicZ = calculateCubicZ(axialPoint);
        var round_x = Mathf.Round(axialPoint.x);
        var round_y = Mathf.Round(axialPoint.y);
        var round_z = Mathf.Round(cubicZ);
        if (round_x + round_y + round_z == 0)
        {
            screenPoint.x = round_x;
            screenPoint.y = round_y;
        }
        else
        {
            var delta_x = Mathf.Abs(axialPoint.x - round_x);
            var delta_y = Mathf.Abs(axialPoint.y - round_y);
            var delta_z = Mathf.Abs(cubicZ - round_z);
            if (delta_x > delta_y && delta_x > delta_z)
            {
                screenPoint.x = -round_y - round_z;
                screenPoint.y = round_y;
            }
            else if (delta_y > delta_x && delta_y > delta_z)
            {
                screenPoint.x = round_x;
                screenPoint.y = -round_x - round_z;
            }
            else if (delta_z > delta_x && delta_z > delta_y)
            {
                screenPoint.x = round_x;
                screenPoint.y = round_y;
            }
        }
        return screenPoint;
    }

    //axial coordinate -> 스크린 좌표 
    public static Vector2 AxialToScreen(Vector2 axialPoint, float sideLength)
    {
        var tileY = rootThree * sideLength * (axialPoint.x + (axialPoint.y / 2));
        var tileX = 3 * sideLength / 2 * axialPoint.y;
        axialPoint.x = tileX;
        axialPoint.y = tileY;
        return axialPoint;
    }

    // Cube coordinates에서 x+y+z=0임을 이용
    public static float calculateCubicZ(Vector2 newAxialPoint)
    {
        return -newAxialPoint.x - newAxialPoint.y;
    }
}
