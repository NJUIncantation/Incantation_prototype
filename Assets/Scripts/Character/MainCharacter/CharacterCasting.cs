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
            Spell_J
        }

        public Dictionary<CharacterSpells, VirtualSpell> MySpells = new Dictionary<CharacterSpells, VirtualSpell>();

        //TODO: 技能的接口应当更加灵活
        public void LoadSpells()
        {
            MySpells.Add(CharacterSpells.Spell_J, new SpellLightning());
        }

        public VirtualSpell GetSpell(CharacterSpells spell)
        {
            VirtualSpell vspell = null;
            MySpells.TryGetValue(spell, out vspell);
            return vspell;
        }
    }

}
