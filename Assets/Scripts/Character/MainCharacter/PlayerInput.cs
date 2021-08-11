using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.NJUCS.PlayerInput;
//[RequireComponent(typeof(PlayerCharacter))]
namespace Unity.NJUCS.Character
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerCharacter character;
        //private CharacterMovement characterMovement;
        // Start is called before the first frame update
        void Start()
        {
            character = GetComponent<PlayerCharacter>();
            //characterMovement = GetComponent<CharacterMovement>();
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
            if (CrossPlatformInputManager.GetKeyUp(KeyCode.LeftShift))
            {
                character.ToggleSprint();
            }
            if (CrossPlatformInputManager.GetKeyDown(KeyCode.Space))
            {
                character.Jump();
            }
        }
    }
}
