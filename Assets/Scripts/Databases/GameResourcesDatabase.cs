using System.Collections.Generic;
using IdleTransport.Utilities;
using UnityEngine;
using static IdleTransport.Utilities.Enums;

namespace IdleTransport.Databases {
    public class GameResourcesDatabase : Singleton<GameResourcesDatabase> {
        [SerializeField]
        private Dictionary<UnitType, Sprite> _unitSpritesDictionary = new Dictionary<UnitType, Sprite>();
        [SerializeField] private Dictionary<StatType, Sprite> _statIconsDictionary = new Dictionary<StatType, Sprite>();

        public static Sprite GetUnitSprite(UnitType unitType) {
            return Instance._unitSpritesDictionary.ContainsKey(unitType)
                ? Instance._unitSpritesDictionary[unitType]
                : null;
        }

        public static Sprite GetStatIcon(StatType statType) {
            return Instance._statIconsDictionary.ContainsKey(statType)
                ? Instance._statIconsDictionary[statType]
                : null;
        }
    }
}
