using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Engine
{
    /// <summary>
    /// A class for handling all types of assets
    /// </summary>
    public class AssetManager
    {
        ContentManager contentManager;

        public AssetManager(ContentManager Content)
        {
            contentManager = Content;
        }

        /// <summary>
        /// Loads and returns the sprite with the given asset name.
        /// </summary>
        /// <param name="sprite">The name of the asset to load</param>
        /// <returns>A Texture2D object containing the loaded sprite.</returns>
        public Texture2D LoadSprite(string sprite)
        {
            return contentManager.Load<Texture2D>(sprite);
        }

        /// <summary>
        /// Loads and returns the font with the given asset name.
        /// </summary>
        /// <param name="font">The name of the font to load</param>
        /// <returns>A SpriteFont object containing the loaded font.</returns>
        public SpriteFont LoadFont(string font)
        {
            return contentManager.Load<SpriteFont>(font);
        }

        /// <summary>
        /// Loads and plays the soundeffect with the given asset name
        /// </summary>
        /// <param name="soundEffect">The name of the asset to load</param>
        public void PlaySoundEffect(string soundEffect)
        {
            SoundEffect snd = contentManager.Load<SoundEffect>(soundEffect);
            snd.Play();
        }

        /// <summary>
        /// Loads and plays the song with the given asset name.
        /// </summary>
        /// <param name="song">The name of the song to load.</param>
        /// <param name="loop">Whether or not the song should loop.</param>
        public void PlaySong(string song, bool loop)
        {
            MediaPlayer.IsRepeating = loop;
            MediaPlayer.Play(contentManager.Load<Song>(song));
        }
    }
}