using System;
using Xunit;

using Sprocket.Text.Ascii;

#nullable enable

// suppress compuiler message of not using a constant when calling Assert.Equal
// this is due to loading of expected results from files rather that using string literals
#pragma warning disable xUnit2000               //xUnit2000 Constants and literals should be the expected argument

namespace CalendarTest
{
    public class UnitTest1
    {
        // part  relative to where the exe is built
        private const string ExpectectResultsFolderPath = "../../../";

        #region "Test Defaults and Week Day Names"
        [Fact]
        public void DefaultPropertyValues()
        {
            var c = CreateCalendarWithFixedDaysOfweek();
            string s = c.Render(new DateTime(2025, 01, 01));

            Assert.Equal(GetExpectedResults("DefaultPropertyValues.txt"), s);
        }

        [Fact]
        public void AlternativeDayNames()
        {
            var c = CreateCalendarWithAlternativeDaysOfweek();
            string s = c.Render(new DateTime(2025, 01, 01));

            Assert.Equal(GetExpectedResults("AlternativeDayNames.txt"), s);
        }
        #endregion

        #region "Test Change of Weekday names and Cell Sizes"

        [Fact]
        public void StartOfWeekIsSunday()
        {
            var c = CreateCalendarWithFixedDaysOfweek();
            c.WeekStartsOnDay = DayOfWeek.Sunday;
            string s = c.Render(new DateTime(2021, 07, 11));

            Assert.Equal(GetExpectedResults("StartOfWeekIsSunday.txt"), s);
        }

        [Fact]
        public void CellHeight1AndCellWidth8()
        {
            var c = CreateCalendarWithFixedDaysOfweek();
            c.CellWidth = 8;                //NB: Narrowing the cell width will cause the day name to be short version
            c.CellHeight = 1;
            string s = c.Render(new DateTime(1980, 02, 23));

            Assert.Equal(GetExpectedResults("CellHeight1AndCellWidth8.txt"), s);
        }

        [Fact]
        public void CellHeight2AndCellWidth3()
        {
            var c = CreateCalendarWithFixedDaysOfweek();
            c.CellWidth = 3;                //NB: Narrowing the cell widthfurther cause the day name to be the first character
            c.CellHeight = 2;
            string s = c.Render(new DateTime(1980, 02, 23));

            Assert.Equal(GetExpectedResults("CellHeight2AndCellWidth3.txt"), s);
        }

        #endregion

        #region "Test Borders"

        [Fact]
        private void DisableWeekRowSeparators()
        {
            var c = CreateCalendarWithFixedDaysOfweek();
            c.RenderWeekRowSeparators = false;
            c.CellWidth = 5;
            c.CellHeight = 1;
            string s = c.Render(new DateTime(2021,11,01));

            Assert.Equal(GetExpectedResults("DisableWeekRowSeparators.txt"), s);

        }


        #endregion

        #region "Supporting Methods"

        /// <summary>
        /// Read contents from the ExpectedContents directory
        /// </summary>
        /// <param name="filename">Name of file</param>
        /// <returns>File's contents</returns>
        private string GetExpectedResults(string filename)
        {
            return System.IO.File.ReadAllText(ExpectectResultsFolderPath + $"ExpectedResults/{filename}");
        }




        /// <summary>
        /// Creates an instance of the calendar object with non-localized days of the weeks.
        /// </summary>
        /// <remarks>
        /// Calendar defaults to using localized week day names.  As such, these tests may fail depending
        /// on the local settings of the system being used. To overcome this, we are explicitly setting
        /// days names to the values we expect in our test results.
        /// </remarks>
        /// <returns></returns>
        private Calendar CreateCalendarWithFixedDaysOfweek()
        {
            Calendar c = new();

            // Calendar defaults to using localized day names 
            // For testing purposes names are being fixed here to ensure that tests pass
            // regardless of localization

            c.DayOfWeekNamesShort[(int)DayOfWeek.Monday] = "Mon";
            c.DayOfWeekNamesShort[(int)DayOfWeek.Tuesday] = "Tue";
            c.DayOfWeekNamesShort[(int)DayOfWeek.Wednesday] = "Wed";
            c.DayOfWeekNamesShort[(int)DayOfWeek.Thursday] = "Thu";
            c.DayOfWeekNamesShort[(int)DayOfWeek.Friday] = "Fri";
            c.DayOfWeekNamesShort[(int)DayOfWeek.Saturday] = "Sat";
            c.DayOfWeekNamesShort[(int)DayOfWeek.Sunday] = "Sun";

            c.DayOfWeekNamesLong[(int)DayOfWeek.Monday] = "Monday";
            c.DayOfWeekNamesLong[(int)DayOfWeek.Tuesday] = "Tuesday";
            c.DayOfWeekNamesLong[(int)DayOfWeek.Wednesday] = "Wednesday";
            c.DayOfWeekNamesLong[(int)DayOfWeek.Thursday] = "Thursday";
            c.DayOfWeekNamesLong[(int)DayOfWeek.Friday] = "Friday";
            c.DayOfWeekNamesLong[(int)DayOfWeek.Saturday] = "Saturday";
            c.DayOfWeekNamesLong[(int)DayOfWeek.Sunday] = "Sunday";

            return c;
        }


        /// <summary>
        /// Create a calendar object with alternative days of the week
        /// </summary>
        /// <returns>Instance of calendar object using alternative days of the week</returns>
        private Calendar CreateCalendarWithAlternativeDaysOfweek()
        {
            Calendar c = new();

            // Calendar defaults to using localized day names 
            // For testing purposes names are being fixed here to ensure that tests pass
            // regardless of localization

            c.DayOfWeekNamesShort[(int)DayOfWeek.Monday] = "Moo";
            c.DayOfWeekNamesShort[(int)DayOfWeek.Tuesday] = "Tre";
            c.DayOfWeekNamesShort[(int)DayOfWeek.Wednesday] = "Hev";
            c.DayOfWeekNamesShort[(int)DayOfWeek.Thursday] = "Mer";
            c.DayOfWeekNamesShort[(int)DayOfWeek.Friday] = "Val";
            c.DayOfWeekNamesShort[(int)DayOfWeek.Saturday] = "Ste";
            c.DayOfWeekNamesShort[(int)DayOfWeek.Sunday] = "Sun";

            c.DayOfWeekNamesLong[(int)DayOfWeek.Monday] = "Moon";
            c.DayOfWeekNamesLong[(int)DayOfWeek.Tuesday] = "Trewsday";
            c.DayOfWeekNamesLong[(int)DayOfWeek.Wednesday] = "Hevensday";
            c.DayOfWeekNamesLong[(int)DayOfWeek.Thursday] = "Mersday";
            c.DayOfWeekNamesLong[(int)DayOfWeek.Friday] = "Valar";
            c.DayOfWeekNamesLong[(int)DayOfWeek.Saturday] = "Sterday";
            c.DayOfWeekNamesLong[(int)DayOfWeek.Sunday] = "Sunday";

            return c;

        }

        #endregion
    }
}
