using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace Dyana
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var digitRgx = new Regex(@"\d+", RegexOptions.Compiled);

                if (String.IsNullOrEmpty(TxtArrive.Text) || !TxtArrive.Text.Contains(":"))
                    throw new InvalidOperationException("Arrive hours is not valid.");
                String[] arrive = TxtArrive.Text.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);
                if (arrive.Length < 2)
                    throw new InvalidOperationException("Arrive hours is not valid.");
                if (!digitRgx.IsMatch(arrive[0]) || !digitRgx.IsMatch(arrive[1]))
                    throw new InvalidOperationException("Arrive hours is not valid.");
                Int32 arrhour = Convert.ToInt32(arrive[0]);
                Int32 arrmin = Convert.ToInt32(arrive[1]);
                if (arrhour < 0 || arrhour > 23)
                    throw new InvalidOperationException("Arrive hours is not valid.");
                if (arrmin < 0 || arrmin > 59)
                    throw new InvalidOperationException("Arrive minutes is not valid.");

                if (String.IsNullOrEmpty(TxtDepart.Text) || !TxtDepart.Text.Contains(":"))
                    throw new InvalidOperationException("Depart hours is not valid.");
                String[] depart = TxtDepart.Text.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);
                if (depart.Length < 2)
                    throw new InvalidOperationException("Depart hours is not valid.");
                if (!digitRgx.IsMatch(depart[0]) || !digitRgx.IsMatch(depart[1]))
                    throw new InvalidOperationException("Depart hours is not valid.");
                Int32 dephour = Convert.ToInt32(depart[0]);
                Int32 depmin = Convert.ToInt32(depart[1]);
                if (dephour < 0 || dephour > 23)
                    throw new InvalidOperationException("Depart hours is not valid.");
                if (depmin < 0 || depmin > 59)
                    throw new InvalidOperationException("Depart minutes is not valid.");

                if (String.IsNullOrEmpty(TxtWorkHour.Text) || !digitRgx.IsMatch(TxtWorkHour.Text))
                    throw new InvalidOperationException("Work hour is not valid.");
                if (String.IsNullOrEmpty(TxtBreak.Text) || !digitRgx.IsMatch(TxtBreak.Text))
                    throw new InvalidOperationException("Break is not valid.");

                Int32 workhour = Convert.ToInt32(TxtWorkHour.Text);
                Int32 breakhour = Convert.ToInt32(TxtBreak.Text);
                Int32 responsibility = workhour + breakhour;

                var now = DateTime.Now;
                var start = new DateTime(now.Year, now.Month, now.Day, arrhour, arrmin, 00);
                var end = new DateTime(now.Year, now.Month, now.Day, dephour, depmin, 00);
                Double hours = end.Subtract(start).TotalHours;
                TxtTotal.Text = hours.ToString();
                TxtExp.Text = hours == responsibility ?
                    "It's time to go home :)" : hours < responsibility ?
                        "Oy! Work more!" : hours > responsibility && hours > responsibility +1 ?
                            "Overtimeeee... Waaaaaaattttt..." : "Just go home already!";
            }
            catch (Exception ex)
            {
                TxtExp.Text = ex.Message;
            }
        }

        void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            TxtArrive.Text = "09:00";
            TxtDepart.Text = "18:00";
            TxtWorkHour.Text = "8";
            TxtBreak.Text = "1";
            TxtTotal.Text = "0";
            TxtExp.Text = String.Empty;
        }
    }
}
