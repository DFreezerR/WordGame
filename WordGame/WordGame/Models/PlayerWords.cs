using System;
using System.Collections.Generic;
using System.Text;

namespace WordGame.Models
{
    public class PlayerWords
    {
        public string Player { get; set; }
        public List<Game> Guessed { get; set; }
        public List<Game> NotGuessed { get; set; }
        public Game Active { get; set; }
    }
}
