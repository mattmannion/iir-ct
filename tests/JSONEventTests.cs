using Models;
using Newtonsoft.Json;

namespace tests;

[TestClass]
public class JSONEventTests
{
  JSONEvent ShortEvent = new JSONEvent(
    100,
    "December 1st Event",
    "VALOR",
    "2022-12-01T08:00:00.00-04:00",
    "2022-12-01T17:00:00.00-04:00",
    "https://www.valorforblue.org/Events/Dec-1-Event",
    "msmith@iir.com"
  );

  JSONEvent LongEvent = new JSONEvent(
    300,
    "November 1st Event",
    "VALOR",
    "2022-11-01T08:00:00.00-04:00",
    "2022-11-03T17:00:00.00-04:00",
    "https://www.valorforblue.org/Events/Nov-1-Event",
    "ksmith@iir.com"
  );

  // It confirm the event length is corrent fot the given UTC DateTimes
  [TestMethod]
  public void TestsShortEventLength()
  {
    Assert.AreEqual("09h 00m 00s", ShortEvent.EventLength());
  }

  // It confirm the event length is corrent fot the given UTC DateTimes
  [TestMethod]
  public void TestsLongEventLength()
  {
    Assert.AreEqual("2 day(s) and 09h 00m 00s", LongEvent.EventLength());
  }

  // Tests if the method retrieves an expected event.
  [TestMethod]
  public async Task TestsGetEvent()
  {
    JSONEvent result = await JSONEvent.GetEvent("data/events.json", 100);

    Assert.AreEqual(
      JsonConvert.SerializeObject(ShortEvent),
      JsonConvert.SerializeObject(result)
    );

    // Note: This could be considered an Intergration Test by some.
    // I'd say this falls into the Unit Test category since it needs
    // to read a static file that must be provided at run time and
    // that the schema for said file is required to be followed
    // for any future files to be processed in the future.
  }
}