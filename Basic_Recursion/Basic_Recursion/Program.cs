using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Recursion
{
    class Program
    {
        static void Main(string[] args)
        {
            int testNum = 5;
            int num_digits = numDigits(testNum);
            Console.WriteLine(testNum + " has " + num_digits + " digits");

            testNum = 42;
            num_digits = numDigits(testNum);
            Console.WriteLine(testNum + " has " + num_digits + " digits");

            testNum = 8743255;
            num_digits = numDigits(testNum);
            Console.WriteLine(testNum + " has " + num_digits + " digits");

            Console.WriteLine();


            String binNumString = "0";
            int decNum = binaryToDecimal(binNumString);
            Console.WriteLine(binNumString + " in decimal = " + decNum);

            binNumString = "1001";
            decNum = binaryToDecimal(binNumString);
            Console.WriteLine(binNumString + " in decimal = " + decNum);

            binNumString = "11100";
            decNum = binaryToDecimal(binNumString);
            Console.WriteLine(binNumString + " in decimal = " + decNum);

            Console.WriteLine();


            String testString = "Apollo 9";
            String testStringResult = spellOutDigits(testString);
            Console.WriteLine("\"" + testString +
                    "\" with digits spelled out is \"" +
                    testStringResult + "\"");

            testString = "CS 2101";
            testStringResult = spellOutDigits(testString);
            Console.WriteLine("\"" + testString +
                    "\" with digits spelled out is \"" +
                    testStringResult + "\"");

            testString = "computer science";
            testStringResult = spellOutDigits(testString);
            Console.WriteLine("\"" + testString +
                    "\" with digits spelled out is \"" +
                    testStringResult + "\"");

            Console.WriteLine();


            String palTest = "Rise to vote sir";
            if (isPalindrome(palTest))
            {
                Console.WriteLine("\"" + palTest + "\" is a palindrome");
            }
            else
            {
                Console.WriteLine("\"" + palTest + "\" is NOT a palindrome");
            }

            palTest = "Are we not drawn onward to New Era";
            if (isPalindrome(palTest))
            {
                Console.WriteLine("\"" + palTest + "\" is a palindrome");
            }
            else
            {
                Console.WriteLine("\"" + palTest + "\" is NOT a palindrome");
            }

            palTest = "This is not a palindrome";
            if (isPalindrome(palTest))
            {
                Console.WriteLine("\"" + palTest + "\" is a palindrome");
            }
            else
            {
                Console.WriteLine("\"" + palTest + "\" is NOT a palindrome");
            }

            Console.ReadKey();
        }

        public static int numDigits(int n)
        {

            if (n < 10)
                return 1;

            return numDigits(n / 10) + 1;

        }


        public static int binaryToDecimal(String n)
        {

            if (n.Equals("0", StringComparison.Ordinal))
            {
                return 0;
            }

            if (n.Equals("1", StringComparison.Ordinal))
            {
                return 1;
            }

            if (n[n.Length - 1] == '0')
            {
                return 2 * binaryToDecimal(n.Substring(0, n.Length - 1));
            }

            return 1 + (2 * binaryToDecimal(n.Substring(0, n.Length - 1)));

        }



        public static String spellOutDigits(String s)
        {

            if (s.Length == 0)
                return "";

            char firstChar = s[0];
            String restWithDigitsSpelled = spellOutDigits(s.Substring(1)) ;

            if (firstChar == '0')
                return "zero" + restWithDigitsSpelled;
            else if (firstChar == '1')
                return "one" + restWithDigitsSpelled;
            else if (firstChar == '2')
                return "two" + restWithDigitsSpelled;
            else if (firstChar == '3')
                return "three" + restWithDigitsSpelled;
            else if (firstChar == '4')
                return "four" + restWithDigitsSpelled;
            else if (firstChar == '5')
                return "five" + restWithDigitsSpelled;
            else if (firstChar == '6')
                return "six" + restWithDigitsSpelled;
            else if (firstChar == '7')
                return "seven" + restWithDigitsSpelled;
            else if (firstChar == '8')
                return "eight" + restWithDigitsSpelled;
            else if (firstChar == '9')
                return "nine" + restWithDigitsSpelled;
            else
                return firstChar + restWithDigitsSpelled;

        }


        public static bool isPalindrome(String s)
        {

            if (s.Length == 1 || s.Length == 0)
            {
                return true;
            }

            String firstChar = s[0].ToString();
            String lastChar = s[s.Length - 1].ToString();

            // take care of leading space, if there is one
            if (firstChar.Equals(' '))
                return isPalindrome(s.Substring(1));

            // take care of trailing space, if there is one
            if (lastChar.Equals(' '))
                return isPalindrome(s.Substring(0, s.Length - 1));

            // if no leading or trailing space, check first and last characters)
            if ( firstChar.ToUpper() != lastChar.ToUpper() )
            {
                Console.WriteLine("Word was: " + s);
                return false;
            }

            // check string without first and last characters
            return isPalindrome(s.Substring(1, s.Length - 2));

        }


    }
}
