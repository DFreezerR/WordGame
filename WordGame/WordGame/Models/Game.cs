using System;
using System.Collections.Generic;
using System.Text;

namespace WordGame.Models
{
    public class Game
    {
        public string Word { get; set; }
        public byte WordDiff { get; set; }
        public byte GameDiff { get; set; }
        public DateTime Date { get; set; }
    }
}
