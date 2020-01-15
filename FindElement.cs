using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;

namespace UIAutomationFormTest
{
    public class FindElement
    {
        public static List<UIAutomationElement> GetElementList(IntPtr intPtr, int x, int y)
        {
            List<UIAutomationElement> list = new List<UIAutomationElement>();
            AutomationElement startElement = AutomationElement.FromHandle(intPtr);
            AutomationElementCollection automationElementCollection;
            Queue<AutomationElement> elementQueue = new Queue<AutomationElement>();
            elementQueue.Enqueue(startElement);
            while (elementQueue.Count != 0)
            {
                startElement = elementQueue.Dequeue();
                list.Add(new UIAutomationElement(startElement.Current.Name, startElement.Current.AutomationId, startElement.Current.FrameworkId,
                    startElement.Current.ClassName, null));
                automationElementCollection = startElement.FindAll(TreeScope.Children, System.Windows.Automation.Condition.TrueCondition);
                foreach (AutomationElement aeElement in automationElementCollection)
                {
                    if (!elementQueue.Contains(aeElement) && aeElement.Current.BoundingRectangle.Contains(x, y) && aeElement.Current.ClassName != "Image")
                    {
                        elementQueue.Enqueue(aeElement);
                       
                    }
                }
            }
            Rect startElementRect = startElement.Current.BoundingRectangle;
            // 确认AutomationElement.FromPoint得到的元素和层层遍历得到的元素是否一致
            AutomationElement focusElement;
            focusElement = AutomationElement.FromPoint(new Point(x, y));
            if (focusElement.Current.ClassName != "Image")
            {
                Rect focusElementRect = focusElement.Current.BoundingRectangle;
                if (startElementRect.Width >= focusElementRect.Width && startElementRect.Height >= focusElementRect.Height)
                {
                    if (startElementRect.Width == focusElementRect.Width && startElementRect.Height == focusElementRect.Height) { }
                    else
                    {
                        list.Add(new UIAutomationElement(focusElement.Current.Name, focusElement.Current.AutomationId, focusElement.Current.FrameworkId,
                            focusElement.Current.ClassName, null));
                    }
                }
            }
            return list;
        }

        public static List<UIAutomationElement> GetElementList(AutomationElement startElement, int x, int y)
        {
            List<UIAutomationElement> list = new List<UIAutomationElement>();
            AutomationElementCollection automationElementCollection;
            AutomationElement focusElement = startElement;
            System.Windows.Automation.Condition conditions = new AndCondition(
                new NotCondition(new PropertyCondition(AutomationElement.ClassNameProperty, "Image")),
                new NotCondition(new PropertyCondition(AutomationElement.BoundingRectangleProperty, new Rect())),
                new PropertyCondition(AutomationElement.IsControlElementProperty, true),
                new PropertyCondition(AutomationElement.IsOffscreenProperty, false)
                );
            //automationElementCollection = startElement.FindAll(TreeScope.Subtree, Condition.TrueCondition);
            automationElementCollection = startElement.FindAll(TreeScope.Subtree, conditions);
            foreach (AutomationElement aeElement in automationElementCollection)
            {
                if (aeElement.Current.BoundingRectangle.Contains(x, y))
                {
                    if (aeElement.Current.BoundingRectangle.Width * aeElement.Current.BoundingRectangle.Height <=
                        focusElement.Current.BoundingRectangle.Width * focusElement.Current.BoundingRectangle.Height)
                    {
                        focusElement = aeElement;
                    }
                }
            }
            TreeWalker walker = TreeWalker.ControlViewWalker;
            
            while (focusElement != startElement)
            {
                list.Add(new UIAutomationElement(focusElement.Current.Name, focusElement.Current.AutomationId, focusElement.Current.LocalizedControlType,
                focusElement.Current.ClassName, null));
                focusElement = walker.GetParent(focusElement);
            }
            list.Add(new UIAutomationElement(focusElement.Current.Name, focusElement.Current.AutomationId, focusElement.Current.LocalizedControlType,
                focusElement.Current.ClassName, null));
            return list;
        }

        public static UIAutomationElement GetUIAutomationElement(IntPtr intPtr, int x, int y)
        {
            AutomationElement startElement = AutomationElement.FromHandle(intPtr);
            AutomationElementCollection automationElementCollection = startElement.FindAll(
                TreeScope.Children, System.Windows.Automation.Condition.TrueCondition);
            if (automationElementCollection.Count == 0)
                return null;
            List<UIAutomationElement> list = GetElementList(startElement, x, y);
            for (int i = 0; i < list.Count-1; i++)
            {
                list[i].Parent = list[i+1];
            }
            return list.Last();
        }

        public static AutomationElement GetElementFromPoint(int x, int y)
        {
            AutomationElement startElement = GetWindowElementByPoint(x, y);
            AutomationElement focusElement1 = GetElementFromPoint(x, y, 0);
            AutomationElement focusElement2 = GetElementFromPoint(startElement, x, y);
            AutomationElement finalElement = focusElement1;
            Rect rect1 = focusElement1.Current.BoundingRectangle;
            Rect rect2 = focusElement2.Current.BoundingRectangle;
            if (rect1.Width > rect2.Width && rect1.Height > rect2.Height)
            {
                finalElement = focusElement2;
            }
            return finalElement;
        }

        //AutomationElement.FromPoint方法
        public static AutomationElement GetElementFromPoint(int x, int y, int type)
        {
            return AutomationElement.FromPoint(new Point(x, y));
        }

        public static AutomationElement GetElementFromPoint(AutomationElement element, int x, int y)
        {
            Queue<AutomationElement> elementQueue = new Queue<AutomationElement>();
            elementQueue.Enqueue(element);
            AutomationElement resultElement = element;
            while (elementQueue.Count != 0)
            {
                resultElement = elementQueue.Dequeue();
                AutomationElementCollection automationElementCollection = resultElement.FindAll(TreeScope.Children, System.Windows.Automation.Condition.TrueCondition);
                foreach (AutomationElement aeElement in automationElementCollection)
                {
                    if (!elementQueue.Contains(aeElement) && aeElement.Current.BoundingRectangle.Contains(x, y))
                        elementQueue.Enqueue(aeElement);
                }
            }
            return resultElement;
        }
        public static AutomationElementCollection GetAllElement(AutomationElement parentElement)
        {
            if (parentElement == null)
            {
                throw new ArgumentException();
            }
            // Use TrueCondition to retrieve all elements.
            AutomationElementCollection elementCollectionAll = parentElement.FindAll(
                TreeScope.Subtree, System.Windows.Automation.Condition.TrueCondition);
            return elementCollectionAll;
        }

        public static AutomationElement GetWindowElementByPoint(int x, int y)
        {
            return AutomationElement.FromHandle(WinAPI.WindowFromPoint(x, y));
        }
    }


}
