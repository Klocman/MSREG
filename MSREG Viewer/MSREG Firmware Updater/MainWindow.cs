using System;
using System.ComponentModel;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Klocman.Extensions;

namespace MSREG.FirmwareUpdater
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool AutomaticallyRestart
        {
            get { return checkBox1.Checked; }
        }

        #region Methods

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void OpenConnection(object sender, EventArgs e)
        {
            //MessageBox.Show("Jeżeli aktualizacja kończy się niepowodzeniem podczas czekania na restart regulatora, spróbuj ponownie.", "Wskazówka użytkowania", MessageBoxButtons.OK, MessageBoxIcon.Information);

            try
            {
                serialPort1.BaudRate = 4800;
                serialPort1.PortName = comboBox1.Text;
                serialPort1.Handshake = Handshake.None;
                serialPort1.Open();

                groupBoxSettings.Enabled = false;
                groupBoxActions.Enabled = false;
                progressBar1.Value = 0;

                Task.Run(new Action(UploadNewFirmware));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd przy otwieraniu portu " + comboBox1.Text + ":\n\n" + ex.Message,
                    "Błąd przy otwieraniu portu", MessageBoxButtons.OK, MessageBoxIcon.Error);

                statusLabel.Text = "Błąd przy otwieraniu portu";
                CloseConnection();
            }
        }

        private void CloseConnection()
        {
            serialPort1.Close();
            this.SafeInvoke(() =>
            {
                groupBoxSettings.Enabled = true;
                groupBoxActions.Enabled = true;
            });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshPortList(sender, e);
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                if (MessageBox.Show(
                    "Czy na pewno zamknąć program? Przerwanie aktualizacji może pozostawić regulator bez użytecznego firmware.",
                    "UWAGA, aktualizacja w toku", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) ==
                    DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textBox1.Text = openFileDialog1.FileName;
        }

        private void RefreshPortList(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(SerialPort.GetPortNames());

            if (comboBox1.Items.Count > 0)
            {
                if (Program.CommandLineArgs.Any())
                {
                    comboBox1.SelectedItem = comboBox1.Items.Cast<string>()
                        .FirstOrDefault(str => str.Equals(Program.CommandLineArgs
                            .FirstOrDefault(x => x.StartsWith("com", StringComparison.OrdinalIgnoreCase)),
                            StringComparison.OrdinalIgnoreCase)) ?? comboBox1.Items[0];
                }
                else
                {
                    comboBox1.SelectedItem = comboBox1.Items[0];
                }
            }
        }

        private void SetStatusText(string status)
        {
            statusLabel.Invoke(new Action(() => statusLabel.Text = status));
        }

        private async void UploadNewFirmware()
        {
            byte[] fileContents;

            try
            {
                fileContents = File.ReadAllBytes(textBox1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd otwarcia pliku z firmware", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatusText("Problem z plikiem .hex");
                CloseConnection();
                return;
            }

            if (fileContents.Length < 10000 || fileContents.Length > 20174
                || !Encoding.ASCII.GetString(fileContents, 0, 9).Equals(":10000000")
                || !Encoding.ASCII.GetString(fileContents, fileContents.Length - 20, 19).Contains(":00000001FF"))
            {
                if (
                    MessageBox.Show(
                        "Podany plik firmware wygląda na nieprawidłowy, upewnij się że nie jest on uszkodzony.\nCzy mimo to wysłać go do regulatora?",
                        "Błąd otwarcia pliku z firmware", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) ==
                    DialogResult.Cancel)
                {
                    SetStatusText("Problem z plikiem .hex");
                    CloseConnection();
                    return;
                }
            }

            if (AutomaticallyRestart)
            {
                SetStatusText("Łączenie z regulatorem");

                if (await TryConnectingToDevice())
                {
                    serialPort1.Write(new[] {'!'}, 0, 1);
                }
                else
                {
                    SetStatusText("Regulator nie odpowiedział na komendę restartu");
                    CloseConnection();
                    return;
                }
            }

            SetStatusText("Oczekiwanie na restart regulatora");

            if (!await TryCallingBootloader())
            {
                SetStatusText("Regulator nie odpowiedział po restarcie, spróbuj ponownie");

                CloseConnection();
                return;
            }

            SetStatusText("Wysyłanie firmware do regulatora");

            progressBar1.Invoke(new Action(() => { progressBar1.Maximum = fileContents.Length; }));

            var writeTask = serialPort1.BaseStream.WriteAsync(fileContents, 0, fileContents.Length);

            while (serialPort1.BytesToWrite > 0)
            {
                progressBar1.Invoke(
                    new Action(() => progressBar1.Value = progressBar1.Maximum - serialPort1.BytesToWrite));
            }

            //await writeTask;

            SetStatusText("Próba połączenia z regulatorem");

            await Task.Delay(400);

            if (await TryConnectingToDevice())
            {
                SetStatusText("Aktualizacja zakończona powodzeniem!");
            }
            else
            {
                SetStatusText("Brak odpowiedzi, możliwe niepowodzenie.");
            }

            CloseConnection();
        }

        private async Task<bool> TryCallingBootloader()
        {
            serialPort1.DiscardInBuffer();
            serialPort1.DiscardOutBuffer();

            var successful = false;
            // Wait a bit longer if user has to manually reset the device
            var timer = Task.Delay(AutomaticallyRestart ? new TimeSpan(0, 0, 4) : new TimeSpan(0, 0, 7));
            var spamStuff = new[] {' '};

            while (!successful && !timer.IsCompleted)
            {
                serialPort1.Write(spamStuff, 0, spamStuff.Length);

                await Task.Delay(4);

                if (serialPort1.ReadExisting().ContainsAny(new[] {'M', 'S', 'R'}))
                    successful = true;
            }

            serialPort1.DiscardInBuffer();
            serialPort1.DiscardOutBuffer();
            return successful;
        }

        private async Task<bool> TryConnectingToDevice()
        {
            for (var i = 0; i < 4; i++)
            {
                serialPort1.Write(new[] {' '}, 0, 1);
                await Task.Delay(20);
                while (serialPort1.BytesToRead > 0)
                {
                    if (serialPort1.ReadChar() == 'b')
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void ValidateSettings(object sender, EventArgs e)
        {
            groupBoxActions.Enabled = File.Exists(textBox1.Text) &&
                                      !string.IsNullOrWhiteSpace(comboBox1.SelectedItem as string);
        }

        #endregion Methods
    }
}