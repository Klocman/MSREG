using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Klocman.Extensions;
using MSREG.Viewer.CustomControls;
using MSREG.Viewer.Properties;
using MSREG.Viewer.SpecialClasses;
using ZedGraph;
using Timer = System.Timers.Timer;

namespace MSREG.Viewer.Windows.MdiChildWindows.MSR33
{
    public sealed partial class Msr33Window : MdiChildForm
    {
        #region Constructors

        public Msr33Window()
        {
            InitializeComponent();

            Text = Strings.Default.TitleMsr33Panel;

            SetupTemperatureGraph(zedGraphTemperature.GraphPane);
            SetupHumidityGraph(zedGraphHumidity.GraphPane);

            _regulator = new MsregDevice();

            _regulator.Connected += regulator_Connected;
            _regulator.ReceivedAlarmValue += regulator_ReceivedAlarmValue;
            _regulator.ReceivedTemperatureSettings += regulator_ReceivedTemperatureSettings;
            _regulator.ReceivedHumiditySettings += regulator_ReceivedHuiditySettings;
            _regulator.ReceivedMeasurement += regulator_ReceivedMeasurement;
            _regulator.Disconnected += regulator_Disconnected;

            deviceControls1.TargetDevice = _regulator;

            RefreshGraphs();
        }

        #endregion Constructors

        private void deviceControls1_GraphPlottingSpeedChanged(decimal obj)
        {
            SetupTimer(obj);
        }

        #region Fields

        // 30 seconds
        //TimeSpan autoscrollRange = new TimeSpan(0,2,0);
        private CurveItem _curveTemp;
        private CurveItem _curveHum;
        private CurveItem _curveTempSetting;
        private CurveItem _curveHumSetting;
        private RegulationSettings _lastHumiditySettings;
        private RegulationSettings _lastTemperatureSettings;
        private readonly MsregDevice _regulator;

        #endregion Fields

        #region Methods

        public bool Connect(string portName)
        {
            if (_regulator.Connect(portName))
            {
                this.SafeInvoke(() =>
                {
                    infoLabelDate.Text = _regulator.ConnectedDeviceInfo.FirmwareBuildTime.ToString(CultureInfo.InvariantCulture);
                    infoLabelModel.Text = _regulator.ConnectedDeviceInfo.Type.ToString();
                    //infoLabelPort.Text = regulator.TargetPort;
                    infoLabelRev.Text = _regulator.ConnectedDeviceInfo.HardwareRevision;
                    //infoLabelStatus.Text = Strings.Default.Connected;
                    infoLabelVer.Text = _regulator.ConnectedDeviceInfo.FirmwareVersion;
                });
                return true;
            }
            return false;
        }

        private void MSR33Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            _regulator.Disconnect();
        }

        private void radioXmanual_CheckedChanged(object sender, EventArgs e)
        {
            if (radioXmanual.Checked)
            {
                zedGraphTemperature.GraphPane.XAxis.Scale.MaxAuto = false;
                zedGraphTemperature.GraphPane.XAxis.Scale.MinAuto = false;
                zedGraphHumidity.GraphPane.XAxis.Scale.MaxAuto = false;
                zedGraphHumidity.GraphPane.XAxis.Scale.MinAuto = false;
            }
        }

        private void radioYmanual_CheckedChanged(object sender, EventArgs e)
        {
            if (radioYmanual.Checked)
            {
                zedGraphTemperature.GraphPane.YAxis.Scale.MaxAuto = false;
                zedGraphTemperature.GraphPane.YAxis.Scale.MinAuto = false;
                zedGraphHumidity.GraphPane.YAxis.Scale.MaxAuto = false;
                zedGraphHumidity.GraphPane.YAxis.Scale.MinAuto = false;
            }
        }

        private void RefreshGraphs()
        {
            zedGraphTemperature.AxisChange();
            zedGraphHumidity.AxisChange();
            zedGraphTemperature.Invalidate();
            zedGraphHumidity.Invalidate();
        }

        private void regulator_Connected(MsregDevice obj)
        {
            this.SafeInvoke(() =>
            {
                //infoLabelStatus.Text = Strings.Default.Connected;
                Text = string.Format("{0} : {1}", Strings.Default.TitleMsr33Panel, obj.TargetPort);
            });
        }

        private void regulator_Disconnected(MsregDevice arg1, string arg2)
        {
            this.SafeInvoke(() =>
            {
                //infoLabelStatus.Text = Strings.Default.Disconnected;
                Text = Strings.Default.TitleMsr33Panel;
            });
        }

        private void regulator_ReceivedAlarmValue(MsregDevice arg1, MsregAlarm arg2)
        {
            //MessageBox.Show(arg2.ToString(), "Blad sterownika");
        }

        private void regulator_ReceivedHuiditySettings(MsregDevice arg1, RegulationSettings arg2)
        {
            _lastHumiditySettings = arg2;
        }

        private Msr33Measurement _lastMeasurement;

        private Timer _graphUpdateTimer;

        public void SetupTimer(decimal timespan)
        {
            if (timespan <= 0)
            {
                if (_graphUpdateTimer != null)
                {
                    _graphUpdateTimer.Dispose();
                    _graphUpdateTimer = null;
                }
            }
            else
            {
                _graphUpdateTimer = new Timer {AutoReset = true};
                _graphUpdateTimer.Elapsed += AddGraphPoint;
                _graphUpdateTimer.Interval = (double) (timespan*1000);
                _graphUpdateTimer.Start();
            }
        }

