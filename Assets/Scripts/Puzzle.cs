using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    private void Start(){
        StartCoroutine(JunkCode());
    }

    private IEnumerator JunkCode(){
        yield return new WaitForSeconds(1f);

        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().isTrigger = true;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
    }
}
