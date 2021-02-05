using System;
using System.Text;

#nullable enable

namespace Sprocket.Text.Ascii
{
    public class Calendar
    {
        /// <summary>
        /// Private variable for the <see cref="CellWidth"/>> property.
        /// </summary>
        private int _cellWidth = 15;

        /// <summary>
        /// Private variable for the <see cref="CellHeight"/> property.
        /// </summary>
        private int _cellHeight = 5;


        /// <summary>
        /// Controls the rendering of a separator line at the bottom of each week row.
        /// Default = true.
        /// </summary>
        /// <value>Determines if week row separators are to be rendered below each week?</value>
        public bool RenderWeekRowSeparators { get; set; } = true;

        /// <summary>
        /// Minimum width of a cell.  Value = 3;
        /// </summary>
        /// <value>Minimum cell width.</value>
        public static readonly int MinimumCellWidth = 3;


        /// <summary>
        /// Determines which day a week starts on.  Default = DayOfWeek.Monday
        /// This becomes the left most column when rendering the calendar.
        /// </summary>
        /// <value><see cref="DayOfWeek"/> value from 0..6 (Sunday..Saturday)</value>
        public DayOfWeek WeekStartsOnDay { get; set; } = DayOfWeek.Monday;

        /// <summary>
        /// Array 0..6 (Sunday..Saturday) holding the long names of the days of the week.
        /// Values default to the locaized names corresponding to <see cref="DateTime.ToString"/> format "dddd"
        /// </summary>
        /// <value>Array holding the long names of the days of the week.</value>
        public string[] DayOfWeekNamesLong { get; init; }

        /// <summary>
        /// Array 0..6 (Sun..Sat) holding the short names of the days of the week.
        /// Values default to the locaized names corresponding to <see cref="DateTime.ToString"/> format "ddd"
        /// </summary>
        /// <value>Array holding the short names of the days of the week.</value>
        public string[] DayOfWeekNamesShort { get; init; }

        /// <summary>
        /// Number of characters for each cell. This is the inside of the cell excluding borders.
        /// Default = 15.
        /// </summary>
        /// <remarks>
        /// Value must not be less than <see cref="MinimumCellWidth"/>.
        /// </remarks>
        /// <exception cref="Exception">Thrown if attempting to set value to less-than <see cref="MinimumCellWidth"/></exception>
        /// <value>Number of characters for the cell's width.</value>
        public int CellWidth
        {
            get { return _cellWidth; }
            set
            {
                if (value >= MinimumCellWidth)
                {
                    _cellWidth = value;
                }
                else
                {
                    throw new Exception($"Cell widths cannot be less than {MinimumCellWidth}");
                }
            }
        }

        /// <summary>
        /// Number of rows for each cell.  
        /// This is the inside of the cell excluding borders and includes the row displaying the day number.
        /// Default = 5.
        /// </summary>
        /// <exception cref="Exception">Thrown if attempting to set value to less-than 1.</exception>
        /// <value>Number of rows for each cell.</value>
        public int CellHeight
        {
            get { return _cellHeight; }
            set
            {
                if (value > 0)
                {
                    _cellHeight = value;
                }
                else
                {
                    throw new Exception("You cannot assign a zero or negative value for the cell height.");
                }
            }
        }

        /// <summary>
        /// Calendar object containing settings and methods (see <see cref="Render"/>) to create an ASCII calendar.
        /// </summary>
        public Calendar()
        {
            DayOfWeekNamesLong = new string[7];
            DayOfWeekNamesShort = new string[7];

            // Build localised day of week names
            DateTime dt = DateTime.Now;
            for (int dayCount = 0; dayCount < 7; dayCount++)
            {
                DayOfWeekNamesLong[(int)dt.DayOfWeek] = dt.ToString("dddd");
                DayOfWeekNamesShort[(int)dt.DayOfWeek] = dt.ToString("ddd");
                dt = dt.AddDays(1);
            }
        }


        public string Render()
        {
            return Render(DateTime.Now);
        }

        public string Render(DateTime forDate)
        {
            int year = forDate.Year;
            int month = forDate.Month;

            int weekStartDay = (int)WeekStartsOnDay;

            StringBuilder output = new(4096);

            // get the day the 1st of the month falls on
            DateTime theFirstOfTheMonthDateTime = new DateTime(year, month, 1);
            int firstDayOfWeekInMonth = (int)theFirstOfTheMonthDateTime.DayOfWeek;         // 0..6 Sun..Sat
            int lastDayInMonth = DateTime.DaysInMonth(year, month);

            // set dayCounter to be a negative number of empty cells
            // to be rendered before the first day of the start of the month. 
            // Will 0 if the 1st falls on weekStartDay
            int dayCounter = 1;
            if (weekStartDay > firstDayOfWeekInMonth)
            {
                dayCounter = -7 + weekStartDay;
            }
            else if (weekStartDay < firstDayOfWeekInMonth)
            {
                dayCounter = 1 - (firstDayOfWeekInMonth - weekStartDay);
            }




            // Build Localised Name to display for the days of the weeks
            string dayNameFormat = (CellWidth > 9) ? "dddd" : "ddd";
            string[] dayNames = new string[7];
            for (int dayCount = 0; dayCount < 7; dayCount++)
            {
                if (CellWidth <= 3)
                {
                    // for narrow columns just display first character of day of week
                    dayNames[dayCount] = DayOfWeekNamesShort[dayCount].Substring(0, 1);

                }
                else if (CellWidth < 10)
                {

                    dayNames[dayCount] = DayOfWeekNamesShort[dayCount];
                }
                else
                {
                    dayNames[dayCount] = DayOfWeekNamesLong[dayCount];
                }
            }

            // Build Title Row with day of week names
            output.Append('|');
            for (int dayCount = 0, dayOfWeek = weekStartDay; dayCount < 7; dayCount++)
            {
                output.Append(string.Format($"{{0,{CellWidth - 1}}} |", dayNames[dayOfWeek]));

                dayOfWeek++;
                if (dayOfWeek == 7)
                {
                    dayOfWeek = 0;
                }
            }
            output.AppendLine("");

            // Row Separator under headers
            if (RenderWeekRowSeparators)
            {
                output.Append('|');

                for (int cellCol = 0; cellCol < 7; cellCol++)
                {
                    output.Append('-', CellWidth); ;
                    output.Append('|');

                }
                output.AppendLine("");
            }


            // Build Calendar Body
            int totalBlockCount = 0;
            for (; dayCounter <= lastDayInMonth; totalBlockCount += 7, dayCounter += 7)
            {
                for (int cellRow = 0; cellRow < CellHeight; cellRow++)
                {
                    output.Append('|');

                    for (int cellCol = 0; cellCol < 7; cellCol++)
                    {
                        int tmp = dayCounter + cellCol;
                        int trueDayOfMonthNum = (tmp < 0 || tmp > lastDayInMonth) ? 0 : tmp;

                        if (cellRow == 0 && trueDayOfMonthNum > 0)
                        {
                            output.Append(string.Format($"{{0,{CellWidth - 1}}} |", trueDayOfMonthNum));

                        }
                        else
                        {
                            output.Append(' ', CellWidth); ;
                            output.Append('|');
                        }


                    }

                    output.AppendLine("");
                }

                if (RenderWeekRowSeparators)
                {
                    output.Append('|');

                    for (int cellCol = 0; cellCol < 7; cellCol++)
                    {
                        output.Append('-', CellWidth); ;
                        output.Append('|');

                    }
                    output.AppendLine("");
                }

            }


            return output.ToString();
        }

    }
}

