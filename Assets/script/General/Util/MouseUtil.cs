using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseUtil 
{
    private static Camera camera = Camera.main;
    public static Vector3 GetMousePositionInWorldSpace(float zValue = 0f)
    {
        Plane dragPlane = new(Camera.main.transform.forward, new Vector3(0, 0, zValue));//����һ��ƽ�� 
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);//�����������һ�����ߵ����λ��
        if (dragPlane.Raycast(ray, out float distance))//���ཻ��distance �ᱻ��ֵΪ����ԭ�㵽����ľ��룬���� true��b
        {
            return ray.GetPoint(distance);//���������ཻ�ĵ�
        }
        return Vector3.zero;

    }
}
