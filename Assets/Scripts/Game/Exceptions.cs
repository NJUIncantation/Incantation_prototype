using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.NJUCS.Game
{
    public class InputTypeNotFoundException : System.Exception
    {
        public InputTypeNotFoundException(string message) : base(message)
        {

        }
    }
}