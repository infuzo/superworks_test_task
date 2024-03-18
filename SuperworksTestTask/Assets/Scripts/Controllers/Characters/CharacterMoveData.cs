using ZiplineValley.Views.Character;

namespace ZiplineValley.Controllers.Characters
{
    internal class CharacterMoveData 
    {
        public int CurrentPointIndex { get; set; } = -1;
        public CharacterView Character { get; set; }
        public float PassedDistance { get; set; }
        public bool WasMovementStarted { get; set; }
    }
}
