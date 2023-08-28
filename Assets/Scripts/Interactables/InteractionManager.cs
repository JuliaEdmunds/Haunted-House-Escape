using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private IInteractable m_CurrentInteractable;

    [SerializeField] private float m_ReachRange = 1.8f;

    private Camera m_FpsCam;
    private GameObject m_Player;

    private int m_RayLayerMask;

    protected GUIConfig m_GuiController;

    private void Start()
    {
        //Initialize moveDrawController if script is enabled.
        m_Player = GameObject.FindGameObjectWithTag("Player");

        m_FpsCam = Camera.main;
        if (m_FpsCam == null)   //a reference to Camera is required for rayasts
        {
            throw new UnityException("A Camera tagged 'MainCamera' is missing.");
        }

        //the layer used to mask raycast for interactable objects only
        LayerMask iRayLM = LayerMask.NameToLayer("InteractRaycast");
        m_RayLayerMask = 1 << iRayLM.value;

        //setup GUI style settings for user prompts
        m_GuiController = new GUIConfig();
    }

    private void Update()
    {
        m_GuiController.ShouldShowMsg = false;
        Vector3 rayOrigin = m_FpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));

        //if raycast hits a collider on the rayLayerMask
        if (Physics.Raycast(rayOrigin, m_FpsCam.transform.forward, out RaycastHit hit, m_ReachRange, m_RayLayerMask))
        {
            m_CurrentInteractable = hit.collider.GetComponent<IInteractable>();
        }
        else
        {
            Collider[] hitColliders = Physics.OverlapSphere(m_FpsCam.transform.position, 0.1f, m_RayLayerMask);

            if (hitColliders.Length == 0)
            {
                return;
            }

            for (int i = 0; i < hitColliders.Length; i++)
            {
                Collider currentColider = hitColliders[i];

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

