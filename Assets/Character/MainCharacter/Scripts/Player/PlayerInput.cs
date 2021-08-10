using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent(typeof(PlayerCharacter))]
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
        character.UpdatePosition(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            character.ToggleRun();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            character.ToggleCrouch();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            character.ToggleSprint();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            character.ToggleSprint();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            character.Jump();
        }
    }
}
