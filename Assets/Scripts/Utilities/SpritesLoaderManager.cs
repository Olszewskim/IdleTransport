using System;
using System.Collections.Generic;
using UnityEngine;

namespace IdleTransport.Utilities
{
    public static class SpritesLoaderManager {
        private static Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();

        public static Sprite LoadIcon(string iconName) {
            return LoadSprite(Constants.RESOURCES_ICONS_FOLDER_NAME, iconName, Constants.CURRENCY_ATLAS_NAME);
        }


        public static Sprite LoadSprite(string spriteName) {
            return LoadSprite(Constants.SPRITES_FOLDER_NAME, spriteName);
        }

        private static Sprite LoadSprite(string path, string spriteName, string multipleSpriteName = "") {
            var spriteFullName = $"{path}/{multipleSpriteName}{spriteName}";
            if (_sprites.ContainsKey(spriteFullName)) {
                return _sprites[spriteFullName];
            }

            Sprite[] sprites;

            if (multipleSpriteName == "") {
                sprites = Resources.LoadAll<Sprite>(spriteFullName);
            } else {
                sprites = Resources.LoadAll<Sprite>($"{path}/{multipleSpriteName}");
            }

            if (sprites.Length == 0) {
                throw new Exception($"There is no sprite with ID {spriteFullName}");
            }

            for (int i = 0; i < sprites.Length; i++) {
                _sprites.Add($"{path}/{multipleSpriteName}{sprites[i].name}", sprites[i]);
            }

            if (!_sprites.ContainsKey(spriteFullName)) {
                throw new Exception($"There is no sprite with ID {spriteFullName}");
            }

            return _sprites[spriteFullName];
        }
    }
}
