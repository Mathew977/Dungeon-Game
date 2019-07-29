using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
        [SerializeField]
        private bool m_DefaultToRunning;
        [SerializeField]
        private float m_WalkSpeed = 2.0f;
        [SerializeField]
        private float m_RunSpeed = 5.0f;
        private float m_MoveSpeedIncrement = 0.15f;
        private float m_WalkSpeedMultiplier = 0.5f;
        private float m_CurrentSpeed;
        private bool m_attacking;
        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (!m_attacking)
            {
                m_attacking = CrossPlatformInputManager.GetButtonDown("attack");
            }
        }

        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);
            bool running = true;

            //TODO - Add running inputs
#if !MOBILE_INPUT
            //Running if either shift key is pressed
            running = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            //Check if default mode is running. If yes, swap the running input]
            if (m_DefaultToRunning)
            {
                running = !running;
            }
#endif
            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v * m_CamForward + h * m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v * Vector3.forward + h * Vector3.right;
            }
#if !MOBILE_INPUT
            // walk speed multiplier
            //if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;

            //walk speed multiplier
            //TODO Apply walk multiplier to reduce movement speed when not running (walking)
            if (!running) m_Move *= m_WalkSpeedMultiplier;
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, m_CurrentSpeed, crouch, m_Jump, m_attacking);
            m_Jump = false;
            m_attacking = false;

            //TODO - Update movement speed to pass to animator in ThirdPersonCharacter
            UpdateMoveSpeed(running);

            //pass all parameters to the character control script
            m_Character.Move(m_Move, m_CurrentSpeed, crouch, m_Jump, m_attacking);
            m_Jump = false;
            m_attacking = false;
        }

        private void UpdateMoveSpeed(bool running)
        {
            if (m_Move.Equals(Vector3.zero))
                m_CurrentSpeed = 0;
            else
            {
                if (running)
                {
                    //Running = limit current speed increment to a maximum i.e. the specified run speed
                    m_CurrentSpeed += m_MoveSpeedIncrement;
                    m_CurrentSpeed = Mathf.Min(m_CurrentSpeed, m_RunSpeed);
                }
                else
                {
                    //Walking - limit current speed decrease to a minimum i.e. the specified walk speed
                    m_CurrentSpeed -= m_MoveSpeedIncrement;
                    m_CurrentSpeed = Mathf.Max(m_WalkSpeed, m_CurrentSpeed);
                }
            }
        }
    }
}
