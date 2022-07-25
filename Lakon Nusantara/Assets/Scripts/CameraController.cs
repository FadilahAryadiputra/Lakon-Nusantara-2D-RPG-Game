using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float interpVelocity;
	public float minDistance;
	public float followDistance;
	public GameObject target;
	public Vector3 offset;
	Vector3 targetPos;

    public BoxCollider2D boundBox;
    public Vector3 minBounds;
    public Vector3 maxBounds;

    private Camera theCamera;
    private float halfHeight;
    private float halfWidth;

	public Transform camTransform;
	public float shakeDuration = 0.2f;
	public float shakeAmount = 5f;
    public float decreaseFactor = 1.0f;
	public bool shakeTrue = false;
	Vector3 originalPos;
    float originalShakeDuration; //<--add this

	// Use this for initialization
	void Start () {
		targetPos = transform.position;

        minBounds = boundBox.bounds.min;
        maxBounds = boundBox.bounds.max;

        theCamera = GetComponent<Camera>();
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
 	}

	void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

	void OnEnable()
    {
        originalPos = camTransform.localPosition;
        originalShakeDuration = shakeDuration; //<--add this
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if (target)
		{
			Vector3 posNoZ = transform.position;
			posNoZ.z = target.transform.position.z;

			Vector3 targetDirection = (target.transform.position - posNoZ);

			interpVelocity = targetDirection.magnitude * 5f;

			targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime); 

			transform.position = Vector3.Lerp( transform.position, targetPos + offset, 1f);

            float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
            float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
		}

		if (shakeTrue)
        {
            if (shakeDuration > 0)
            {
                // camTransform.localPosition = targetPos + Random.insideUnitSphere * shakeAmount;    //Rough shake
                camTransform.localPosition = Vector3.Lerp(camTransform.localPosition,targetPos + Random.insideUnitSphere * shakeAmount,Time.deltaTime * 3);   //Smooth shake

                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shakeDuration = originalShakeDuration; //<--add this
				transform.position = Vector3.Lerp( transform.position, targetPos + offset, 1f);
                shakeTrue = false;
            }
        }
	}

	public void ShakeCamera()
    {
        shakeTrue = true;
    }

    // public void SetBounds(BoxCollider2D newBounds)
    // {
    //     boundBox = newBounds;

    //     minBounds = boundBox.bounds.min;
    //     maxBounds = boundBox.bounds.max;
    // }
}
