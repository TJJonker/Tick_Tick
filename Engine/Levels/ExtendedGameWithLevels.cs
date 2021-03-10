using System.Collections.Generic;
using System.IO;

namespace Engine
{
    public abstract class ExtendedGameWithLevels : ExtendedGame
    {
        // Easier accessible StateNames
        public static string StateName_Title = "title";
        public static string StateName_Help = "help";
        public static string StateName_LevelSelect = "levelselect";
        public static string StateName_Playing = "playing";

        /// <summary>
        /// Variable containing the progress saved in a text file 
        /// </summary>
        private static List<LevelStatus> progress;

        /// <summary>
        /// Returns the total number of levels in the game
        /// </summary>
        public static int NumberOfLevels { get { return progress.Count; } }

        /// <summary>
        /// Loads the level progress from a text file
        /// </summary>
        protected void LoadProgress()
        {
            // Prepare a list for the progress
            progress = new List<LevelStatus>();

            // Read the textfile and saved the progress
            StreamReader r = new StreamReader("Content/Levels/levels_status.txt");
            string line = r.ReadLine();
            while (line != null)
            {
                // Adds status depending on textfile line
                if (line == "locked") progress.Add(LevelStatus.Locked);
                else if (line == "unlocked") progress.Add(LevelStatus.Unlocked);
                else if (line == "solved") progress.Add(LevelStatus.Solved);

                // Go to the next line
                line = r.ReadLine();
            }

            // Closes the stream
            r.Close();
        }

        /// <summary>
        /// Returns the <see cref="LevelStatus"/> of the level with the given index
        /// </summary>
        /// <param name="levelIndex">The index of the level to check</param>
        /// <returns>the <see cref="LevelStatus"/> of the requested index</returns>
        public static LevelStatus GetLevelStatus(int levelIndex)
        {
            return progress[levelIndex - 1];
        }

        /// <summary>
        /// Sets the <see cref="LevelStatus"/> of the given level to the given status value
        /// </summary>
        /// <param name="levelIndex">The index of the level to change</param>
        /// <param name="status">The new status of the given level</param>
        private static void SetStatusLevel(int levelIndex, LevelStatus status)
        {
            progress[levelIndex - 1] = status;
        }

        /// <summary>
        /// Marks a certain level as completed and marks the following level,
        /// if applicable, as unlocked and saves the progress.
        /// </summary>
        /// <param name="levelIndex">The index of the level to mark as completed</param>
        public static void MarkLevelAsCompleted(int levelIndex)
        {
            // Mark the level as solved
            SetStatusLevel(levelIndex, LevelStatus.Solved);

            // If there is a next level, mark it as unlocked
            if (levelIndex < NumberOfLevels && GetLevelStatus(levelIndex + 1) == LevelStatus.Locked)
                SetStatusLevel(levelIndex + 1, LevelStatus.Unlocked);

            // Store the new level status
            SaveProgress();
        }

        /// <summary>
        /// Writes the entries in the progress variable to the designated text file
        /// </summary>
        private static void SaveProgress()
        {
            // Opens the stream with the textfile
            StreamWriter w = new StreamWriter("Content/Levels/levels_status.txt");
            foreach (LevelStatus status in progress)
            {
                // Writes a certain line depending on the Levelstatus in progress
                if (status == LevelStatus.Locked) w.WriteLine("locked");
                else if (status == LevelStatus.Solved) w.WriteLine("solved");
                else if (status == LevelStatus.Unlocked) w.WriteLine("unlocked");
            }
            // Closes the stream
            w.Close();
        }

        /// <summary>
        /// Sends the player to the next level, if this level exists. 
        /// Otherwise to the levelselect menu
        /// </summary>
        /// <param name="levelIndex">the levelIndex of the current level</param>
        public static void GoToNextLevel(int levelIndex)
        {
            // If this is the last level, go abck to the level selection menu
            if (levelIndex == NumberOfLevels)
                GameStateManager.SwitchTo(StateName_LevelSelect);

            // Otherwise go to the next level
            else
                GetPlayingState().LoadLevel(levelIndex + 1);
        }

        /// <summary>
        /// Returns the game state with the key StateName_Playing, cast to an IPlayingState object.
        /// </summary>
        /// <returns>The game state with the key StateName_Playing, cast to an IPlayingState object.</returns>
        public static IPlayingState GetPlayingState()
        {
            return (IPlayingState)GameStateManager.GetGameState(StateName_Playing);
        }
    }
}