using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private IInteractable m_CurrentInteractable;

    [SerializeField] private float m_ReachRange = 1.8f;

    private Camera m_FpsCam;

    private int m_RayLayerMask;

    protected GUIConfig m_GuiController;

    private static readonly Vector3 m_RayOriginPos = new(0.5f, 0.5f, 0f);

    private static readonly Collider[] m_HitColliders = new Collider[5];

    private void Start()
    {
        // A reference to Camera is required for rayasts
        m_FpsCam = Camera.main;
        if (m_FpsCam == null)   
        {
            throw new UnityException("A Camera tagged 'MainCamera' is missing.");
        }

        // The layer used to mask raycast for interactable objects only
        LayerMask iRayLM = LayerMask.NameToLayer("InteractRaycast");
        m_RayLayerMask = 1 << iRayLM.value;

        // Setup GUI style settings for user prompts
        m_GuiController = new GUIConfig();
    }

    private void Update()
    {
        const float radius = 0.1f;

        m_GuiController.ShouldShowMsg = false;
        Vector3 rayOrigin = m_FpsCam.ViewportToWorldPoint(m_RayOriginPos);

        // If raycast hits a collider on the rayLayerMask
        if (Physics.Raycast(rayOrigin, m_FpsCam.transform.forward, out RaycastHit hit, m_ReachRange, m_RayLayerMask))
        {
            m_CurrentInteractable = hit.collider.GetComponent<IInteractable>();
        }
        else
        {
            int numCollisions = Physics.OverlapSphereNonAlloc(m_FpsCam.transform.position, radius, m_HitColliders, m_RayLayerMask);

            if (numCollisions == 0)
            {
                m_CurrentInteractable = null;
                return;
            }

            for (int i = 0; i < numCollisions; i++)
            {
                Collider currentColider = m_HitColliders[i];
                m_CurrentInteractable = currentColider.GetComponent<IInteractable>();

                if (m_CurrentInteractable != null)
                {
                    break;
                }   
            }
        }

        if (m_CurrentInteractable != null)
        {
            m_CurrentInteractable.LookAt(m_GuiController);

            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1"))
            {
                m_CurrentInteractable.Interact();
            }
        }
    }

    private void OnGUI()
    {
        m_GuiController.OnGUI();
    }
}
