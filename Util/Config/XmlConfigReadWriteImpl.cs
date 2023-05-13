using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Util.Config
{
    public class XmlConfigReadWriteImpl : IConfigReadWriteImpl
    {
        #region 常量
        private const string NODENAME_ROOT = "Root";
        private const string NODENAME_ITEM = "Item";
        private const string ATTRIBUTE_PROPERTYNAME = "Property";
        private const string ATTRIBUTE_NAME = "Name";
        private const string ATTRIBUTE_DESC = "Desc";

        private const string DEBUG_VALUE = "DebugValue";
        private const string RELEASE_VALUE = "Value";
        #endregion

        public XmlConfigReadWriteImpl(string path) 
        {
            SavePath = path;
            FileInfo file = new FileInfo(path);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
        }
        /// <summary>
        /// 配置数据保存路径
        /// </summary>
        public string SavePath { get; }



        #region 读
        public T GetConfig<T>() where T : new()
        {
            Type t = typeof(T);
            T output = new T();

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(SavePath);
            }
            catch
            {
                return output;
            }


            if (doc.DocumentElement.Name == NODENAME_ROOT)
            {
                Dictionary<string, XmlNode> nodes = new Dictionary<string, XmlNode>();

                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    if (node.NodeType != XmlNodeType.Element || node.Name != NODENAME_ITEM)
                    {
                        continue;
                    }
                    string propertyName = node.Attributes[ATTRIBUTE_PROPERTYNAME]?.Value;
                    if (!string.IsNullOrEmpty(propertyName))
                    {
                        nodes.Add(propertyName, node);
                    }
                }
                PropertyInfo[] properties = t.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    object obj = null;
                    if (nodes.ContainsKey(property.Name))
                    {
                        obj = Parse(nodes[property.Name], property.PropertyType);
                    }
                    if (obj == null)
                    {
                        // 取特性的默认值
                        property.ExistCustomAttribute<DefaultAttribute>((att) =>
                        {
                            obj = Util.String.StringConverter.Convert(att.DefaultValue, property.PropertyType);
                        });
                    }
                    if (obj == null) continue;
                    property.SetValue(output, obj);
                }
            }

            return output;
        }
        /// <summary>
        /// 将节点转换为指定类型的对象
        /// </summary>
        /// <param name="node"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        protected object Parse(XmlNode node, Type type)
        {
            object debug = null;
            object release = null;
            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                switch (child.Name) 
                {
#if DEBUG
                    case DEBUG_VALUE:
                        debug = ParseValue(child, type);
                        break;
#endif
                    case RELEASE_VALUE:
                        release = ParseValue(child, type);
                        break;
                }
            }
            return debug ?? release;
        }
        protected object ParseValue(XmlNode node, Type type)
        {
            //if (type.IsValueType || type == typeof(string) || type.IsEnum)
            //{ 
            //    return StringConverter.Convert(node.InnerText, type);
            //}
            using (StringReader reader = new StringReader(node.InnerXml))
            {
                try
                {
                    return new XmlSerializer(type).Deserialize(reader);
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion

        #region 写
        public void SaveConfig<T>(T config) where T : new()
        {
            XmlDocument doc = new XmlDocument();

            XmlNode root = doc.AppendChild(doc.CreateElement(NODENAME_ROOT));

            Type t = typeof(T);

            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.SetMethod == null) continue;

                object value = property.GetValue(config);
                if (value == null) continue;
                XmlNode node = CreateNode(doc, value);
                node.Attributes.SetNamedItem(CreateAttribute(doc, ATTRIBUTE_PROPERTYNAME, property.Name));
                property.ExistCustomAttribute<ConfigInfoAttribute>((attr) =>
                {
                    node.Attributes.SetNamedItem(CreateAttribute(doc, ATTRIBUTE_NAME, attr.Name));
                    node.AppendChild(CreateDescNode(doc, ATTRIBUTE_DESC, attr.Desc));
                });
                if (node == null) continue;
                root.AppendChild(node);
            }

            try
            {
                using (FileStream fs = new FileStream(SavePath, FileMode.Create))
                {
                    using (XmlWriter write = XmlWriter.Create(fs, new XmlWriterSettings()
                    {
                        Indent = true,
                        OmitXmlDeclaration = true,
                    }))
                    {
                        doc.Save(write);
                    }
                }
            }
            catch
            {

            }
        }
        protected XmlElement CreateNode(XmlDocument doc, object obj)
        {
            XmlElement node = doc.CreateElement(NODENAME_ITEM, null);
#if DEBUG
            node.AppendChild(CreateValueNode(doc, obj, DEBUG_VALUE));
#else
            node.AppendChild(CreateValueNode(doc, obj, RELEASE_VALUE));
#endif

            return node;
        }
        protected XmlElement CreateValueNode(XmlDocument doc, object obj, string nodeName)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            XmlElement valueNode = doc.CreateElement(nodeName);
            StringBuilder stringBuilder = new StringBuilder();
            using (StringWriter sw = new StringWriter(stringBuilder))
            {
                using (XmlWriter writer = XmlWriter.Create(sw, new XmlWriterSettings()
                {
                    OmitXmlDeclaration = true,
                    //Indent = true,
                }))
                {
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add(string.Empty, string.Empty);

                    serializer.Serialize(writer, obj, namespaces);;
                    // stringBuilder.AppendLine();
                    // valueNode.InnerXml = stringBuilder.ToString();
                    // valueNode.AppendChild(doc.CreateTextNode(stringBuilder.ToString()));
                    using (StringReader sr = new StringReader(stringBuilder.ToString()))
                    {
                        using (XmlReader reader = XmlReader.Create(sr))
                        {
                            valueNode.AppendChild(doc.ReadNode(reader));
                        }
                    }
                }
            }
            return valueNode;
        }

        protected XmlAttribute CreateAttribute(XmlDocument doc, string key, string value)
        {
            XmlAttribute node = doc.CreateAttribute(key);
            node.Value = value;
            return node;
        }
        protected XmlElement CreateDescNode(XmlDocument doc, string key, string value)
        {
            XmlElement node = doc.CreateElement(key);
            if (!string.IsNullOrEmpty(value))
            {
                node.InnerText = value;
            }
            return node;
        }
        #endregion


        class ConfigItem
        {
            public string PropertyName { get; set; }
            public string Name { get; set; }
            public string Desc { get; set; }
            public string Value { get; set; }
        }
    }
}
