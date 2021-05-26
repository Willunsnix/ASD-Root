using System;

namespace ASD.Forms.Helpers
{
    public class MathHelper : object
    {
        public static float GetRadian(float val)
        {
            return (float)(val * Math.PI / 180);
        }
    }
}