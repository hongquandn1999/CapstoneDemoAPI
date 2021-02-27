using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneDemo.Models
{
    public class Character
    {
        public int CharacterId { get; set; }
        public string CharacterName { get; set; }
        public string MovieName { get; set; }
        public string DateOfRelease { get; set; }
        public string PhotoFileName { get; set; }
    }
}
