using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SimpleEnemy))]
public class SimpleEnemyEditor : Editor
{
    SimpleEnemy enemy;

    void OnSceneGUI()
    {

        Vector3 leftPos = enemy.transform.position + enemy.moveLeftPoint;
        Vector3 newPos = Handles.DoPositionHandle(leftPos, Quaternion.identity);
        Handles.DrawWireCube(newPos, new Vector3(.5f, .3f, .1f));
        Handles.Label(new Vector3(newPos.x - .25f, newPos.y, newPos.z), "LEFT");

        if (newPos != leftPos)
        {
            enemy.moveLeftPoint = newPos - enemy.transform.position;
        }

        Vector3 rightPos = enemy.transform.position + enemy.moveRightPoint;
        newPos = Handles.DoPositionHandle(rightPos, Quaternion.identity);
        Handles.Label(new Vector3(newPos.x - .25f, newPos.y, newPos.z), "RIGHT");
        Handles.DrawWireCube(newPos, new Vector3(.5f, .3f, .1f));
        if (newPos != rightPos)
        {
            enemy.moveRightPoint = newPos - enemy.transform.position;
        }
    }

    private void OnEnable()
    {
        enemy = (SimpleEnemy)target;
    }

}
