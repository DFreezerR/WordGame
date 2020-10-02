using System;
using System.Collections.Generic;
using System.Text;

namespace WordGame.Models
{
    static class Settings
    {
        public enum GameDifficulties
        { 
            Easy, Normal, Advanced, Hard, Extreme
        }
        public enum WordDifficulties
        {
            Easy, Normal, Advanced, Hard, Extreme
        }
        public static Player currentPLayer { get; set; }
    }
}
