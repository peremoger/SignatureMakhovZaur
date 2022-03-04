using System;

namespace SignatureMakhovZaur
{
    static class Converter
    {
        public static long Convert(string value)
        {
            try
            {
                long number = Int64.Parse(value);

                if (number <= 0) throw new ArgumentException();

                return number;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Значение не может быть меньше или равно нолю");
                Console.WriteLine(e.StackTrace);
                Environment.Exit(0);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Невозможно конвертировать '{0}'.", value);
                Console.WriteLine(e.StackTrace);
                Environment.Exit(0);
            }
            catch (OverflowException e)
            {
                Console.WriteLine("'{0}' находится за границами Int64.", value);
                Console.WriteLine(e.StackTrace);
                Environment.Exit(0);
            }
            
            return 0;
        }
    }
}
