using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using MAPP_w3.iOS.Renderers;

[assembly: ExportRenderer(typeof(ViewCell), typeof(CustomViewCellRenderer))]
namespace MAPP_w3.iOS.Renderers
{
    using UIKit;
    using Xamarin.Forms.Platform.iOS;
    public class CustomViewCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);

            cell.SelectionStyle = UITableViewCellSelectionStyle.None; 

            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

            return cell;
        }
    }
}
