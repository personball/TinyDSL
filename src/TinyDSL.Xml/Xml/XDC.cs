using System.Xml.Linq;

namespace TinyDSL.Xml
{
    public static class XDC
    {
        public static dynamic New(string name, object attributes = null)
        {
            var element = new XElement(name);
            if (attributes != null)
            {
                SetAttributes(element, attributes);
            }

            return New(element);
        }

        public static dynamic New(XElement element)
        {
            return (dynamic)new XmlDynamicConstructor(element);
        }

        public static void SetAttributes(XElement child, object arg)
        {
            var properties = arg.GetType().GetProperties();

            foreach (var property in properties)
            {
                child.SetAttributeValue(property.Name, property.GetValue(arg));
            }
        }
    }
}
