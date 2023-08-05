using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGrappling : MonoBehaviour
{
    [SerializeField] public bool isLerping;
    [SerializeField]
    private Vector2 startPosition;
    private Vector2 endPosition;
    private Vector2 launchPower = new Vector2(0,10);
    //private float desiredDuration = .7f;
    private float grappleReleaseDistance = 6;
    private float elapsedTime;
    //private float elapsedTime2;
    private float launchSpeed;
    private bool hasLaunched;
    public GrapplingRope grapplePointer;

    private void Update()
    {
        if (isLerping)
        {
            //grapplePointer = transform.position;
            //Debug.Log(elapsedTime);
            grapplePointer.reverseMoveTime = elapsedTime;
            if (this.GetComponent<ItemInteraction>().distanceFromPlayer < grappleReleaseDistance) {
                //this.GetComponent<ItemInteraction>().ActivateItem();
                this.GetComponent<ItemInteraction>().pm.OnItemPickup();
                //isLerping = false; 
            }
            else if(hasLaunched && isLerping && (this.GetComponent<Rigidbody2D>().velocity.y < 0))
            {
                elapsedTime += Time.deltaTime;
                
                //float percentageComplete = elapsedTime / desiredDuration;
                //transform.position = Vector2.Lerp(startPosition, endPosition, Mathf.SmoothStep(0, 1, percentageComplete));
                //this.GetComponent<Rigidbody2D>().AddForce((endPosition - startPosition)+Vector2.up);
            }
            if (!hasLaunched)
            {
                //this.GetComponent<Rigidbody2D>().AddForce((endPosition - startPosition) + launchPower, ForceMode2D.Impulse);
                this.GetComponent<Rigidbody2D>().AddForce((endPosition - startPosition)+launchPower, ForceMode2D.Impulse);

                hasLaunched = !hasLaunched;
            }
            //transform.position = Vector2.Lerp(startPosition, endPosition, Mathf.SmoothStep(0, 1, percentageComplete));
        }
        else
        {
            //hasLaunched = false;
            //endPosition = Vector2.zero;
            //startPosition = Vector2.zero;
            //elapsedTime = 0;
        }
        //this.GetComponent<Rigidbody2D>().AddForce(endPosition - startPosition);

        //gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Mathf.SmoothStep(0, 1, percentageComplete));
    }
    public void FlipTheLerp(Vector2 endPos, float ls, GrapplingRope _grapplePoint)
    {
        isLerping = !isLerping;
        startPosition = this.transform.position;
        endPosition = endPos;
        launchSpeed = ls;
        grapplePointer = _grapplePoint;
        //return "Is Lerping: " + isLerping+"From: "+startPosition+", To: "+endPosition;
    }

}
