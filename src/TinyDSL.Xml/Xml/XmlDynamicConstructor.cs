using System;
using System.Dynamic;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace TinyDSL.Xml
{
    public sealed class XmlDynamicConstructor : DynamicObject
    {
        public XElement Element { get; }
        public XElement RootElement { get { return _root.Element; } }
        private XmlDynamicConstructor _root;

        public XmlDynamicConstructor(XElement element)
        {
            Element = element;
            _root = this;
        }

        public XmlDynamicConstructor(XElement element, XmlDynamicConstructor root)
        {
            Element = element;
            _root = root;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var name = GetName(binder.Name);//TODO@personball deal with numbers
            var child = new XElement(name);
            Element.Add(child);
            result = new XmlDynamicConstructor(child, _root);
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var name = GetName(binder.Name);//TODO deal with numbers
            var child = new XElement(name);

            if (args.Length > 0)
            {
                var arg = args[0];
                XDC.SetAttributes(child, arg);
            }

            Element.Add(child);
            result = new XmlDynamicConstructor(child, _root);
            return true;
        }

        private string GetName(string name)
        {
            return name;
        }

        public override bool TryBinaryOperation(BinaryOperationBinder binder, object arg, out object result)
        {
            if (!(arg is XmlDynamicConstructor))
            {
                throw new ArgumentException("operatiing object should be type of XmlDynamicConstructor which is created by XDC.New!");
            }

            dynamic brother = arg;

            if (binder.Operation == ExpressionType.Add
                || binder.Operation == ExpressionType.AddChecked)
            {
                Element.AddAfterSelf(brother.RootElement);
            }
            else
            {
                throw new NotImplementedException();
            }

            result = new XmlDynamicConstructor(Element, _root);
            return true;
        }

        public override string ToString()
        {
            return RootElement.ToString();
        }
    }
}
