# Sprocket.Text.Ascii.Calendar.dll v1.0.0 API documentation

Created by [David Pullin](https://ict-man.me)

Readme Last Updated on 05.02.2021

The full API can be found at https://ict-man.me/sprocket/api/Sprocket.Text.Ascii.html

Licence GPL-3.0 

<br>

# Summary

Ascii Calendar generates a month to view calendar using plain text characters.

Options all you to: -

- Specify the start day of the week.
- Set alternative day of the week names.
- Specifiy the cell size to be used used.
- Turn off the row separator between weeks.


<br>


# Examples

Namespace: Sprocket.Text.Ascill



### Example with default field delimiters

<pre><code>
    var c = new Calendar();
    c.CellWidth = 8;
    c.CellHeight = 1;
    string s = c.Render(new DateTime(1980, 02, 23));

    |    Mon |    Tue |    Wed |    Thu |    Fri |    Sat |    Sun |
    |--------|--------|--------|--------|--------|--------|--------|
    |        |        |        |        |      1 |      2 |      3 |
    |--------|--------|--------|--------|--------|--------|--------|
    |      4 |      5 |      6 |      7 |      8 |      9 |     10 |
    |--------|--------|--------|--------|--------|--------|--------|
    |     11 |     12 |     13 |     14 |     15 |     16 |     17 |
    |--------|--------|--------|--------|--------|--------|--------|
    |     18 |     19 |     20 |     21 |     22 |     23 |     24 |
    |--------|--------|--------|--------|--------|--------|--------|
    |     25 |     26 |     27 |     28 |     29 |        |        |
    |--------|--------|--------|--------|--------|--------|--------|
</code></pre>

Using a longer CellWidth will display the full day's name.  Day names displayed will be dependant on your locale unless you use overwrite them.


---

The full API can be found at DOXX to do

---

<br>

# Calendar Class

Namespace: Sprocket.Text.Ascii

## Properties

| Name | Type | Default | Summary |
|---|---|---|---|
| **CellWidth** | int | 15 | Width, in characters, of each cell |
| **CellHeight** | int | 5 | Height, in characters, of each cell |
| **DayOfWeekNamesLong** | string[] | As per your locale | Holds the full names of the days of the week.  Change this to use alternative names. |
| **DayOfWeekNamesShort** | string[] | As per your locale | Holds the short names of days of the week.  Change this to use alternative names. |
| **RenderWeekRowSeparators** | bool | true | Enables the drawing of the line underneath each week.|
| **WeekStartsOnDay** | DayOfWeek | Monday | The start day of the week. |
