using ColorPicker.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace PublicColor.Model
{
    public class StructColor : BindableBase
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        private string key;
        public string Key
        {
            get { return key; }
            set { SetProperty(ref key, value); }
        }

        private bool ischanged;
        public bool IsChanged
        {
            get
            {
                return ischanged;
            }
            set
            {
                SetProperty(ref ischanged, value);
            }
        }
        private string precolor;
        public string PreColor
        {
            get { return precolor; }
            set { SetProperty(ref precolor, value); }
        }

        private string nowcolor;
        public string NowColor
        {
            get { return nowcolor; }
            set { SetProperty(ref nowcolor, value); }
        }
        private ColorState clrstate;
        public ColorState ClrState
        {
            get { return clrstate; }
            set { SetProperty(ref clrstate, value); }
        }
        private Color clr;
        public Color Clr
        {
            get { return clr; }
            set { SetProperty(ref clr, value); }
        }
    }
    public class ColorModel : BindableBase
    {
        #region Model
        private List<StructColor> mycolors;
        public List<StructColor> MyColors
        {
            get { return mycolors; }
            set { SetProperty(ref mycolors, value); }
        }
        //Colors.cs에서 color값을 받아와서 현재 View에 저장
        private Dictionary<string, string> diccolors;
        public Dictionary<string, string> DicColors
        {
            get { return diccolors; }
            set { SetProperty(ref diccolors, value); }
        }


        #endregion Model

        #region Events
        private DelegateCommand commandsavecolors;
        public DelegateCommand CommandSaveColors =>
            commandsavecolors ?? (commandsavecolors = new DelegateCommand(ExecuteCommandSaveColors));

        void ExecuteCommandSaveColors()
        {
            for(int i = 0; i < MyColors.Count; i++) 
            {
                MyColors[i].NowColor = ColorToString(ColorStateToColor(MyColors[i].ClrState));
                if (MyColors[i].NowColor != MyColors[i].PreColor)
                    MyColors[i].IsChanged = true;
            }
            bool _b = false;
            foreach(var v in MyColors)
            {
                if (v.IsChanged)
                {
                    _b = true;
                    PublicColor.Colors.Colorinstance.SetColor(v.Key, v.NowColor);
                }
            }
            if (_b)
            {
                PublicColor.Colors.Colorinstance.SetColor();
                MessageBox.Show("변경완료");
            }
        }

        #endregion Events

        #region Functions
        /// <summary>
        /// DirColors값을 받아옴.
        /// </summary>
        private Dictionary<string, string> GetColors()
        {
            return PublicColor.Colors.Colorinstance.DirColors;
        }

        private ColorState StringToColorState(string color)
        {
            ColorState val = new ColorState();
            Color clr = StringToColor(color);
            val.RGB_R = clr.R / 255.0;
            val.RGB_G = clr.G / 255.0;
            val.RGB_B = clr.B / 255.0;
            val.A = clr.A / 255.0;

            return val;
        }

        private Color ColorStateToColor(ColorState cst)
        {
            Color val = new Color();
            val.R = (byte)Math.Round(255 * cst.RGB_R);
            val.G = (byte)Math.Round(255 * cst.RGB_G);
            val.B = (byte)Math.Round(255 * cst.RGB_B);
            val.A = (byte)Math.Round(255 * cst.A);

            return val;
        }

        private Color StringToColor(string color)
        {
            Color clr;
            byte A = Convert.ToByte((color[0].ToString() + color[1].ToString()), 16);
            byte R = Convert.ToByte((color[2].ToString() + color[3].ToString()), 16);
            byte G = Convert.ToByte((color[4].ToString() + color[5].ToString()), 16);
            byte B = Convert.ToByte((color[6].ToString() + color[7].ToString()), 16);
            clr = Color.FromArgb(
                    A,
                    R,
                    G,
                    B);

            return clr;
        }

        private Brush ColorToBrush(Color clr)
        {
            Brush br;
            var bc = new BrushConverter();

            return (Brush)bc.ConvertFrom($"#{ColorToString(clr)}");
        }

        private string ColorToString(Color clr)
        {
            string val = "";
            byte A = clr.A;
            byte G = clr.G;
            byte B = clr.B;
            byte R = clr.R;
            val = $"{A:X2}{R:X2}{G:X2}{B:X2}";

            return val;
        }
        #endregion Functions


        public ColorModel()
        {
            DicColors = GetColors();
            MyColors = new List<StructColor>();
            foreach (KeyValuePair<string,string> k in DicColors)
            {
                StructColor st = new StructColor();
                st.Name = k.Key;
                st.PreColor = k.Value;
                st.NowColor = k.Value;
                st.Key = k.Key;
                st.Clr = StringToColor(k.Value);
                st.IsChanged = false;
                st.ClrState = StringToColorState(k.Value);
                MyColors.Add(st);
            }

        }
    }
}
