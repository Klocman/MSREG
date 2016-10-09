using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;

namespace Klocman.Subsystems
{
    public sealed class FontGrabber
    {
        #region Fields

        private readonly Dictionary<string, FontFamily> _validFontFamilies = new Dictionary<string, FontFamily>();

        #endregion Fields

        #region Constructors

        public FontGrabber()
        {
            UpdateFontFamilies();
        }

        #endregion Constructors

        #region Properties

        public IEnumerable<FontFamily> ValidFontFamilies
        {
            get { return _validFontFamilies.Values; }
        }

        public IEnumerable<string> ValidFontFamilyNames
        {
            get { return _validFontFamilies.Keys; }
        }

        #endregion Properties

        #region Methods

        public Font GetFont(string familyName, float size, FontStyle style)
        {
            if (size <= 0)
                throw new ArgumentException("Font size must be higher than 0");

            if (!_validFontFamilies.ContainsKey(familyName))
                return null;

            return new Font(_validFontFamilies[familyName], size, style);
        }

        public FontFamily GetFontFamily(string familyName)
        {
            if (!_validFontFamilies.ContainsKey(familyName))
                return null;
            //throw new ArgumentException("Invalid font family name");

            return _validFontFamilies[familyName];
        }

        private void UpdateFontFamilies()
        {
            using (var installedFonts = new InstalledFontCollection())
            {
                for (var i = 0; i < installedFonts.Families.Length; i++)
                {
                    if (installedFonts.Families[i].IsStyleAvailable(FontStyle.Regular))
                    {
                        _validFontFamilies.Add(installedFonts.Families[i].Name, installedFonts.Families[i]);
                    }
                }
            }
        }

        #endregion Methods
    }
}