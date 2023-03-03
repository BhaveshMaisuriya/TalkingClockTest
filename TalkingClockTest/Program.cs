using System.Linq;
using System;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Text.RegularExpressions;

public class TalkingClock
{
    public static bool validateNumericTime(string numericTime)
    {
        if (numericTime == null)
        {
            throw new ArgumentNullException("Time shouldn't be Null");

        }
        else if (!numericTime.Contains(":"))
        {
            throw new NoColonSuppliedException(numericTime);
        }
        else if (numericTime.Split(':')[0] != null && numericTime.Split(':')[1] != null)
        {
            string[] hourMinute = numericTime.Split(':');
            Regex regex = new Regex("^[0-9]+$");

            if (!regex.IsMatch(hourMinute[0]) || !regex.IsMatch(hourMinute[1]))
                throw new HourMinuteNumericException(numericTime);
        }

        return true;
    }


    public static string convertHumanReadableTime(int hourTiming, int minuteTiming)
    {
        String[] units = { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve" };
        String[] teenMinutes = { "", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        String[] tensMinutes = { "", "ten", "twenty" };
        String[] quarters = { "", "quarter", "half" };
        String[] fillerWords = { " o'clock", " past ", " to " };

        if (hourTiming > 24 || hourTiming < 0 || minuteTiming >= 60 || minuteTiming < 0)
        {
            return "Invalid Input";
        }
        if (hourTiming > 12) hourTiming = hourTiming - 12;
        if (minuteTiming == 0)
        {
            return units[hourTiming] + fillerWords[0];
        }
        String fillerWord = fillerWords[1];
        if (minuteTiming > 30)
        {
            minuteTiming = 60 - minuteTiming;
            hourTiming++;
            fillerWord = fillerWords[2];
        }
        if (minuteTiming % 15 == 0)
        {
            return quarters[minuteTiming / 15] + fillerWord + units[hourTiming];
        }
        else if (minuteTiming % 10 == 0)
        {
            return tensMinutes[minuteTiming / 10] + fillerWord + units[hourTiming];
        }
        else if (minuteTiming < 10 || minuteTiming > 20)
        {
            return tensMinutes[minuteTiming / 10] + " " + units[minuteTiming % 10] + fillerWord + units[hourTiming];
        }
        else
        {
            return teenMinutes[minuteTiming % 10] + fillerWord + units[hourTiming];
        }
    }


}
class MainClass
{
    public static void Main(string[] args)
    {
        String hourMinute = String.Empty;

        // Test if input arguments were supplied.
        if (args.Length == 0)
        {
            Console.WriteLine("Getting current time Hour and Minute...");
            hourMinute = DateTime.Now.ToString("HH:mm");
        }
        else
        {
            hourMinute = args[0];
        }
        // Validating input
        TalkingClock.validateNumericTime(hourMinute);

        // validation check passed and process further
        String[] time = hourMinute.Split(":");

        int hourTiming = int.Parse(time[0]);
        int minuteTiming = int.Parse(time[1]);
        String result = TalkingClock.convertHumanReadableTime(hourTiming, minuteTiming);

        result = result.Substring(0, 1).ToUpper() + result.Substring(1).ToLower();
        //Output the result
        Console.WriteLine(result);
        Console.ReadKey();

    }


}


[Serializable]
public class NoColonSuppliedException : Exception
{
    public NoColonSuppliedException(string? time) : base(String.Format("Time should contains colon : {0}", time)) { }

}

[Serializable]
public class HourMinuteNumericException : Exception
{
    public HourMinuteNumericException(string? time) : base(String.Format("Hour and Minute should Numeric : {0}", time)) { }

}