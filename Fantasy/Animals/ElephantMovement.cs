using UnityEngine;

namespace Assets.Scripts.Fantasy.Animals
{
    [RequireComponent(typeof(Animator))]
    public class ElephantMovement : MonoBehaviour
    {
        private Animator anim;

        private void Awake()
        {
            if (anim == null) anim = GetComponent<Animator>();
        }

        private void Update()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (h != 0 || v != 0) anim.SetBool("Walk", true);
            else anim.SetBool("Walk", false);
        }
    }
}
