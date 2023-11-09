using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField] GameObject bladeTrailPrefab;
    GameObject bladeTrailInstantion;

    [SerializeField] LayerMask targetLayer;

    [SerializeField] Vector3 raycastOffset = new Vector3(0f, 0f, -5f); // to avoid casting rays from inside of targets

    bool isCutting;

	void Update()
    {
        if (Input.GetMouseButtonDown(0))
		{
            isCutting = true;
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) * Vector2.one;
            bladeTrailInstantion = Instantiate(bladeTrailPrefab, transform);
		}
		else if (Input.GetMouseButtonUp(0))
		{
            isCutting = false;
            Destroy(bladeTrailInstantion);
        }

        if (isCutting)
		{
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) * Vector2.one;

            bool bladeMoved = Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved;

            if (Application.platform == RuntimePlatform.WindowsEditor && Input.touchCount == 0)
                bladeMoved = Input.GetAxisRaw("Mouse X") != 0f || Input.GetAxisRaw("Mouse Y") != 0f;

            if (bladeMoved)
            {
                RaycastHit rayHit;
                if (Physics.Raycast(transform.position + raycastOffset, Vector3.forward, out rayHit, 100f, targetLayer))
                {
                    Target slicedTarget = rayHit.transform.GetComponent<Target>();
                    if (slicedTarget != null)
                    {
                        Events.OnTargetHit?.Invoke(slicedTarget, rayHit.point);
                        slicedTarget.OnTargetHit(transform.position);
                    }
                }
            }
        }
    }
}
