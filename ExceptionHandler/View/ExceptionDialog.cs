#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Security;
using System.Windows.Forms;

#endregion

namespace ExceptionHandler.View
{
    public partial class ExceptionDialog : Form, IExceptionDialog
    {
        public PictureBox PictureBox1 { get { return _PictureBox1; } }
        public RichTextBox ErrorBox { get { return _ErrorBox; } }
        public RichTextBox ScopeBox { get { return _ScopeBox; } }
        public RichTextBox ActionBox { get { return _ActionBox; } }
        public TextBox TxtMore { get { return _TxtMore; } }

        public ExceptionDialog()
        {
            Load += UserErrorDialog_Load;

            //This call is required by the Windows Form Designer.
            InitializeComponent();

            //Add any initialization after the InitializeComponent() call
        }

        public Button Button1
        {
            get { return _withEventsFieldButton1; }
            set
            {
                if (_withEventsFieldButton1 != null)
                {
                    _withEventsFieldButton1.Click -= Button1Click;
                }
                _withEventsFieldButton1 = value;
                if (_withEventsFieldButton1 != null)
                {
                    _withEventsFieldButton1.Click += Button1Click;
                }
            }
        }

        public IButtonControl FormAcceptButton
        {
            set { AcceptButton = value; }
        }

        public Button Button2
        {
            get { return _withEventsFieldButton2; }
            set
            {
                if (_withEventsFieldButton2 != null)
                {
                    _withEventsFieldButton2.Click -= Button2Click;
                }
                _withEventsFieldButton2 = value;
                if (_withEventsFieldButton2 != null)
                {
                    _withEventsFieldButton2.Click += Button2Click;
                }
            }
        }

        public Button Button3
        {
            get { return _withEventsFieldButton3; }
            set
            {
                if (_withEventsFieldButton3 != null)
                {
                    _withEventsFieldButton3.Click -= Button3Click;
                }
                _withEventsFieldButton3 = value;
                if (_withEventsFieldButton3 != null)
                {
                    _withEventsFieldButton3.Click += Button3Click;
                }
            }
        }

        internal Button btnMore
        {
            get { return withEventsField_btnMore; }
        }

        public void FormShowDialog()
        {
            ShowDialog();
        }

        private const int IntSpacing = 10;

        //--
        //-- security-safe process.start wrapper
        //--
        private static void LaunchLink(string strUrl)
        {
            try
            {
                Process.Start(strUrl);
            }
            catch (SecurityException)
            {
                //-- do nothing; we can't launch without full trust.
            }
        }

        private static void SizeBox(Control ctl)
        {
            Graphics g = null;
            try
            {
                //-- note that the height is taken as MAXIMUM, so size the label for maximum desired height!
                g = Graphics.FromHwnd(ctl.Handle);
                var objSizeF = g.MeasureString(ctl.Text, ctl.Font, new SizeF(ctl.Width, ctl.Height));
                g.Dispose();
                ctl.Height = Convert.ToInt32(objSizeF.Height) + 5;
            }
            catch (SecurityException)
            {
                //-- do nothing; we can't set control sizes without full trust
            }
            finally
            {
                if ((g != null))
                    g.Dispose();
            }
        }

        private static DialogResult DetermineDialogResult(string strButtonText)
        {
            const DialogResult returnVal = DialogResult.None;

            //-- strip any accelerator keys we might have
            strButtonText = strButtonText.Replace("&", "");
            switch (strButtonText.ToLower())
            {
                case "abort":
                    return DialogResult.Abort;
                case "cancel":
                    return DialogResult.Cancel;
                case "ignore":
                    return DialogResult.Ignore;
                case "no":
                    return DialogResult.No;
                case "none":
                    return DialogResult.None;
                case "ok":
                    return DialogResult.OK;
                case "retry":
                    return DialogResult.Retry;
                case "yes":
                    return DialogResult.Yes;
            }
            return returnVal;
        }

        private void Button1Click(Object sender, EventArgs e)
        {
            Close();
            DialogResult = DetermineDialogResult(Button1.Text);
        }

        private void Button2Click(Object sender, EventArgs e)
        {
            Close();
            DialogResult = DetermineDialogResult(Button2.Text);
        }

        private void Button3Click(Object sender, EventArgs e)
        {
            Close();
            DialogResult = DetermineDialogResult(Button3.Text);
        }

        private void UserErrorDialog_Load(Object sender, EventArgs e)
        {
            //-- make sure our window is on top
            TopMost = true;
            TopMost = false;

            //-- More >> has to be expanded
            TxtMore.Anchor = AnchorStyles.None;
            TxtMore.Visible = false;

            //-- size the labels' height to accommodate the amount of text in them
            SizeBox(ScopeBox);
            SizeBox(ActionBox);
            SizeBox(ErrorBox);

            //-- now shift everything up
            lblScopeHeading.Top = ErrorBox.Top + ErrorBox.Height + IntSpacing;
            ScopeBox.Top = lblScopeHeading.Top + lblScopeHeading.Height + IntSpacing;

            lblActionHeading.Top = ScopeBox.Top + ScopeBox.Height + IntSpacing;
            ActionBox.Top = lblActionHeading.Top + lblActionHeading.Height + IntSpacing;

            lblMoreHeading.Top = ActionBox.Top + ActionBox.Height + IntSpacing;
            btnMore.Top = lblMoreHeading.Top - 3;

            Height = btnMore.Top + btnMore.Height + IntSpacing + 45;

            CenterToScreen();
        }

        private void btnMore_Click(Object sender, EventArgs e)
        {
            if (btnMore.Text == ">>")
            {
                Height = Height + 300;
                var with1 = TxtMore;
                with1.Location = new Point(lblMoreHeading.Left, lblMoreHeading.Top + lblMoreHeading.Height + IntSpacing);
                with1.Height = ClientSize.Height - TxtMore.Top - 45;
                with1.Width = ClientSize.Width - 2*IntSpacing;
                with1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                               | AnchorStyles.Left | AnchorStyles.Right;
                with1.Visible = true;
                Button3.Focus();
                btnMore.Text = "<<";
            }
            else
            {
                SuspendLayout();
                btnMore.Text = ">>";
                Height = btnMore.Top + btnMore.Height + IntSpacing + 45;
                TxtMore.Visible = false;
                TxtMore.Anchor = AnchorStyles.None;
                ResumeLayout();
            }
        }
    }
}