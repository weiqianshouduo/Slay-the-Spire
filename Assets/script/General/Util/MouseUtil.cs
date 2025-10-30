using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseUtil 
{
    private static Camera camera = Camera.main;
    public static Vector3 GetMousePositionInWorldSpace(float zValue = 0f)
    {
        Plane dragPlane = new(Camera.main.transform.forward, new Vector3(0, 0, zValue));//创造一个平面 
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);//从摄像机发射一条射线到鼠标位置
        if (dragPlane.Raycast(ray, out float distance))//若相交：distance 会被赋值为射线原点到交点的距离，返回 true。b
        {
            return ray.GetPoint(distance);//返回他们相交的点
        }
        return Vector3.zero;

    }
}
