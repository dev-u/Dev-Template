using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class InteractableObject : MonoBehaviour
{
    #region Fields and Properties
    // Private Fields
    private Collider2D _col2D;

    // Serialized Fields
    [Tooltip("Obejto pode ser interagido")]
    [SerializeField]private bool isInteractable = true;
    [Tooltip("Uma tecla � necess�ria para interagir com o objeto")]
    [SerializeField] private bool isKeyInteractable = true;
    [Tooltip("Tecla para interagir com o objeto")]
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [Tooltip("Filtro de colis�o para intera��o")]
    [SerializeField] private ContactFilter2D contactFilter;
    [Tooltip("Evento de intera��o com o objeto")]
    [SerializeField] private UnityEvent<GameObject> onInteract;


    // Public Properties
    public bool IsInteractable => isInteractable;
    public bool IsKeyInteractable => isKeyInteractable;
    public ContactFilter2D ContactFilter => contactFilter;
    public KeyCode InteractKey => interactKey;
    public UnityEvent<GameObject> OnInteract => onInteract;

    #endregion

    private void Awake()
    {
        _col2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        // Clausa de guarda
        if (!isInteractable) return;

        Collider2D[] colliders = new Collider2D[10];

        // Checar se o collider do objeto est� colidindo com algum outro objeto
        if (_col2D.OverlapCollider(contactFilter, colliders) > 0)
        {
            // Checar se o objeto com o qual est� colidindo est� na layer mask
            if (colliders.Any(c => contactFilter.layerMask == (contactFilter.layerMask | (1 << c.gameObject.layer))))
            {
                if (!isKeyInteractable)
                {
                    Interact();
                }
                else if (Input.GetKeyDown(interactKey))
                {
                    Interact();
                }
            }
        }
    }
    

    public void Interact()
    {
        onInteract.Invoke(gameObject);
    }

}
