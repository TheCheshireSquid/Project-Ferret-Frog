using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{

    [SerializeField] PlayerScript player;




    [SerializeField] float speed;


    [SerializeField] float heightAllowance;


    float groundLevel;


    private void LateUpdate()
    {
        var pPos = player.gameObject.transform.position.y;
        var gPos = gameObject.transform.position.y;

        Vector3 newPos = player.transform.position;

        if (player.Grounded || pPos >= gPos + heightAllowance || pPos < gPos)
        {
            groundLevel = pPos;

        }
        else
        {
            newPos.y = groundLevel;



        }






        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, newPos, speed * Time.deltaTime);
        
    }



}
