using UnityEngine;

namespace raCamera
{
	public class DoubleClickDetector
	{
		int numberOfClicks;
		float timer;

		public bool IsDoubleClick()
		{
			bool isDoubleClick = numberOfClicks == 2;
			if (isDoubleClick)
				numberOfClicks = 0;
			return isDoubleClick;
		}

		public void Update()
		{
			timer += Time.deltaTime;

			if (timer > 0.3f)
			{
				numberOfClicks = 0;
			}

			if (Input.GetMouseButtonDown(0))
			{
				numberOfClicks++;
				timer = 0.0f;
			}
		}
	}

	public class raCameraRTS : MonoBehaviour
	{

		public Transform terrain;
		public GameObject objectToFollow;

		[Header("Speed")]
		public float panSpeed = 15.0f;
		public float zoomSpeed = 100.0f;
		public float rotationSpeed = 50.0f;

		[Header("Mouse")]
		public float mousePanMultiplier = 0.1f;
		public float mouseRotationMultiplier = 0.2f;
		public float mouseZoomMultiplier = 5.0f;

		[Header("Zoom")]
		public float minZoomDistance = 20.0f;
		public float maxZoomDistance = 200.0f;
		public float smoothingFactor = 0.1f;
		public float goToSpeed = 0.1f;

		[Header("Settings")]
		public bool useKeyboardInput = true;
		public bool useMouseInput = true;
		public bool adaptToTerrainHeight = true;
		public bool increaseSpeedWhenZoomedOut = true;
		public bool correctZoomingOutRatio = true;
		public bool smoothing = true;
		public bool allowDoubleClickMovement = false;

		[Header("Edge move")]
		public bool allowScreenEdgeMovement = false;
		public int screenEdgeSize = 10;
		public float screenEdgeSpeed = 1.0f;

		public Vector3 cameraTarget;

		float currentCameraDistance;
		Vector3 lastMousePos;
		Vector3 lastPanSpeed;
		Vector3 goingToCameraTarget;
		bool doingAutoMovement;
		DoubleClickDetector doubleClickDetector;

		void Start()
		{
			currentCameraDistance = ((maxZoomDistance - minZoomDistance) / 2.0f);
			lastMousePos = Camera.main.WorldToScreenPoint(transform.position);// Vector3.zero;
			cameraTarget = transform.position;
			doubleClickDetector = new DoubleClickDetector();
		}

		void Update()
		{
			if (allowDoubleClickMovement)
			{
				doubleClickDetector.Update();
				UpdateDoubleClick();
			}
			UpdatePanning();
			UpdateRotation();
			UpdateZooming();
			UpdatePosition();
			UpdateAutoMovement();
			lastMousePos = Input.mousePosition;
		}

		public void GoTo(Vector3 position)
		{
			doingAutoMovement = true;
			goingToCameraTarget = position;
			objectToFollow = null;
		}

		public void Follow(GameObject gameObjectToFollow)
		{
			objectToFollow = gameObjectToFollow;
		}

		private void UpdateDoubleClick()
		{
			if (doubleClickDetector.IsDoubleClick() && terrain && terrain.GetComponent<Collider>())
			{
				float cameraTargetY = cameraTarget.y;

				Collider collider = terrain.GetComponent<Collider>();
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				Vector3 pos;

				if (collider.Raycast(ray, out hit, Mathf.Infinity))
				{
					pos = hit.point;
					pos.y = cameraTargetY;
					GoTo(pos);
				}
			}
		}

