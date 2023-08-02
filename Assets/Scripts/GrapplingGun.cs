using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    public Animator anim;
    public GameObject player;
    [Header("Scripts Ref:")]
    public GrapplingRope grappleRope;

    [Header("Layers Settings:")]
    [SerializeField] private bool grappleToAll = false;
    [SerializeField] private int grappableLayerNumber = 6;

    [Header("Main Camera:")]
    public Camera m_camera;

    [Header("Transform Ref:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    [Header("Physics Ref:")]
    public SpringJoint2D m_springJoint2D;
    public Rigidbody2D m_rigidbody;

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistnace = 20;

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [Header("Launching:")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float launchSpeed = 1;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequncy = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;

    private void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        m_camera = Camera.main;
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (player.transform.localScale.x > 0 && player.transform.position.x > m_camera.ScreenToWorldPoint(Input.mousePosition).x)
            {
                //Debug.Log("Turn left if grappling behind. Player Scale: " + player.transform.localScale.x + ", Player Position: " + player.transform.position.x + "Mouse Pos:" + m_camera.ScreenToWorldPoint(Input.mousePosition).x);
                player.GetComponent<PlayerMovement>().Turn();
            }
            else if (player.transform.localScale.x < 0 && player.transform.position.x < m_camera.ScreenToWorldPoint(Input.mousePosition).x)
            {
                //Debug.Log("Turn right if grappling behind. Player Scale: " + player.transform.localScale.x + ", Player Position: " + player.transform.position.x + "Mouse Pos:" + m_camera.ScreenToWorldPoint(Input.mousePosition).x);
                player.GetComponent<PlayerMovement>().Turn();
            }
            //Debug.Log("Setting Grapple Point");
            SetGrapplePoint();
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            //Debug.Log("Debug Grappling behind. Player Scale: " + player.transform.localScale.x + ", Player Position: " + player.transform.position.x + "Mouse Pos:" + m_camera.ScreenToWorldPoint(Input.mousePosition).x);
            if (player.transform.localScale.x > 0 && player.transform.position.x > m_camera.ScreenToWorldPoint(Input.mousePosition).x)
            {
                //Debug.Log("Turn left if grappling behind. Player Scale: " + player.transform.localScale.x + ", Player Position: " + player.transform.position.x + "Mouse Pos:" + m_camera.ScreenToWorldPoint(Input.mousePosition).x);
                player.GetComponent<PlayerMovement>().Turn();
            }
            else if(player.transform.localScale.x < 0 && player.transform.position.x < m_camera.ScreenToWorldPoint(Input.mousePosition).x)
            {
                //Debug.Log("Turn right if grappling behind. Player Scale: " + player.transform.localScale.x + ", Player Position: " + player.transform.position.x + "Mouse Pos:" + m_camera.ScreenToWorldPoint(Input.mousePosition).x);
                player.GetComponent<PlayerMovement>().Turn();
            }
            if (grappleRope.enabled)
            {
                RotateGun(grapplePoint, false);
            }
            else
            {
                Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
                
                RotateGun(mousePos, true);
            }

            if (launchToPoint && grappleRope.isGrappling)
            {
                if (launchType == LaunchType.Transform_Launch)
                {
                    Vector2 firePointDistnace = firePoint.position - gunHolder.localPosition;
                    Vector2 targetPos = grapplePoint - firePointDistnace;
                    gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Time.deltaTime * launchSpeed);
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            if (anim.GetBool("isGrappling"))
            {
                anim.SetBool("isGrappling", false);
            }
            grappleRope.enabled = false;
            m_springJoint2D.enabled = false;
            m_rigidbody.gravityScale = 1;
        }
        else
        {
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            RotateGun(mousePos, true);
        }
    }

    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        if (rotateOverTime && allowRotationOverTime)
        {
            gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
        else
        {
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void SetGrapplePoint()
    {
        Vector2 distanceVector = m_camera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;
        if (Physics2D.Raycast(firePoint.position, distanceVector.normalized))
        {
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized);
            if (_hit.transform.gameObject.layer == grappableLayerNumber || grappleToAll)
            {
                if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistnace || !hasMaxDistance)
                {
                    //Debug.Log("Raycast Successful, layer is grappable");
                    grapplePoint = _hit.point;
                    grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                    grappleRope.enabled = true;
                    if (!anim.GetBool("isGrappling"))
                    {
                        anim.SetBool("isGrappling", true);
                    }
                }
                else { //Debug.Log("Hit Point distances and firepoint distance are not within max distance range."); 
                }
            }
            else { //Debug.Log("Layer of hit is " + _hit.transform.gameObject.layer);
                //Debug.DrawRay(firePoint.position, distanceVector.normalized,Color.red,30);
            }
        }
        else { //Debug.Log("Raycast did not collide with anything"); 
        }
    }

    public void Grapple()
    {
        m_springJoint2D.autoConfigureDistance = false;
        if (!launchToPoint && !autoConfigureDistance)
        {
            m_springJoint2D.distance = targetDistance;
            m_springJoint2D.frequency = targetFrequncy;
        }
        if (!launchToPoint)
        {
            if (autoConfigureDistance)
            {
                m_springJoint2D.autoConfigureDistance = true;
                m_springJoint2D.frequency = 0;
            }

            m_springJoint2D.connectedAnchor = grapplePoint;
            m_springJoint2D.enabled = true;
        }
        else
        {
            switch (launchType)
            {
                case LaunchType.Physics_Launch:
                    m_springJoint2D.connectedAnchor = grapplePoint;

                    Vector2 distanceVector = firePoint.position - gunHolder.position;

                    m_springJoint2D.distance = distanceVector.magnitude;
                    m_springJoint2D.frequency = launchSpeed;
                    //Debug.Log("SpringJoin Enabled Here");
                    m_springJoint2D.enabled = true;
                    break;
                case LaunchType.Transform_Launch:
                    m_rigidbody.gravityScale = 0;
                    m_rigidbody.velocity = Vector2.zero;
                    break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistnace);
        }
    }

}
