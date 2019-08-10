using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Engine
{
    /// <summary>
    /// A class for handling all types of assets: sprites, fonts, music, and sound effects.
    /// </summary>
    public class AssetManager
    {
        ContentManager contentManager;

        public AssetManager(ContentManager content)
        {
            contentManager = content;
        }

        /// <summary>
        /// Loads and returns the sprite with the given asset name.
        /// </summary>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <returns>A Texture2D object containing the loaded sprite.</returns>
        public Texture2D LoadSprite(string assetName)
        {
            return contentManager.Load<Texture2D>(assetName);
        }

        /// <summary>
        /// Loads and returns the font with the given asset name.
        /// </summary>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <returns>A SpriteFont object containing the loaded font.</returns>
        public SpriteFont LoadFont(string assetName)
        {
            return contentManager.Load<SpriteFont>(assetName);
        }

        /// <summary>
        /// Loads and plays the sound effect with the given asset name.
        /// </summary>
        /// <param name="assetName">The name of the asset to load.</param>
        public void PlaySoundEffect(string assetName)
        {
            SoundEffect snd = contentManager.Load<SoundEffect>(assetName);
            snd.Play();
        }

        /// <summary>
        /// Loads and plays the song with the given asset name.
        /// </summary>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <param name="repeat">Whether or not the song should loop.</param>
        public void PlaySong(string assetName, bool repeat)
        {
            MediaPlayer.IsRepeating = repeat;
            MediaPlayer.Play(contentManager.Load<Song>(assetName));
        }

    }
}