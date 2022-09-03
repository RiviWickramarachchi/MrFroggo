using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentCaller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TongueCol")
        {
            print("Hit = TOngue collision_Fairy");
            transform.parent.SendMessage("callFairyDeathAnim");

        }
    }

    private void destroyFairyParentObj()
    {
        Destroy(transform.parent.gameObject);
        Destroy(this.gameObject);
    }
}
