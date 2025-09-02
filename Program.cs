// See https://aka.ms/new-console-template for more information
using System.Data;

//ask for input
Console.WriteLine("Enter 1 to create data file.");
Console.WriteLine("Enter 2 to parse data file.");
Console.WriteLine("Emter anything else to quit.");
// input response
string? resp = Console.ReadLine();

if (resp == "1")
{
    // create data file
    // ask question
    Console.WriteLine("How many weeks of data is needed?");
    // input the response (convert to int)
    int weeks = Convert.ToInt32(Console.ReadLine());
    // determine start and end date
    DateTime today = DateTime.Now;
    // we want fulll weeks sunday - saturday
    DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
    // subtract # of weeks from endDate to get startDate
    DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
    // random number generator
    Random rnd = new();
    // create file
    StreamWriter sw = new("data.txt");

    // loop for desired number of weeks provided by user
    while (dataDate < dataEndDate)
    {
        // 7 days in a week
        int[] hours = new int[7];
        for (int i = 0; i < hours.Length; i++)
        {
            // generate random number between 4-12 inclusive
            hours[i] = rnd.Next(4, 13);
        }
        // M/d/yy,#/#/#/#/#/#/#
        sw.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
        // add 1 week to date
        dataDate = dataDate.AddDays(7);
    }
    sw.Close();
}
if (resp == "2")
{
    // parse data file
    using (StreamReader sr = new StreamReader("data.txt")){
        while (!sr.EndOfStream)
        {
            // grab entry
            string? line = sr.ReadLine();
            // separate entry as needed
            string dateString = line.Substring(0, line.IndexOf(","));
            string hoursString = line.Substring(line.IndexOf(",") + 1);

            DateTime date = DateTime.Parse(dateString);
            string[] hours = hoursString.Split("|");
            // display entry
            Console.WriteLine($"Week of {date:MMM} {date:dd}, {date:yyyy}");
            Console.WriteLine("Su Mo Tu We Th Fr Sa");
            Console.WriteLine("-- -- -- -- -- -- --");
            foreach (string hour in hours)
            {
                Console.Write($"{hour} ");
            }
            Console.WriteLine();
            }
            sr.Close();
        }

}
