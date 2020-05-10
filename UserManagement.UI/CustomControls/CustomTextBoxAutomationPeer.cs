using System;
using System.Windows;
using System.Windows.Automation.Peers;

namespace UserManagement.UI.CustomControls
{
    public class CustomTextBoxAutomationPeer : FrameworkElementAutomationPeer
    {
        public CustomTextBoxAutomationPeer(FrameworkElement owner) : base(owner)
        {
            if (!(owner is CustomTextBox))
                throw new ArgumentOutOfRangeException();
        }

        private CustomTextBox MyOwner => (CustomTextBox)Owner;

        public override object GetPattern(PatternInterface patternInterface)
        {
            return patternInterface == PatternInterface.Text ? this : base.GetPattern(patternInterface);
        }

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Text;
        }

        protected override System.Collections.Generic.List<AutomationPeer> GetChildrenCore()
        {
            var children = base.GetChildrenCore();

            if (!(Owner is CustomTextBox) || children == null || children.Count <= 0) return children;

            if (!(children[children.Count - 1] is DocumentAutomationPeer)) return children;

            children.RemoveAt(children.Count - 1);

            if (children.Count == 0) children = null;

            return children;
        }

        protected override string GetClassNameCore()
        {
            return "CustomTextBox";
        }

        protected override string GetNameCore()
        {
            return "CustomTextBox";
        }
    }
}