using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypewriterBarCollider : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name=="Bar-Right Target")
        {
            GameObject.Find("Typewriter-Bar").GetComponent<Bar>().CarriageReturn();
        }
    }
}
