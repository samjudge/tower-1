using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public Player Player;
    bool isOpended = false;

    void OnMouseDown()
    {
        if ((this.transform.position - Player.transform.position).sqrMagnitude <= 1)
        {
            if (!isOpended)
            {
                isOpended = !isOpended;
                StartCoroutine(SlideDown());
            }
        }
    }

    private IEnumerator SlideDown() {
        Player.ActionLog.WriteNewLine("the door creeks open...");
        Vector3 target = new Vector3(
            this.transform.position.x,
            -1,
            this.transform.position.z
        );
        float t = 0;
        Vector3 origin = this.transform.position;
        while ((this.transform.position - target).sqrMagnitude > Vector3.kEpsilon) {
            t += Time.deltaTime;
            this.transform.position = Vector3.Lerp(origin,target,t);
            yield return null;
        }
        yield return null;
    }

    void OnTriggerEnter(Collider other) {
        //check if player
        Player p = other.gameObject.GetComponent<Player>() as Player;
        if (p != null)
        {
            //set flag to cancel movement and return
            p.SetCancelMovementFlag(true);
        }
    }
}
