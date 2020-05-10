using System.Collections.Generic;
using System.Linq;

namespace UserManagement.Common.Constants
{
    public static class StaticTypes
    {
        public static List<string> ClinicType { get; set; } = new List<string>() {"Clinic", "Hospital", "Other"};

        public static List<string> VisibilityTypes { get; set; } =
            new List<string>() {"Visible", "Hidden", "Collapsed"};

        public static List<string> ButtonList { get; set; } = new List<string>()
        {
            "Button1", "Button2", "Button3",
            "Button3a", "Button3b", "Button3c",
        };

        public static List<string> IconList { get; set; } = new List<string>()
        {
            "None",
            "icon_check", "icon_adult", "icon_delete",
            "icon_edit", "icon_flag", "icon_move",
            "icon_old", "icon_syringe_green", "icon_syringe_yellow"
        };
    }

    public interface IUIButton
    {
        string ElementCMD { get; set; }
        string ElementText { get; set; }
        string ElementType { get; set; }
        string Visible { get; set; }
    }

    public interface IActionButton : IUIButton
    {
        bool Equals(object ob);
        int GetHashCode();
    }

    public class ActionButton : IActionButton
    {
        public string ElementCMD { get; set; } = string.Empty;
        public string ElementText { get; set; } = string.Empty;
        public string ElementType { get; set; } = string.Empty;

        public string Visible { get; set; } = string.Empty;

        public override bool Equals(object ob)
        {
            if (ob == null)
                return false;
            else if (Equals(ob as ActionButton))
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (ElementCMD != null ? ElementCMD.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ElementText != null ? ElementText.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ElementType != null ? ElementType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Visible != null ? Visible.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(AppointmentButton other)
        {
            return ElementCMD != null &&
                   ElementCMD == other.ElementCMD &&
                   ElementText == other.ElementText &&
                   ElementType == other.ElementType &&
                   Visible == other.Visible;
        }

    }

    public interface IAppointmentButton : IUIButton
    {
        string Icon1 { get; set; }
        string Icon2 { get; set; }
        bool Equals(object ob);
        int GetHashCode();
    }

    public class AppointmentButton : ActionButton, IAppointmentButton

    {
        public string Icon1 { get; set; } = string.Empty;
        public string Icon2 { get; set; } = string.Empty;

        public override bool Equals(object ob)
        {
            if (ob == null)
                return false;
            else if (Equals(ob as AppointmentButton))
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (Icon1 != null ? Icon1.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Icon2 != null ? Icon2.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected new bool Equals(AppointmentButton other)
        {
            return base.Equals(other) &&
                   Icon1 == other.Icon1 &&
                   Icon2 == other.Icon2;
        }
    }

    public class Clinic
    {
        public string ClinicType = string.Empty;
        public List<ActionButton> ActButtons { get; set; } = new List<ActionButton>();
        public List<AppointmentButton> AppButtons { get; set; } = new List<AppointmentButton>();

        public override bool Equals(object ob)
        {
            return Equals(ob as Clinic);
        }

        protected bool Equals(Clinic other)
        {
            return other != null &&
                   ClinicType == other.ClinicType &&
                   AppButtons.Count == other.AppButtons.Count &&
                   AppButtons.Where((t, j) => t.Equals(other.AppButtons[j])).Any() &&
                   ActButtons.Count == other.ActButtons.Count &&
                   ActButtons.Where((t, j) => t.Equals(other.ActButtons[j])).Any();
        }

        public override int GetHashCode()
        {
            return (ClinicType != null ? ClinicType.GetHashCode() : 0);
        }
    }

    public class Clinics
    {
        public Clinics()
        {
            ClinicList = new List<Clinic>();
        }

        public List<Clinic> ClinicList { get; set; }
    }
}