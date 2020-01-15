using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIAutomationFormTest
{
    public class UIAutomationElement
    {
        #region 内部属性
        /// <summary>
        /// 组件父级元素
        /// </summary>
        private UIAutomationElement parent;

        /// <summary>
        /// 元素Name
        /// </summary>
        private string elementName;

        /// <summary>
        /// 元素AutomationId
        /// </summary>
        private string elementAutomationId;

        /// <summary>
        /// 元素FrameworkId
        /// </summary>
        private string elementControlType;

        /// <summary>
        /// 元素ClassName
        /// </summary>
        private string elementClassName;

        #endregion

        #region 公开属性

        /// <summary>
        /// 获取组件父级元素
        /// </summary>
        public UIAutomationElement Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        /// <summary>
        /// 获取元素Name
        /// </summary>
        public string ElementName
        {
            get { return elementName; }
            set { elementName = value; }
        }

        /// <summary>
        /// 获取元素AutomationId
        /// </summary>
        public string ElementAutomationId
        {
            get { return elementAutomationId; }
            set { elementAutomationId = value; }
        }

        /// <summary>
        /// 获取元素FrameworkId
        /// </summary>
        public string ElementControlType
        {
            get { return elementControlType; }
            set { elementControlType = value; }
        }

        /// <summary>
        /// 获取元素ClassName
        /// </summary>
        public string ElementClassName
        {
            get { return elementClassName; }
            set { elementClassName = value; }
        }

        public List<UIAutomationElement> Elements
        {
            get
            {
                List<UIAutomationElement> list = new List<UIAutomationElement>();
                UIAutomationElement ele = this;
                while (true)
                {
                    list.Insert(0, ele);
                    if (ele.parent == null)
                        break;
                    ele = ele.parent;
                }
                return list;
            }
        }

        #endregion
        #region 构造函数
        public UIAutomationElement(string sElementName, string sElementAutomationId, string sElementControlType, string sElementClassName, UIAutomationElement aeParent)
        {
            this.elementName = sElementName;
            this.elementAutomationId = sElementAutomationId;
            this.elementControlType = sElementControlType;
            this.elementClassName = sElementClassName;
            this.parent = aeParent;
        }
        public UIAutomationElement()
        {

        }
        #endregion
    }
}