        private void AddGraphPoint(object sender, EventArgs e)
        {
            if (_lastHumiditySettings == null || _lastTemperatureSettings == null)
            {
                _regulator.SendCommandAsync(MsregInputCommand.HumiditySettings);
                _regulator.SendCommandAsync(MsregInputCommand.TemperatureSettings);
                return;
            }

            var now = DateTime.Now;
            var date = new XDate(now);

            _curveTemp.AddPoint(date, _lastMeasurement.Temperature);
            _curveTempSetting.AddPoint(date, _lastTemperatureSettings.TargetValue);

            _curveHum.AddPoint(date, _lastMeasurement.Humidity);
            _curveHumSetting.AddPoint(date, _lastHumiditySettings.TargetValue);

            UpdateGraphLimits(new XDate(now.Subtract(new TimeSpan(0, (int) xLastMinuteCount.Value, 0))), date);

            this.SafeInvoke(RefreshGraphs);
        }

        private void regulator_ReceivedMeasurement(MsregDevice arg1, Msr33Measurement arg2)
        {
            _lastMeasurement = arg2;
            measurementDisplay1.UpdateDisplays(arg2);

            if (_graphUpdateTimer == null)
                AddGraphPoint(null, null);
        }

        private void regulator_ReceivedTemperatureSettings(MsregDevice arg1, RegulationSettings arg2)
        {
            _lastTemperatureSettings = arg2;
        }

        private void SetupHumidityGraph(GraphPane targetPane)
        {
            targetPane.Title.Text = Strings.Default.GraphHumidityTitle;

            targetPane.XAxis.Title.Text = Strings.Default.GraphTimeX;
            targetPane.XAxis.Type = AxisType.Date;

            targetPane.YAxis.Title.Text = Strings.Default.GraphHumidityY;
            //targetPane.YAxis.Type = AxisType.Linear;

            var list = new RollingPointPairList(Settings.Default.GraphMaxDataPoints, true);
            _curveHum = targetPane.AddCurve(Strings.Default.GraphCurveMeasurement,
                list, Color.MediumSeaGreen, SymbolType.None);

            list = new RollingPointPairList(Settings.Default.GraphMaxDataPoints, true);
            _curveHumSetting = targetPane.AddCurve(Strings.Default.GraphCurveSetting,
                list, Color.Gray, SymbolType.None);
        }

        private void SetupTemperatureGraph(GraphPane targetPane)
        {
            targetPane.Title.Text = Strings.Default.GraphTemperatureTitle;

            targetPane.XAxis.Title.Text = Strings.Default.GraphTimeX;
            targetPane.XAxis.Type = AxisType.Date;

            targetPane.YAxis.Title.Text = Strings.Default.GraphTemperatureY;
            //targetPane.YAxis.Type = AxisType.Linear;

            var list = new RollingPointPairList(Settings.Default.GraphMaxDataPoints, true);
            _curveTemp = targetPane.AddCurve(Strings.Default.GraphCurveMeasurement,
                list, Color.Red, SymbolType.None);

            list = new RollingPointPairList(Settings.Default.GraphMaxDataPoints, true);
            _curveTempSetting = targetPane.AddCurve(Strings.Default.GraphCurveSetting,
                list, Color.Gray, SymbolType.None);
        }

        private XDate UpdateGraphLimits(XDate minDate, XDate maxDate)
        {
            if (radioXAuto.Checked)
            {
                zedGraphTemperature.GraphPane.XAxis.Scale.MaxAuto = true;
                zedGraphTemperature.GraphPane.XAxis.Scale.MinAuto = true;
                zedGraphHumidity.GraphPane.XAxis.Scale.MaxAuto = true;
                zedGraphHumidity.GraphPane.XAxis.Scale.MinAuto = true;
            }
            else if (radioXlastmins.Checked)
            {
                zedGraphTemperature.GraphPane.XAxis.Scale.MaxAuto = false;
                zedGraphTemperature.GraphPane.XAxis.Scale.MinAuto = false;
                zedGraphHumidity.GraphPane.XAxis.Scale.MaxAuto = false;
                zedGraphHumidity.GraphPane.XAxis.Scale.MinAuto = false;

                // Last x minutes
                zedGraphTemperature.GraphPane.XAxis.Scale.Max = maxDate;
                zedGraphTemperature.GraphPane.XAxis.Scale.Min = minDate;
                zedGraphHumidity.GraphPane.XAxis.Scale.Max = maxDate;
                zedGraphHumidity.GraphPane.XAxis.Scale.Min = minDate;
            }

            if (radioYauto.Checked)
            {
                zedGraphTemperature.GraphPane.YAxis.Scale.MaxAuto = true;
                zedGraphTemperature.GraphPane.YAxis.Scale.MinAuto = true;
                zedGraphHumidity.GraphPane.YAxis.Scale.MaxAuto = true;
                zedGraphHumidity.GraphPane.YAxis.Scale.MinAuto = true;
            }
            else if (radioYlimits.Checked)
            {
                zedGraphTemperature.GraphPane.YAxis.Scale.MaxAuto = false;
                zedGraphTemperature.GraphPane.YAxis.Scale.MinAuto = false;
                zedGraphHumidity.GraphPane.YAxis.Scale.MaxAuto = false;
                zedGraphHumidity.GraphPane.YAxis.Scale.MinAuto = false;

                zedGraphTemperature.GraphPane.YAxis.Scale.Max = 125;
                zedGraphTemperature.GraphPane.YAxis.Scale.Min = -40;
                zedGraphHumidity.GraphPane.YAxis.Scale.Max = 100;
                zedGraphHumidity.GraphPane.YAxis.Scale.Min = 0;
            }
            return maxDate;
        }

        private void zedGraphControl_ZoomEvent(ZedGraphControl sender, ZoomState oldState, ZoomState newState)
        {
            radioXmanual.Checked = true;
            radioYmanual.Checked = true;
        }

        #endregion Methods
    }
}