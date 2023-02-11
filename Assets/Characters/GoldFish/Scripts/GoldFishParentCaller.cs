using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFishParentCaller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TongueCol")
        {
            transform.parent.SendMessage("callDeathAnim");
        }
    }
}
