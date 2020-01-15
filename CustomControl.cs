using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;
using System.Windows.Controls.Primitives;

namespace UIAutomationFormTest
{
    class CustomControl
    {
    }

    public class NumericUpDown : RangeBase
    {
        public NumericUpDown()
        {
            // other initialization; DefaultStyleKey etc.
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new NumericUpDownAutomationPeer(this);
        }
    }
    public class NumericUpDownAutomationPeer: FrameworkElementAutomationPeer
    {
        public NumericUpDownAutomationPeer(NumericUpDown owner) : base(owner)
        { }
        protected override string GetClassNameCore()
        {
            return "NumericUpDown";
        }
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Spinner;
        }

    }
}
