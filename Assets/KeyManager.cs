using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour {

    public Output target;

	public void SetTarget(GameObject target)
    {
        if (target != null)
        {
            this.target = target.GetComponentInChildren<Output>();
        }   
    }

    public Output GetTarget()
    {
        return target;
    }
}
