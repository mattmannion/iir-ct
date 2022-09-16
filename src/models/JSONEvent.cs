using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Models;

public class JSONEvent
{
  int id;
  string program, dateStart, dateEnd, owner;
  public string name, url;

  public JSONEvent(
    int id,
    string name,
    string program,
    string dateStart,
    string dateEnd,
    string url,
    string owner
    )
  {
    this.id = id;
    this.name = name;
    this.program = program;
    this.dateStart = dateStart;
    this.dateEnd = dateEnd;
    this.url = url;
    this.owner = owner;
  }

  string[] HMS(string[] time_parts, int position)
  {
    return time_parts[position].Split(":");
  }

  string FormatHMS(string h, string m, string s)
  {
    return h + "h " + m + "m " + s + "s";
  }

  public string EventLength()
  {

    DateTime start = DateTime.Parse(dateStart);
    DateTime end = DateTime.Parse(dateEnd);

    TimeSpan diff = end - start;

    string[] time_parts = diff.ToString().Split(".");

    if (time_parts.Length > 1)
    {
      string[] hms = HMS(time_parts, 1);

      return time_parts[0] + " day(s) and " + FormatHMS(hms[0], hms[1], hms[2]);
    }
    else
    {
      string[] hms = HMS(time_parts, 0);

      return FormatHMS(hms[0], hms[1], hms[2]);
    }
  }

  public static async Task<JSONEvent> GetEvent(string url, int id)
  {
    StreamReader reader = new StreamReader(url);
    JSONEvent[] events = JsonConvert.DeserializeObject<JSONEvent[]>(reader.ReadToEnd())
    ?? throw new ArgumentException();

    JSONEvent result = Array.Find(events, e => e.id == id);

    return await Task.FromResult(result);
  }
}