using System;
using System.Windows;
using System.Windows.Controls;
using Dyana.Core;

namespace Dyana.View {
    public partial class DyanaWindow : UserControl {
        public DyanaWindow() {
            InitializeComponent();
        }

        void BtnCalculate_Click(Object sender, RoutedEventArgs e) {
            try {
                DyanaWorkHour wh = D.CalculateWorkHours(TxtArrive.Text, TxtDepart.Text, TxtWorkHour.Text, TxtBreak.Text);
                String message = D.GetAppropriateMessage(wh);

                TxtTotal.Text = wh.TotalHours.ToString("0.00");
                TxtExp.Text = message;
            }
            catch (Exception ex) {
                TxtExp.Text = ex.Message;
            }
        }

        void BtnReset_Click(Object sender, RoutedEventArgs e) {
            TxtArrive.Text = "09:00";
            TxtDepart.Text = "18:00";
            TxtWorkHour.Text = "8";
            TxtBreak.Text = "1";
            TxtTotal.Text = "0";
            TxtExp.Text = String.Empty;
        }
    }
}