		private void UpdatePanning()
		{
			Vector3 moveVector = Vector3.zero;
			if (useKeyboardInput)
			{
				if (Input.GetKey(KeyCode.A))
				{
					moveVector += new Vector3(-1, 0, 0);
				}
				if (Input.GetKey(KeyCode.S))
				{
					moveVector += new Vector3(0, 0, -1);
				}
				if (Input.GetKey(KeyCode.D))
				{
					moveVector += new Vector3(1, 0, 0);
				}
				if (Input.GetKey(KeyCode.W))
				{
					moveVector += new Vector3(0, 0, 1);
				}
			}

			if (allowScreenEdgeMovement)
			{
				if (Input.mousePosition.x < screenEdgeSize)
				{
					moveVector.x -= screenEdgeSpeed;
				}
				else if (Input.mousePosition.x > Screen.width - screenEdgeSize)
				{
					moveVector.x += screenEdgeSpeed;
				}

				if (Input.mousePosition.y < screenEdgeSize)
				{
					moveVector.z -= screenEdgeSpeed;
				}
				else if (Input.mousePosition.y > Screen.height - screenEdgeSize)
				{
					moveVector.z += screenEdgeSpeed;
				}
			}

			if (useMouseInput)
			{
				if (Input.GetMouseButton(2) && !Input.GetKey(KeyCode.LeftShift))
				{
					Vector3 deltaMousePos = (Input.mousePosition - lastMousePos);
					moveVector += new Vector3(-deltaMousePos.x, 0, -deltaMousePos.y) * mousePanMultiplier;
				}
			}

			if (moveVector != Vector3.zero)
			{
				objectToFollow = null;
				doingAutoMovement = false;
			}

			var effectivePanSpeed = moveVector;
			if (smoothing)
			{
				effectivePanSpeed = Vector3.Lerp(lastPanSpeed, moveVector, smoothingFactor);
				lastPanSpeed = effectivePanSpeed;
			}

			Vector3 oldRotation = transform.localEulerAngles;
			transform.localEulerAngles = new Vector3(0,oldRotation.y, oldRotation.z);

			float panMultiplier = increaseSpeedWhenZoomedOut ? (Mathf.Sqrt(currentCameraDistance)) : 1.0f;
			cameraTarget = cameraTarget + transform.TransformDirection(effectivePanSpeed) * panSpeed * panMultiplier * Time.deltaTime;
			transform.localEulerAngles = oldRotation;
		}

		private void UpdateRotation()
		{
			float deltaAngleH = 0;
			float deltaAngleV = 0;

			if (useKeyboardInput)
			{
				if (Input.GetKey(KeyCode.Q))
				{
					deltaAngleH = 1.0f;
				}
				if (Input.GetKey(KeyCode.E))
				{
					deltaAngleH = -1.0f;
				}
			}

			if (useMouseInput)
			{
				if (Input.GetMouseButton(2) && Input.GetKey(KeyCode.LeftShift))
				{
					Vector3 deltaMousePos = (Input.mousePosition - lastMousePos);
					deltaAngleH += deltaMousePos.x * mouseRotationMultiplier;
					deltaAngleV -= deltaMousePos.y * mouseRotationMultiplier;
				}
			}
			Vector3 euler = transform.localEulerAngles;
			euler.y = transform.localEulerAngles.y + deltaAngleH * Time.deltaTime * rotationSpeed;
			euler.x = Mathf.Min(80.0f, Mathf.Max(5.0f, transform.localEulerAngles.x + deltaAngleV * Time.deltaTime * rotationSpeed));
			transform.localEulerAngles = euler;
		}

		private void UpdateZooming()
		{
			float deltaZoom= 0.0f;
			if (useKeyboardInput)
			{
				if (Input.GetKey(KeyCode.F))
				{
					deltaZoom = 1.0f;
				}
				if (Input.GetKey(KeyCode.R))
				{
					deltaZoom = -1.0f;
				}
			}
			if (useMouseInput)
			{
				float scroll = Input.GetAxis("Mouse ScrollWheel");
				deltaZoom -= scroll * mouseZoomMultiplier;
			}
			float zoomedOutRatio = correctZoomingOutRatio ? (currentCameraDistance - minZoomDistance) / (maxZoomDistance - minZoomDistance) : 0.0f;
			currentCameraDistance = Mathf.Max(minZoomDistance, Mathf.Min(maxZoomDistance, currentCameraDistance + deltaZoom * Time.deltaTime * zoomSpeed * (zoomedOutRatio * 2.0f + 1.0f)));
		}

		private void UpdatePosition()
		{
			if (objectToFollow != null)
			{
				cameraTarget = Vector3.Lerp(cameraTarget, objectToFollow.transform.position, goToSpeed);
			}

			transform.position = cameraTarget;
			transform.Translate(Vector3.back * currentCameraDistance);

			if (adaptToTerrainHeight && terrain != null)
			{
				//Vector3 pos = transform.position;
				//pos.y = Mathf.Max(terrain.SampleHeight(transform.position) + terrain.transform.position.y + 10.0f, transform.position.y);
				//transform.position = pos;
			}
		}

		private void UpdateAutoMovement()
		{
			if (doingAutoMovement)
			{
				cameraTarget = Vector3.Lerp(cameraTarget, goingToCameraTarget, goToSpeed);
				if (Vector3.Distance(goingToCameraTarget, cameraTarget) < 1.0f)
				{
					doingAutoMovement = false;
				}
			}
		}
	}
}