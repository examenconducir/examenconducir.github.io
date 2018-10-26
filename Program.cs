using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace examen
{
    class Program
    {
        static void Main(string[] args)
        {

            var jsonStr = File.ReadAllText("data.json");
            var json = JObject.Parse(jsonStr);

            foreach (JObject q in json["questions"].AsJEnumerable())
            {
                var question = q.Value<string>("q");
                System.Console.WriteLine($"<p class='question'>{question}</p>");
                var options = q["a"].AsJEnumerable();
                System.Console.WriteLine("<ul>");
                foreach (JObject o in options)
                {
                    var text = o.Value<string>("option");
                    var correct = o.Value<bool>("correct");
                    var cssClass = correct ? "correct" : "incorrect";
                    System.Console.WriteLine($"<li class='{cssClass}'>{text}</li>");
                }
                System.Console.WriteLine("</ul>");
            }
            System.Console.WriteLine("<br />");
            System.Console.WriteLine("<br />");
        }
    }
}
