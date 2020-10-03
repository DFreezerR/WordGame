using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
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
        public static FirebaseConfig cfg = new FirebaseConfig() { AuthSecret = "8WfR7vpKdavySh5qICoGgLBJ3wGG67Zsz7cYExxm", BasePath = "https://wordgame-bf41c.firebaseio.com/" };
        public static IFirebaseClient firebaseClient = new FirebaseClient(cfg);
    }
}
