
using UnityEngine;

public class MetaMovementAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform trackingSpace;
    [SerializeField] private Transform head;
    [SerializeField] private float smoothing = 5f;
    [SerializeField] private float maxSpeed = 1.5f;


    private Vector3 lastHeadPos;
    private Vector3 smoothedVelocity;

    // --- Networked animation parameters ---
    public float MoveX { get; set; }
    public float MoveY { get; set; }
    public float LeftGrab { get; set; }
    public float RightGrab { get; set; }
    public float LeftGrabState { get; set; }
    public float RightGrabState { get; set; }

    // Local interpolation buffer
    private float currentMoveX, currentMoveY, currentLeftGrab, currentRightGrab, currentLeftGrabState, currentRightGrabState;

    private void Start()
    {
        if (!animator)
            animator = GetComponent<Animator>();

        if (head)
            lastHeadPos = head.position;
    }

    //public void Init(Transform trackingSpace, Transform head,
    //    HandGrabInteractor leftHandInteractor, HandGrabInteractor rightHandInteractor,
    //    GrabInteractor leftControllerInteractor, GrabInteractor rightControllerInteractor)
    //{
    //    this.trackingSpace = trackingSpace;
    //    this.head = head;
    //    this.leftHandInteractor = leftHandInteractor;
    //    this.rightHandInteractor = rightHandInteractor;
    //    this.leftControllerInteractor = leftControllerInteractor;
    //    this.rightControllerInteractor = rightControllerInteractor;
    //}

    public void OnLeftGrab(float grabbingState, float leftGrab)
    {
        LeftGrabState = grabbingState;
        LeftGrab = leftGrab;
    }

    public void OnRightGrab(float grabbingState, float rightGrab)
    {
        RightGrabState = grabbingState;
        RightGrab = rightGrab;

        Debug.LogWarning("OnRightGrab : " + grabbingState + " __ " + rightGrab);
    }

    private void LateUpdate()
    {
        // --- Only input authority updates parameters ---
        if (head != null)
        {
            Vector3 rawVelocity = (head.position - lastHeadPos) / Time.deltaTime;
            lastHeadPos = head.position;

            smoothedVelocity = Vector3.Lerp(smoothedVelocity, rawVelocity, Time.deltaTime * smoothing);

            Vector3 flatVel = new Vector3(smoothedVelocity.x, 0, smoothedVelocity.z);
            Vector3 headForward = new Vector3(head.forward.x, 0, head.forward.z).normalized;
            Vector3 headRight = new Vector3(head.right.x, 0, head.right.z).normalized;

            float forward = Vector3.Dot(flatVel, headForward) * 2;
            float right = Vector3.Dot(flatVel, headRight) * 2;

            float normalizedForward = Mathf.Clamp(forward / maxSpeed, -1f, 1f);
            float normalizedRight = Mathf.Clamp(right / maxSpeed, -1f, 1f);

            MoveY = normalizedForward;
            MoveX = normalizedRight;

            //if (leftControllerInteractor)
            //    LeftGrabState = leftControllerInteractor.IsGrabbing ? 1f : 0f;
            //else if (leftHandInteractor)
            //    LeftGrabState = leftHandInteractor.IsGrabbing ? 1f : 0f;

            //if (rightControllerInteractor)
            //    RightGrabState = rightControllerInteractor.IsGrabbing ? 1f : 0f;
            //else if (rightHandInteractor)
            //    RightGrabState = rightHandInteractor.IsGrabbing ? 1f : 0f;
        }

        // --- Interpolate animator parameters on all clients ---
        currentMoveX = Mathf.Lerp(currentMoveX, MoveX, Time.deltaTime * smoothing);
        currentMoveY = Mathf.Lerp(currentMoveY, MoveY, Time.deltaTime * smoothing);
        //currentLeftGrab = Mathf.Lerp(currentLeftGrab, LeftGrab, Time.deltaTime * smoothing);
        //currentRightGrab = Mathf.Lerp(currentRightGrab, RightGrab, Time.deltaTime * smoothing);
        //currentLeftGrabState = Mathf.Lerp(currentLeftGrabState, LeftGrabState, Time.deltaTime * smoothing);
        //currentRightGrabState = Mathf.Lerp(currentRightGrabState, RightGrabState, Time.deltaTime * smoothing);

        if (animator)
        {
            //animator.SetFloat("moveX", currentMoveX);
            //animator.SetFloat("moveY", currentMoveY);
            animator.SetFloat("leftGrab", LeftGrab);
            animator.SetFloat("rightGrab", RightGrab);
            animator.SetFloat("leftGrabState", LeftGrabState);
            animator.SetFloat("rightGrabState", RightGrabState);
        }
    }
}
