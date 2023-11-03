using System;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleKey key; double[] octaveList = getOctave();
            while ((key = Console.ReadKey(true).Key) != ConsoleKey.Escape)
            {
                switch (key)
                {
                    case ConsoleKey.F1: { octaveList = getOctave(0); break; }
                    case ConsoleKey.F2: { octaveList = getOctave(1); break; }
                    case ConsoleKey.F3: { octaveList = getOctave(2); break; }
                    case ConsoleKey.F4: { octaveList = getOctave(3); break; }
                    case ConsoleKey.F5: { octaveList = getOctave(4); break; }
                    case ConsoleKey.F6: { octaveList = getOctave(5); break; }
                    case ConsoleKey.F7: { octaveList = getOctave(6); break; }
                    case ConsoleKey.F8: { octaveList = getOctave(7); break; }

                    case ConsoleKey.D: { getSound(octaveList[0]); break; };
                    case ConsoleKey.F: { getSound(octaveList[2]); break; };
                    case ConsoleKey.G: { getSound(octaveList[4]); break; };
                    case ConsoleKey.H: { getSound(octaveList[5]); break; };
                    case ConsoleKey.J: { getSound(octaveList[7]); break; };
                    case ConsoleKey.K: { getSound(octaveList[9]); break; };
                    case ConsoleKey.L: { getSound(octaveList[11]); break; };

                    case ConsoleKey.R: { getSound(octaveList[1]); break; };
                    case ConsoleKey.T: { getSound(octaveList[3]); break; };
                    case ConsoleKey.Y: { getSound(octaveList[6]); break; };
                    case ConsoleKey.U: { getSound(octaveList[8]); break; };
                    case ConsoleKey.I: { getSound(octaveList[10]); break; };
                }
            }
        }
        static double[] getOctave(int octave = 0)
        {
            double[] octave0 = new[] { 16.35, 17.32, 18.35, 19.45, 20.6, 21.83, 23.12, 24.5, 25.96, 27.5, 29.14, 30.87 };
            double[] octave1 = new[] { 32.7, 34.65, 36.71, 38.89, 41.2, 43.65, 46.25, 49.0, 51.91, 55.0, 58.27, 61.74 };
            double[] octave2 = new[] { 65.41, 69.3, 73.42, 77.78, 82.41, 87.31, 92.5, 98.0, 103.8, 110.0, 116.5, 123.5 };
            double[] octave3 = new[] { 130.8, 138.6, 146.8, 155.6, 164.8, 174.6, 185.0, 196.0, 207.7, 220.0, 233.1, 246.9 };
            double[] octave4 = new[] { 261.6, 277.2, 293.7, 311.1, 329.6, 349.2, 370.0, 392.0, 415.3, 440.0, 466.2, 493.9 };
            double[] octave5 = new[] { 523.3, 554.4, 587.3, 622.3, 659.3, 698.5, 740.0, 784.0, 830.6, 880.0, 932.3, 987.8 };
            double[] octave6 = new[] { 1047.0, 1109.0, 1175.0, 1245.0, 1319.0, 1397.0, 1480.0, 1568.0, 1661.0, 1760.0, 1865, 1976 };
            double[] octave7 = new[] { 2093.0, 2217.0, 2349.0, 2489.0, 2637.0, 2794.0, 2960.0, 3136.0, 3322.0, 3520.0, 3729.0, 3951.0 };
            double[] octave8 = new[] { 4186.0, 4435.0, 4699.0, 4978.0, 5274.0, 5588.0, 5920.0, 6272.0, 6645.0, 7040.0, 7459.0, 7902.0 };

            switch (octave)
            {
                case 1:return octave1;
                case 2:return octave2;
                case 3: return octave3;
                case 4: return octave4;
                case 5: return octave5;
                case 6: return octave6;
                case 7: return octave7;

            }
            return octave0;
        }
        static void getSound(double note)
        {
            try
            {
                Console.Beep(Convert.ToInt32(note), 190);
            }
            catch { }
        }
    }
}