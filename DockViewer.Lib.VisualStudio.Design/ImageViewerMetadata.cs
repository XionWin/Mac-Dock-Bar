using Microsoft.Windows.Design.Features;
using Microsoft.Windows.Design.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: ProvideMetadata(typeof(DockViewer.Lib.VisualStudio.Design.ImageViewerMetadata))]
namespace DockViewer.Lib.VisualStudio.Design
{
    class ImageViewerMetadata : IProvideAttributeTable
    {
        // Accessed by the designer to register any design-time metadata.
        public AttributeTable AttributeTable
        {
            get
            {
                AttributeTableBuilder builder = new AttributeTableBuilder();


                // Add the menu provider to the design-time metadata.
                builder.AddCustomAttributes(
                    typeof(ImageViewer),
                    new FeatureAttribute(typeof(SpaceSliderAdornerProvider)));

                



                return builder.CreateTable();
            }
        }
    }
}
