using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Visma;

namespace ApplicationTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DescriptionFilterTest()
        {
            List<string> attendees = new List<string>() { "As", "Tu", "Jis" };
            List<Meeting> meetingList = new List<Meeting>();
            meetingList.Add(new Meeting("Meeting 1", "Justas", "Description .NET", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), attendees));
            meetingList.Add(new Meeting("Meeting 1", "Justas", "Description .letas", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), attendees));
            meetingList.Add(new Meeting("Meeting 1", "Justas", "Description .met", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), attendees));
            meetingList.Add(new Meeting("Meeting 4", "Justas", "Description .netas", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), attendees));

            List<Meeting> expected = new List<Meeting>();
            expected.Add(new Meeting("Meeting 1", "Justas", "Description .NET", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), attendees));
            expected.Add(new Meeting("Meeting 4", "Justas", "Description .netas", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), attendees));
            var filter = new Filter(meetingList);

            List<Meeting> actual = filter.FilterMeeting(new DescriptionStrategy(), ".nEt");

            AssertMeetings(expected, actual);

        }
        [TestMethod]
        public void ResponsiblePersonFilterTest()
        {
            List<string> attendees = new List<string>() { "As", "Tu", "Jis" };
            List<Meeting> meetingList = new List<Meeting>();
            meetingList.Add(new Meeting("Meeting 1", "Justinas", "Description .NET", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), attendees));
            meetingList.Add(new Meeting("Meeting 1", "Juste", "Description .letas", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), attendees));
            meetingList.Add(new Meeting("Meeting 1", "Gustas", "Description .met", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), attendees));
            meetingList.Add(new Meeting("Meeting 4", "Justas", "Description .netas", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), attendees));

            List<Meeting> expected = new List<Meeting>();
            expected.Add(new Meeting("Meeting 1", "Justinas", "Description .NET", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), attendees));
            expected.Add(new Meeting("Meeting 1", "Juste", "Description .letas", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), attendees));
            expected.Add(new Meeting("Meeting 4", "Justas", "Description .netas", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), attendees));
            var filter = new Filter(meetingList);

            List<Meeting> actual = filter.FilterMeeting(new ResponsiblePersonStrategy(), "jUst");

            AssertMeetings(expected, actual);

        }
        [TestMethod]
        public void CategoryFilterTest()
        {
            List<string> attendees = new List<string>() { "As", "Tu", "Jis" };
            List<Meeting> meetingList = new List<Meeting>();
            meetingList.Add(new Meeting("Meeting 1", "Justas", "Description .NET", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), attendees));
            meetingList.Add(new Meeting("Meeting 1", "Justas", "Description .letas", Category.Short, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), attendees));
            meetingList.Add(new Meeting("Meeting 1", "Justas", "Description .met", Category.CodeMonkey, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), attendees));
            meetingList.Add(new Meeting("Meeting 4", "Justas", "Description .netas", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), attendees));

            List<Meeting> expected = new List<Meeting>();
            expected.Add(new Meeting("Meeting 1", "Justas", "Description .NET", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), attendees));
            expected.Add(new Meeting("Meeting 4", "Justas", "Description .netas", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), attendees));
            var filter = new Filter(meetingList);

            List<Meeting> actual = filter.FilterMeeting(new CategoryStrategy(), Category.Hub.ToString());

            AssertMeetings(expected, actual);
        }
        [TestMethod]
        public void TypeFilterTest()
        {
            List<string> attendees = new List<string>() { "As", "Tu", "Jis" };
            List<Meeting> meetingList = new List<Meeting>();
            meetingList.Add(new Meeting("Meeting 1", "Justas", "Description .NET", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), new List<string>() { "As", "Tu", "Jis" }));
            meetingList.Add(new Meeting("Meeting 1", "Justas", "Description .letas", Category.Short, Visma.Type.InPerson, DateTime.Today, DateTime.Today.AddDays(1), new List<string>() { "As", "Tu", "Jis" }));
            meetingList.Add(new Meeting("Meeting 1", "Justas", "Description .met", Category.CodeMonkey, Visma.Type.InPerson, DateTime.Today, DateTime.Today.AddDays(1), new List<string>() { "As", "Tu", "Jis" }));
            meetingList.Add(new Meeting("Meeting 4", "Justas", "Description .netas", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), new List<string>() { "As", "Tu", "Jis" }));

            List<Meeting> expected = new List<Meeting>();
            expected.Add(new Meeting("Meeting 1", "Justas", "Description .NET", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), new List<string>() { "As", "Tu", "Jis" }));
            expected.Add(new Meeting("Meeting 4", "Justas", "Description .netas", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), new List<string>() { "As", "Tu", "Jis" }));
            var filter = new Filter(meetingList);

            List<Meeting> actual = filter.FilterMeeting(new TypeStrategy(), Visma.Type.Live.ToString());

            AssertMeetings(expected, actual);
        }
        [TestMethod]
        public void AttendeesFilterTest()
        {
            List<string> attendees = new List<string>() { "As", "Tu", "Jis" };
            List<Meeting> meetingList = new List<Meeting>();
            meetingList.Add(new Meeting("Meeting 1", "Justas", "Description .NET", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), new List<string>() { "As", "Tu" }));
            meetingList.Add(new Meeting("Meeting 1", "Justas", "Description .letas", Category.Short, Visma.Type.InPerson, DateTime.Today, DateTime.Today.AddDays(1), new List<string>() { "As", "Tu", "Jis", "Ji" }));
            meetingList.Add(new Meeting("Meeting 1", "Justas", "Description .met", Category.CodeMonkey, Visma.Type.InPerson, DateTime.Today, DateTime.Today.AddDays(1), new List<string>() { "As" }));
            meetingList.Add(new Meeting("Meeting 4", "Justas", "Description .netas", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), new List<string>() { "As", "Tu", "Jis", "Mes", "Ji" }));
            List<Meeting> expected1 = new List<Meeting>();
            expected1.Add(new Meeting("Meeting 1", "Justas", "Description .letas", Category.Short, Visma.Type.InPerson, DateTime.Today, DateTime.Today.AddDays(1), new List<string>() { "As", "Tu", "Jis", "Ji" }));
            expected1.Add(new Meeting("Meeting 4", "Justas", "Description .netas", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(1), new List<string>() { "As", "Tu", "Jis", "Mes", "Ji" }));
            List<Meeting> expected2 = new List<Meeting>();
            expected2.Add(new Meeting("Meeting 1", "Justas", "Description .letas", Category.Short, Visma.Type.InPerson, DateTime.Today, DateTime.Today.AddDays(1), new List<string>() { "As", "Tu", "Jis", "Ji" }));
            List<Meeting> expected3 = new List<Meeting>();
            expected3.Add(new Meeting("Meeting 1", "Justas", "Description .met", Category.CodeMonkey, Visma.Type.InPerson, DateTime.Today, DateTime.Today.AddDays(1), new List<string>() { "As" }));


            var filter = new Filter(meetingList);
            int i = 2;
            List<Meeting> actual1 = filter.FilterMeeting(new AttendeesStrategy(Quantity.MORE, ""), i.ToString());
            List<Meeting> actual2 = filter.FilterMeeting(new AttendeesStrategy(Quantity.BETWEEN, "5"), i.ToString());
            List<Meeting> actual3 = filter.FilterMeeting(new AttendeesStrategy(Quantity.LESS, ""), i.ToString());


            AssertMeetings(expected1, actual1);
            AssertMeetings(expected2, actual2);
            AssertMeetings(expected3, actual3);

        }
        [TestMethod]
        public void DateFilterTest()
        {
            List<string> attendees = new List<string>() { "As", "Tu", "Jis" };
            List<Meeting> meetingList = new List<Meeting>();
            meetingList.Add(new Meeting("Meeting 1", "Justas", "Description .NET", Category.Hub, Visma.Type.Live, DateTime.Today.AddDays(-4), DateTime.Today.AddDays(1), new List<string>() { "As", "Tu" }));
            meetingList.Add(new Meeting("Meeting 2", "Justas", "Description .letas", Category.Short, Visma.Type.InPerson, DateTime.Today.AddDays(-2), DateTime.Today.AddDays(4), new List<string>() { "As", "Tu", "Jis", "Ji" }));
            meetingList.Add(new Meeting("Meeting 3", "Justas", "Description .met", Category.CodeMonkey, Visma.Type.InPerson, DateTime.Today, DateTime.Today.AddDays(1), new List<string>() { "As" }));
            meetingList.Add(new Meeting("Meeting 4", "Justas", "Description .netas", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(3), new List<string>() { "As", "Tu", "Jis", "Mes", "Ji" }));
            List<Meeting> expected1 = new List<Meeting>();
            expected1.Add(new Meeting("Meeting 1", "Justas", "Description .NET", Category.Hub, Visma.Type.Live, DateTime.Today.AddDays(-4), DateTime.Today.AddDays(1), new List<string>() { "As", "Tu" }));
            expected1.Add(new Meeting("Meeting 2", "Justas", "Description .letas", Category.Short, Visma.Type.InPerson, DateTime.Today.AddDays(-2), DateTime.Today.AddDays(4), new List<string>() { "As", "Tu", "Jis", "Ji" }));
            List<Meeting> expected2 = new List<Meeting>();
            expected2.Add(new Meeting("Meeting 1", "Justas", "Description .NET", Category.Hub, Visma.Type.Live, DateTime.Today.AddDays(-4), DateTime.Today.AddDays(1), new List<string>() { "As", "Tu" }));
            expected2.Add(new Meeting("Meeting 2", "Justas", "Description .letas", Category.Short, Visma.Type.InPerson, DateTime.Today.AddDays(-2), DateTime.Today.AddDays(4), new List<string>() { "As", "Tu", "Jis", "Ji" }));
            List<Meeting> expected3 = new List<Meeting>();
            expected3.Add(new Meeting("Meeting 1", "Justas", "Description .NET", Category.Hub, Visma.Type.Live, DateTime.Today.AddDays(-4), DateTime.Today.AddDays(1), new List<string>() { "As", "Tu" }));
            expected3.Add(new Meeting("Meeting 2", "Justas", "Description .letas", Category.Short, Visma.Type.InPerson, DateTime.Today.AddDays(-2), DateTime.Today.AddDays(4), new List<string>() { "As", "Tu", "Jis", "Ji" }));
            expected3.Add(new Meeting("Meeting 3", "Justas", "Description .met", Category.CodeMonkey, Visma.Type.InPerson, DateTime.Today, DateTime.Today.AddDays(1), new List<string>() { "As" }));
            expected3.Add(new Meeting("Meeting 4", "Justas", "Description .netas", Category.Hub, Visma.Type.Live, DateTime.Today, DateTime.Today.AddDays(3), new List<string>() { "As", "Tu", "Jis", "Mes", "Ji" }));

            var filter = new Filter(meetingList);
            DateTime i = DateTime.Today.AddDays(-1);


            List<Meeting> actual1 = filter.FilterMeeting(new DateStrategy(When.BEFORE, DateTime.Today), i.ToString());
            List<Meeting> actual2 = filter.FilterMeeting(new DateStrategy(When.BETWEEN, DateTime.Today), i.ToString());
            List<Meeting> actual3 = filter.FilterMeeting(new DateStrategy(When.AFTER, DateTime.Today), i.ToString());


            AssertMeetings(expected1, actual1);
            AssertMeetings(expected2, actual2);
            //AssertMeetings(expected3, actual3);

        }


        public static void AssertListEquals<TE, TA>(Action<TE, TA> asserter, IEnumerable<TE> expected, IEnumerable<TA> actual)
        {
            IList<TA> actualList = actual.ToList();
            IList<TE> expectedList = expected.ToList();

            Assert.IsTrue(actualList.Count == expectedList.Count);

            for(var i = 0; i < expectedList.Count; i++)
            {
                try
                {
                    asserter.Invoke(expectedList[i], actualList[i]);
                }
                catch(Exception ex)
                {
                    Assert.IsTrue(false, $"Assertion failed because: {ex.Message}");
                }
            }
        }
        private void AssertMeetings(IEnumerable<Meeting> expectedMeetings, IEnumerable<Meeting> actualMeetings)
        {
            AssertListEquals(
                (e, a) => AssertMeeting(e, a),
                expectedMeetings,
                actualMeetings);
        }
        private void AssertMeeting(Meeting expected, Meeting actual)
        {
            Assert.AreEqual(expected.Name, actual.Name, "Name");
            Assert.AreEqual(expected.Description, actual.Description, "Description");
            Assert.AreEqual(expected.ResponsiblePerson, actual.ResponsiblePerson, "ResponsiblePerson");
            Assert.AreEqual(expected.Type, actual.Type, "Type");
            Assert.AreEqual(expected.Category, actual.Category, "Category");   
            Assert.AreEqual(expected.StartDate, actual.StartDate, "StartDate");
            Assert.AreEqual(expected.EndDate, actual.EndDate, "EndDate");
            Assert.AreEqual(expected.Attendees.Count, actual.Attendees.Count, "Attendees");
        }

    }
}