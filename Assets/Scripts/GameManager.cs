using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Animator animator;

    public Animator GetAnimator
    {

        get { return animator; }

    }

}
