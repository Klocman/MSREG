using System;
using System.IO;
using System.Reflection;
using MSREG.Viewer.CustomControls;

namespace MSREG.Viewer.Windows.MdiChildWindows
{
    internal sealed partial class AboutBox : MdiChildForm
    {
        #region Fields

        private readonly Assembly _currentExecutingAssembly = Assembly.GetExecutingAssembly();

        #endregion Fields

        #region Constructors

        public AboutBox()
        {
            InitializeComponent();
            Text = string.Format("O programie {0}", AssemblyTitle);
            labelProductName.Text += AssemblyProduct;
            labelVersion.Text += AssemblyVersion;
            labelCopyright.Text += AssemblyCopyright;
            labelCompanyName.Text += AssemblyCompany;
            textBoxDescription.Text = AssemblyDescription;
            labelCpu.Text += _currentExecutingAssembly.GetName().ProcessorArchitecture.ToString();
            okButton.Focus();
        }

        #endregion Constructors

        private void AboutBox_Shown(object sender, EventArgs e)
        {
            okButton.Focus();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region Properties

        public string AssemblyCompany
        {
            get
            {
                var attributes = _currentExecutingAssembly.GetCustomAttributes(typeof (AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return string.Empty;
                }
                return ((AssemblyCompanyAttribute) attributes[0]).Company;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                var attributes = _currentExecutingAssembly.GetCustomAttributes(typeof (AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return string.Empty;
                }
                return ((AssemblyCopyrightAttribute) attributes[0]).Copyright;
            }
        }

        public string AssemblyDescription
        {
            get
            {
                var attributes = _currentExecutingAssembly.GetCustomAttributes(typeof (AssemblyDescriptionAttribute),
                    false);
                if (attributes.Length == 0)
                {
                    return string.Empty;
                }
                return ((AssemblyDescriptionAttribute) attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                var attributes = _currentExecutingAssembly.GetCustomAttributes(typeof (AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return string.Empty;
                }
                return ((AssemblyProductAttribute) attributes[0]).Product;
            }
        }

        public string AssemblyTitle
        {
            get
            {
                var attributes = _currentExecutingAssembly.GetCustomAttributes(typeof (AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    var titleAttribute = (AssemblyTitleAttribute) attributes[0];
                    if (titleAttribute.Title.Length == 0)
                    {
                        return titleAttribute.Title;
                    }
                }
                return Path.GetFileNameWithoutExtension(_currentExecutingAssembly.CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get { return _currentExecutingAssembly.GetName().Version.ToString(); }
        }

        #endregion Properties
    }
}