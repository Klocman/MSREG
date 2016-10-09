using System.Windows.Forms;
using Klocman.Extensions;
using MSREG.Viewer.SpecialClasses;

namespace MSREG.Viewer.CustomControls
{
    public partial class MeasurementDisplay : UserControl
    {
        public MeasurementDisplay()
        {
            InitializeComponent();
        }

        public void UpdateDisplays(Msr33Measurement measurement)
        {
            labelT.SafeInvoke(() =>
            {
                labelT.Text = string.Format("{0:0.0}\n°C{1}", measurement.Temperature,
                    GetRegulationString(measurement.TemperatureRegulationResult));
                labelH.Text = string.Format("{0:0.0}\nRH%{1}", measurement.Humidity,
                    GetRegulationString(measurement.HumidityRegulationResult));
            });
        }

        private static string GetRegulationString(RegulationResult result)
        {
            string t;
            switch (result)
            {
                case RegulationResult.AboveSettingAlarm:
                    t = "↑!";
                    break;
                case RegulationResult.AboveSetting:
                    t = "↑";
                    break;

                case RegulationResult.BelowSettingAlarm:
                    t = "↓!";
                    break;
                case RegulationResult.BelowSetting:
                    t = "↓";
                    break;

                default:
                    t = string.Empty;
                    break;
            }
            return t;
        }
    }
}