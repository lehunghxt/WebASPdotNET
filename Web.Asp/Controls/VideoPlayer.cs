using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("Web.Asp.Controls", "VIT")]
namespace Web.Asp.Controls
{
    [DefaultProperty("FilePath")]
    [ToolboxData("<{0}:VideoPlayer runat=server></{0}:VideoPlayer>")]
    public class VideoPlayer : WebControl
    {
        #region enum

        public enum VSkin
        {
            SwfFormFix,

            PlayerViral,

            MediaPlayer
        }

        #endregion

        #region Declarations

        private VSkin _Skin;

        private string mFilePath;

        private string mSwfPath;

        private string mImagePath;

        private string idContent;

        private bool allowFullScreen;

        private bool autoStart;

        private bool mShowStatusBar;

        private bool mShowControls;

        private bool mShowPositionControls;

        private bool mShowTracker;

        #endregion

        #region Properties

        [Category("Skin")]
        [Browsable(true)]
        [Description("Skin on control")]
        [Editor(typeof(System.Web.UI.Design.UrlEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public VSkin VideoSkin
        {
            get
            {
                return _Skin;
            }
            set
            {
                _Skin = value;
            }
        }

        [Category("File URL")]
        [Browsable(true)]
        [Description("link file video")]
        [Editor(typeof(System.Web.UI.Design.UrlEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string FilePath
        {
            get
            {
                return mFilePath;
            }
            set
            {
                if (value == string.Empty)
                {
                    mFilePath = string.Empty;
                }
                else
                {
                    int tilde = -1;
                    tilde = value.IndexOf('~');
                    if (tilde != -1)
                    {
                        mFilePath = value.Substring((tilde + 2)).Trim();
                    }
                    else
                    {
                        mFilePath = value;
                    }
                }
            }
        }

        [Category("Image URL")]
        [Browsable(true)]
        [Description("Hình đại diện hiển thị trước khi chạy video")]
        [Editor(typeof(System.Web.UI.Design.UrlEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string ImagePath
        {
            get
            {
                return mImagePath;
            }
            set
            {
                if (value == string.Empty)
                {
                    mImagePath = string.Empty;
                }
                else
                {
                    int tilde = -1;
                    tilde = value.IndexOf('~');
                    if (tilde != -1)
                    {
                        mImagePath = value.Substring((tilde + 2)).Trim();
                    }
                    else
                    {
                        mImagePath = value;
                    }
                }
            }
        }

        [Category("AllowFullScreen")]
        [Browsable(true)]
        [Description("Cho phép hiện nút fullscreen")]
        public bool AllowFullScreen
        {
            get
            {
                return allowFullScreen;
            }
            set
            {
                allowFullScreen = value;
            }
        }

        [Category("SwfPath")]
        [Browsable(true)]
        [Description("đường dẫn file swf")]
        public string SwfPath
        {
            get
            {
                return mSwfPath;
            }
            set
            {
                mSwfPath = value;
            }
        }

        [Category("IdContent")]
        [Browsable(true)]
        [Description("id của tag chứa embed")]
        public string IdContent
        {
            get
            {
                return idContent;
            }
            set
            {
                idContent = value;
            }
        }

        [Category("AutoStart")]
        [Browsable(true)]
        [Description("Tự động play")]
        public bool AutoStart
        {
            get
            {
                return autoStart;
            }
            set
            {
                autoStart = value;
            }
        }




        [Category("Media Player")]
        [Browsable(true)]
        [Description("Show or hide the tracker.")]
        public bool ShowTracker
        {
            get
            {
                return mShowTracker;
            }
            set
            {
                mShowTracker = value;
            }
        }

        [Category("Media Player")]
        [Browsable(true)]
        [Description("Show or hide the position controls.")]
        public bool ShowPositionControls
        {
            get
            {
                return mShowPositionControls;
            }
            set
            {
                mShowPositionControls = value;
            }
        }

        [Category("Media Player")]
        [Browsable(true)]
        [Description("Show or hide the controls.")]
        public bool ShowControls
        {
            get
            {
                return mShowControls;
            }
            set
            {
                mShowControls = value;
            }
        }

        [Category("Media Player")]
        [Browsable(true)]
        [Description("Show or hide the status bar.")]
        public bool ShowStatusBar
        {
            get
            {
                return mShowStatusBar;
            }
            set
            {
                mShowStatusBar = value;
            }
        }

        #endregion

        #region "Rendering"

        protected override void RenderContents(HtmlTextWriter writer)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                switch (VideoSkin)
                {
                    case VSkin.SwfFormFix:
                        sb.Append("<script type='text/javascript'>");
                        sb.AppendFormat(
                            "var so = new SWFObject(noCacheIE('{0}'), 'sotester', '{1}', '{2}', '0', '#123456');",
                            mSwfPath,
                            Width.Value,
                            Height.Value);
                        sb.AppendFormat("so.addParam('allowFullScreen', '{0}');", allowFullScreen);
                        sb.Append("so.addParam('scale', 'noScale');");
                        sb.AppendFormat("so.addVariable('source_file', '{0}');", mFilePath);
                        sb.Append("so.addVariable('default_resize_mode', 'fillScreen');");
                        sb.AppendFormat("so.addVariable('default_thumb_height', '{0}');", Height.Value);
                        sb.Append("so.addVariable('prevent_xml_cache', 'true');");
                        sb.Append("<embed src=" + FilePath.ToString() + " ");
                        sb.Append(
                            "pluginspage=http://www.microsoft.com/Windows/MediaPlayer type=application/x-mplayer2 ");
                        sb.AppendFormat("so.addVariable('auto_start', '{0}');", autoStart);
                        sb.AppendFormat("so.write('{0}');", idContent);
                        sb.Append("</script>");
                        break;
                    case VSkin.PlayerViral:
                        sb.AppendFormat(
                            "<object id='player' classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' name='player' width='{0}' height='{1}'>",
                            Width.Value,
                            Height.Value);
                        sb.AppendFormat("<param name='movie' value='{0}' />", mSwfPath);
                        sb.AppendFormat("<param name='allowfullscreen' value='{0}' />", allowFullScreen);
                        sb.Append("<param name='allowscriptaccess' value='always' />");
                        sb.AppendFormat("<param name='flashvars' value='file={0}&image={1}' />", mFilePath, mImagePath);
                        sb.AppendFormat(
                            "<embed type='application/x-shockwave-flash' id='player2' name='player2' src='{0}' width='{1}' height='{2}' allowscriptaccess='always' allowfullscreen='{4}' flashvars='file={5}&image={6}/>",
                            mSwfPath,
                            Width.Value,
                            Height.Value,
                            allowFullScreen,
                            mFilePath,
                            mImagePath);
                        sb.Append("</object>");
                        break;
                    case VSkin.MediaPlayer:
                        sb.Append("<object classid=clsid:22D6F312-B0F6-11D0-94AB-0080C74C7E95 ");
                        sb.Append(
                            "codebase=http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=5,1,52,701 Width = "
                            + Width.Value.ToString() + " Height = " + Height.Value.ToString()
                            + "type=application/x-oleobject align=absmiddle");
                        sb.Append("standby='Loading Microsoft+reg; Windows+reg; Media Player components...' id=mp1 /> ");
                        sb.Append("<param name=FileName value=" + FilePath.ToString() + "> ");
                        sb.Append("<param name=ShowStatusBar value=" + ShowStatusBar.ToString() + "> ");
                        sb.Append("<param name=ShowPositionControls value=" + ShowPositionControls.ToString() + "> ");
                        sb.Append("<param name=ShowTracker value=" + ShowTracker.ToString() + "> ");
                        sb.Append("<param name=ShowControls value=" + ShowControls.ToString() + "> ");
                        sb.Append("<embed src=" + FilePath.ToString() + " ");
                        sb.Append(
                            "pluginspage=http://www.microsoft.com/Windows/MediaPlayer type=application/x-mplayer2 ");
                        sb.Append("Width = " + Width.Value.ToString() + " ");
                        sb.Append("Height = " + Height.Value.ToString());
                        sb.Append(" /></embed></object>");

                        break;
                }

                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(sb.ToString());
                writer.RenderEndTag();
            }
            catch
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write("Display VedeoPlayer Control");
                writer.RenderEndTag();
            }
        }

        #endregion
    }
}
