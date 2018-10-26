using System;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace examen
{
    class Program
    {
        static void Main(string[] args)
        {

            var jsonStr = File.ReadAllText("data.json");
            var json = JObject.Parse(jsonStr);

            var sb = new StringBuilder();

            sb.AppendLine(File.ReadAllText("start.html"));

            foreach (JObject q in json["questions"].AsJEnumerable())
            {
                var question = q.Value<string>("q");
                var image = q.Value<string>("i");

                sb.AppendLine($"<p class='question'>{question}</p>");
                if (!string.IsNullOrWhiteSpace(image))
                {
                    sb.AppendLine(image);
                }
                var options = q["a"].AsJEnumerable();
                sb.AppendLine("<ul>");
                foreach (JObject o in options)
                {
                    var text = o.Value<string>("option");
                    var correct = o.Value<bool>("correct");
                    var cssClass = correct ? "correct" : "incorrect";
                    sb.AppendLine($"<li class='{cssClass}'>{text}</li>");
                }
                sb.AppendLine("</ul>");
            }
            sb.AppendLine("<br />");
            sb.AppendLine("<br />");

            sb.AppendLine(File.ReadAllText("end.html"));

            File.WriteAllText("index.html", sb.ToString(), Encoding.UTF8);
        }
    }
}
