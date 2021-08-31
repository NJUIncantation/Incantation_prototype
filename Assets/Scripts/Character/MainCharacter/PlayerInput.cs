using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.NJUCS.PlayerInput;
using Unity.NJUCS.Game;
//[RequireComponent(typeof(PlayerCharacter))]
namespace Unity.NJUCS.Character
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerCharacter character;
        //private CharacterMovement characterMovement;
        // Start is called before the first frame update

        //zjq modified
        private PlayerWeaponManager m_playerWeaponManager;
        private bool m_QInputWasHeld;
        private bool m_EInputWasHeld;
        //modified
        private bool m_FireInputWasHeld;

        void Start()
        {
            character = GetComponent<PlayerCharacter>();
            //characterMovement = GetComponent<CharacterMovement>();
            m_playerWeaponManager = GetComponent<PlayerWeaponManager>();
        }

        // Update is called once per frame
        void Update()
        {

            character.UpdatePosition(CrossPlatformInputManager.GetAxis("Vertical"), CrossPlatformInputManager.GetAxis("Horizontal"));
            if (CrossPlatformInputManager.GetKeyDown(KeyCode.CapsLock))
            {
                character.ToggleRun();
            }
            if (CrossPlatformInputManager.GetKeyDown(KeyCode.LeftControl))
            {
                character.ToggleCrouch();
            }
            if (CrossPlatformInputManager.GetKeyDown(KeyCode.LeftShift))
            {
                character.ToggleSprint();
            }
            if (CrossPlatformInputManager.GetKeyDown(KeyCode.Space))
            {
                character.Jump();
            }
            //ÊÍ·Å¼¼ÄÜ
            if (CrossPlatformInputManager.GetKeyDown(KeyCode.J))
            {
                character.Cast(CharacterCasting.CharacterSpells.Spell_J);
            }
            if (CrossPlatformInputManager.GetKeyDown(KeyCode.K))
            {
                character.Cast(CharacterCasting.CharacterSpells.Spell_K);
            }
            //fire input handle
            //modified
            if (m_playerWeaponManager != null)
            {
                /*bool Q_inputDown = GetInputHeld(KeyCode.Q) && !m_QInputWasHeld;
                bool Q_inputHeld = GetInputHeld(KeyCode.Q);
                bool Q_inputUp = !GetInputHeld(KeyCode.Q) && m_QInputWasHeld;
                bool E_inputDown = GetInputHeld(KeyCode.E) && !m_EInputWasHeld;
                bool E_inputHeld = GetInputHeld(KeyCode.E);
                bool E_inputUp = !GetInputHeld(KeyCode.E) && m_EInputWasHeld;*/

                bool Fire_inputDown = GetFireInputHeld() && !m_FireInputWasHeld;
                bool Fire_inputHeld = GetFireInputHeld();
                bool Fire_inputUp = !GetFireInputHeld() && m_FireInputWasHeld;
                
                //m_playerWeaponManager.Control(Fire_inputDown, Fire_inputHeld, Fire_inputUp);

                //m_playerWeaponManager.Control(Q_inputDown, Q_inputHeld, Q_inputUp, E_inputDown, E_inputHeld, E_inputUp);
            }
        }

        void LateUpdate()
        {
            m_QInputWasHeld = GetInputHeld(KeyCode.Q);
            m_EInputWasHeld = GetInputHeld(KeyCode.E);
            //modified
            m_FireInputWasHeld = GetFireInputHeld();
        }

        //modified
        public bool GetFireInputHeld()
        {
            return CrossPlatformInputManager.GetButtonDown("Fire1");
        }

        bool GetInputHeld(UnityEngine.KeyCode keyCode)
        {
            return (CrossPlatformInputManager.GetKeyDown(keyCode));
        }
}
}
