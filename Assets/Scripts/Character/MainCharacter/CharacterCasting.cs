using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.NJUCS.Spell;

namespace Unity.NJUCS.Character
{
    public class CharacterCasting : MonoBehaviour
    {
        public enum CharacterSpells
        {
            Spell_Q,
            Spell_E
        }

        public Dictionary<CharacterSpells, VirtualSpell> MySpells = new Dictionary<CharacterSpells, VirtualSpell>();

        //TODO: 技能的接口应当更加灵活
        public void LoadSpells(CharacterSpells characterSpells, VirtualSpell virtualSpell)
        {
            MySpells.Add(characterSpells, virtualSpell);
        }

        public VirtualSpell GetSpell(CharacterSpells spell)
        {
            VirtualSpell vspell = null;
            MySpells.TryGetValue(spell, out vspell);
            return vspell;
        }
    }

}
