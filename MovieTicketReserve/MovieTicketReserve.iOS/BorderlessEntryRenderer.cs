using Foundation;
using MovieTicketReserve.iOS;
using MovieTicketReserve.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace MovieTicketReserve.iOS
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
       protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
                Control.BorderStyle = UIKit.UITextBorderStyle.None;
        }
    }
}